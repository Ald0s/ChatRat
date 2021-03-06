﻿/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A wrapper for a rich text box that functions as the primary text output.
But can target any rich text box.

Provides macros that print colourful text, in different formats.
And also some macros for commonly printed things like 'joined the server!'
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using ChatRat.Network.Messages;
using ChatRat.Network.Client;

namespace ChatRat.Elements {
    public class CBeautifulText {
        private RichTextBox dest;
        private Queue<CWriteOrder> orders;

        private Color defaultColour = Color.DarkGray;

        public CBeautifulText(RichTextBox _dst) {
            this.dest = _dst;
            this.orders = new Queue<CWriteOrder>();
        }

        #region Macros
        public void RoomChangeResult(CRoom room, bool success) {
            if(success)
                WriteTexts(
                    new string[] { "We successfully moved you to '", room.DisplayName, "'" },
                    new Color[] { defaultColour, Color.DarkTurquoise, defaultColour
                    });
            else
                WriteTexts(
                    new string[] { "We were unable to change your room. '" },
                    new Color[] { defaultColour
                    });
        }

        public void UserJoinRoom(COfflineUser user, CRoom newRoom) {
            WriteTexts(
                new string[] { "[" + user.Rank.Name + "] " + user.Username, " has joined your room." },
                new Color[] { user.Rank.Colour, defaultColour, Color.Indigo, defaultColour
                });
        }

        public void UserLeftRoom(COfflineUser user, CRoom old) {
            WriteTexts(
                new string[] { "[" + user.Rank.Name + "] " + user.Username, " has left your room." },
                new Color[] { user.Rank.Colour, defaultColour, Color.Indigo, defaultColour
                });
        }

        public void ProcessJoinLeave(msg_JoinLeave message) {
            COfflineUser user = message.ReadUser();
            CRoom room = message.ReadRoom();
            bool join_or_leave = message.ReadBool();

            ProcessJoinLeave(user, room, join_or_leave);
        }

        public void ProcessJoinLeave(COfflineUser user, CRoom subject, bool join_or_leave) {
            if (join_or_leave)
                UserJoinRoom(user, subject);
            else
                UserLeftRoom(user, subject);
        }

        public void ChatMessage(msg_SendMessage chat) {
            // Some logic for timestamp here.
            // We'll miss out on it for now cos it makes the output look dirty.
            
            COfflineUser user = chat.ReadUser();
            string msg = chat.ReadString();
            double timestamp = chat.ReadDouble();
            
            WriteTexts(
                new string[] { "[" + user.Rank.Name + "] " + user.Username, ": " + msg },
                new Color[] { user.Rank.Colour, defaultColour
                });
        }
        
        public void DisconnectFromServer(string reason) {
            WriteTexts(
                new string[] { "You've been disconnected from the server! (", reason, ")" },
                new Color[] { defaultColour, Color.Firebrick, defaultColour
                });
        }

        public void WriteServerInfo(string name, string motd) {
            WriteTexts(
                new string[] { "Connected to '", name, "'! The server says; ", motd },
                new Color[] { defaultColour, Color.Purple, defaultColour, defaultColour
                });
        }

        public void UserJoined(string username, CRank rank) {
            WriteTexts(
                new string[] { (username == null) ? "Unknown" : username, " has joined the server!" },
                new Color[] { rank.Colour, defaultColour
                });
        }

        public void UserLeft(string username, CRank rank) {
            WriteTexts(
                new string[] { (username == null) ? "Unknown" : username, " has left the server! :(" },
                new Color[] { rank.Colour, defaultColour
                });
        }
        #endregion

        public void WriteText(string text, Color color) {
            orders.Enqueue(new CWriteOrder(
                new string[] { text }, 
                new Color[] { (color == Color.Empty) ? defaultColour : color }));

            OrderReceived();
        }

        public void WriteText(string tag, Color tag_color, string text) {
            orders.Enqueue(new CWriteOrder(
                new string[] { "[" + tag + "] ", text },
                new Color[] { tag_color, defaultColour }));

            OrderReceived();
        }

        public void WriteTexts(string[] texts, Color[] colors) {
            orders.Enqueue(new CWriteOrder(texts, colors));

            OrderReceived();
        }

        private void OrderReceived() {
            if (orders.Count <= 0)
                return;

            for(int i = 0; i < orders.Count; i++) {
                if (dest == null)
                    return;

                orders.Dequeue().Write(dest);
            }
        }

        private delegate void Clear_Delegate();
        public void Clear() {
            if (dest.InvokeRequired) {
                Clear_Delegate clear = new Clear_Delegate(Clear);
                dest.Invoke(clear);
            } else {
                dest.Clear();
            }
        }
    }

    public class CWriteOrder {
        private delegate void Write_ThreadSafe(RichTextBox target);

        private string[] texts;
        private Color[] colors;

        public CWriteOrder(string[] _texts, Color[] _colors) {
            this.texts = _texts;
            this.colors = _colors;
        }

        public void Write(RichTextBox target) {
            try {
                if (target.InvokeRequired) {
                    Write_ThreadSafe write = new Write_ThreadSafe(Write);
                    target.Invoke(write, new object[] { target });

                    return;
                } else {
                    target.ScrollToCaret();

                    if (colors == null || colors.Length == 0)
                        colors = new Color[] { Color.DarkGray };

                    if (texts == null || texts.Length == 0)
                        return;

                    Color old = target.ForeColor;
                    for (int i = 0; i < texts.Length; i++) {
                        if (i < colors.GetUpperBound(0) + 1)
                            target.SelectionColor = colors[i];

                        target.SelectedText = texts[i];
                    }

                    target.SelectionColor = old;
                    target.AppendText(Environment.NewLine);
                }
            } catch (ObjectDisposedException) {
                return;
            }
            catch (NullReferenceException) {
                return;
            }
        }
    }
}
