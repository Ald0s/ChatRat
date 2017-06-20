/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
Our ChatRat server class, deriving from CServerMain.
Performs administrative functions over the connected users, and will eventually coordinate messages and
user moderation actions.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using ZapNetwork;
using ZapNetwork.Server;
using ZapNetwork.Shared;
using ChatRat.Elements;
using ChatRat.Network.Messages;

namespace ChatRat.Network.Server {
    // Custom type for Server.
    public class CServer : CServerMain {
        private CChatRooms rooms;
        private CBeautifulText beautiful;

        private string sUsername;

        public CServer(ServerCfg _cfg, CBeautifulText _txt)
            :base(_cfg) {
            this.rooms = new CChatRooms(Clients);

            this.ServerStarted += CServer_ServerStarted;
            this.ServerStopped += CServer_ServerStopped;

            this.beautiful = _txt;
        }

        public void SetInfo(string _user) {
            sUsername = _user;
        }

        public void Broadcast(CRatMessage msg) {
            for(int i = 0; i < Clients.Count; i++) {
                Clients[i].SendNetMessage(msg);
            }
        }

        public CUser GetLocalhost() {
            return (CUser)Clients[0];
        }

        public void HandleInput(string input, DateTime time) {
            // We can handle input commands here. But for now ignore them.
            // This is coming FROM our localhost user.

            GetLocalhost().SendNetMessage(new msg_CreateMessage(input, time));
        }

        private void CServer_ServerStarted() {
            beautiful.Clear();
            beautiful.WriteText("Your server is open for business!", Color.Green);

            // Create a localhost CUser so we can interact
            // equally with all other users.
            
            // We'll accomplish this by using null for both arguments in CUser,
            // which lets CUser know that class will be a localhost.
            NewClient(null, null);
        }

        private void CServer_ServerStopped(string reason) {
            beautiful.WriteText(reason, Color.DarkRed);
        }

        protected override void NewClient(CServerMain main, TcpClient client) {
            // Call UserConnected() with custom type INSTEAD of base.
            CUser user = new CUser(main, client);

            user.Authenticated += User_Authenticated;
            user.NetMessageReceived += NetMessageReceived_Public;
            user.Disconnected += User_Disconnected;

            // If the new client is localhost, 
            // begin force authenticate it.
            if (user.IsLocalhost) {
                user.BeginLocalhost(sUsername);
            }

            base.UserConnected(user);
        }

        private void User_Authenticated(CServerClient client) {
            CUser user = (CUser)client;
            rooms.MoveToRoom(user, "home");

            // Make other clients aware of this new user.
            beautiful.UserJoined(user.Username, user.Rank);
            Broadcast(new msg_NewUser(user));
        }

        private void NetMessageReceived_Public(CServerClient client, CNetMessage message) {
            // For handling PUBLIC net messages. This includes things like chat messages.
            
            switch(message.GetMessageName()) {
                case "create_chatmsg": // This needs to be processed and rebroadcasted.
                    RawMessageReceived((CUser)client, (msg_CreateMessage)message);
                    break;

                case "sent_chatmsg": // The loopback of the processed message.
                    beautiful.ChatMessage((msg_SendMessage)message);
                    break;
            }
        }

        private void User_Disconnected(CServerClient client, string reason) {
            CUser user = (CUser)client;
            
            // Tell all clients this one has disconnected.
            beautiful.UserLeft(user.Username, user.Rank);
            Broadcast(new msg_LostUser(user));
        }

        private void RawMessageReceived(CUser client, msg_CreateMessage message) {
            // Handle the chat message serverside.
            // Broadcast to all users it relates to and print to our local text box.
            string msg = message.ReadString();
            double time = message.ReadDouble();

            msg_SendMessage send = new msg_SendMessage(client, msg, time);

            // Logic for chat rooms here.
            // Broadcast to all clients in the same room here.
            CRoom target = client.Room;
            target.ChatMessage(send);
        }
    }
}
