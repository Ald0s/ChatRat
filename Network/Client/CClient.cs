/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
The ChatRat client.
Deriving from Zap's CClientMain, this class exposes functionality that is directly chat-related.
Also exposes an internal mirror of the connected users list, which will provide user information to the front end UI.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using ZapNetwork;
using ZapNetwork.Client;
using ZapNetwork.Shared;
using ChatRat.Elements;
using ChatRat.Network.Messages;

namespace ChatRat.Network.Client {
    // Our custom type for the client.
    public class CClient : CClientMain {
        // Information pertaining to just us.
        // Client ID is held by our parent, CClientMain.
        // Referred to by 'ClientID'
        public string Username { get { return this.sUsername; } }
        public CRank Rank { get { return this.rank; } }
        public CRoom Room { get { return this.room; } }

        private CRank rank;
        private CRoom room;
        private string sUsername;
        //

        public List<COfflineUser> Users { get { return this.users; } }

        // Room related events.
        public delegate void RoomAdded_Delegate(CRoom room);
        public event RoomAdded_Delegate RoomAdded;

        public delegate void RoomRemoved_Delegate(CRoom room);
        public event RoomRemoved_Delegate RoomRemoved;
        //

        // A clientside mirror of all users connected to the server.
        private List<COfflineUser> users;

        private CBeautifulText beautiful;

        public CClient(ClientCfg _cfg, CBeautifulText _txt)
            :base(_cfg) {
            this.rank = new CRank(null, Color.Empty);
            this.beautiful = _txt;

            this.Authenticated += CClient_Authenticated;
            this.NetMessageReceived += CClient_NetMessageReceived;
            this.Disconnected += CClient_Disconnected;
        }

        public void Connect(string username, ClientCfg cfg = null) {
            this.sUsername = username;

            beautiful.WriteText("Attempting to connect to the chosen server ...", Color.Empty);
            base.Connect(cfg, true);
        }

        public void HandleInput(string input, DateTime time) {
            // This is going to the server.

            SendNetMessage(new msg_CreateMessage(input, time));
        }

        public void ChangeRoom(CRoom chosen) {
            // This is going to the server.

            SendNetMessage(new msg_ChangeRoom(chosen));
        }

        private void CClient_Authenticated(CClientMain client) {
            beautiful.WriteServerInfo(ServerName, ServerDescription);

            // Initialise clientside mirror of users.
            // Maybe a new type that supports in-active storage?
            if (users == null)
                users = new List<COfflineUser>();
        }

        protected override void SendSetupData(CNetMessage setup) {
            // Write our chosen username.
            setup.WriteString(sUsername);

            base.SendSetupData(setup);
        }

        protected override void ReceivedSetupData(CNetMessage setup) {
            // Contains some initial information about us and the server.
            string rankName = setup.ReadString();
            Color rankColour = setup.ReadColour();
            rank.UpdateRank(rankName, rankColour);

            base.ReceivedSetupData(setup);
        }

        private void CClient_Disconnected(string reason) {
            // When we've been disconnected. (Whether by us or the server.)
            users.Clear();
            sUsername = null;
            rank.WipeRank();

            beautiful.DisconnectFromServer(reason);
        }

        private void CClient_NetMessageReceived(CClientMain client, ZapNetwork.Shared.CNetMessage message) {
            switch (message.GetMessageName()) {
                case "actionreview":
                    ((msg_ActionReview)message).Process(beautiful);
                    break;

                case "serverinfo":
                    ProcessServerInformation((msg_ServerInfo)message);
                    break;

                case "newuser":
                    AddUser(((msg_NewUser)message).GetUser());
                    break;

                case "lostuser":
                    RemoveUser(((msg_LostUser)message).GetUser());
                    break;
                    
                case "sent_chatmsg": 
                    beautiful.ChatMessage((msg_SendMessage)message);
                    break;

                case "addremoveroom": // A loop back of add or remove.
                    HandleRoomLoopback((msg_AddRemoveRoom)message);
                    break;

                case "joinleave":
                    beautiful.ProcessJoinLeave((msg_JoinLeave)message);
                    break;

                case "changeroom_result":
                    ProcessChangeRoomResult((msg_ChangeRoomResult)message);
                    break;
            }
        }

        private void ProcessChangeRoomResult(msg_ChangeRoomResult message) {
            CRoom room = message.ReadRoom();
            bool success = message.ReadBool();

            this.room = room;
            beautiful.RoomChangeResult(room, success);
        }

        private void ProcessServerInformation(msg_ServerInfo message) {
            // ERROR CASE
            // Setting (default) client room to Home statically.
            this.room = new CRoom("home", "Home", true);

            CRoom[] rooms = message.ReadRooms();
            if(RoomAdded != null) {
                for(int i = 0; i < rooms.Length; i++) {
                    RoomAdded(rooms[i]);
                }
            }
        }

        private void AddUser(COfflineUser user) {
            if (UserExists(user))
                return;

            users.Add(user);
            // Report an update. See below.
        }

        private void UpdateUser(COfflineUser user) {
            // A lazy method of updating. Just remove the user if they're in the list,
            // and add the new one, whilst refusing to report the removal to the UI.
            RemoveUser(user, false);
            AddUser(user);

            // Report an update. See below.
        }

        private void RemoveUser(COfflineUser user, bool report = true) {
            for(int i = 0; i < users.Count; i++) {
                if(users[i].ClientID == user.ClientID) {
                    users.RemoveAt(i);
                }
            }

            if (report) {
                // Fire the event that'll report a change to the UI.
                // The UI will then make all necessary changes to synchronise with the backend;
                // such as updating the ListView containing users.
            }
        }

        private bool UserExists(COfflineUser user) {
            for (int i = 0; i < users.Count; i++) {
                if (users[i].ClientID == user.ClientID) {
                    return true;
                }
            }
            return false;
        }

        private void HandleRoomLoopback(msg_AddRemoveRoom addrem) {
            CRoom room = addrem.ReadRoom();
            bool add_or_remove = addrem.ReadBool();

            if (add_or_remove && RoomAdded != null)
                RoomAdded(room);
            else if (!add_or_remove && RoomRemoved != null)
                RoomRemoved(room);
        }
    }
}
