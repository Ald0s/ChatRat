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
        private CBeautifulText beautiful;

        public CServer(ServerCfg _cfg, CBeautifulText _txt)
            :base(_cfg) {
            this.ServerStarted += CServer_ServerStarted;
            this.ServerStopped += CServer_ServerStopped;

            this.beautiful = _txt;
        }

        private void CServer_ServerStarted() {
            beautiful.Clear();
            beautiful.WriteText("Your server is open for business!", Color.Green);
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

            base.UserConnected(user);
        }

        private void User_Authenticated(CServerClient client) {
            CUser user = (CUser)client;

            // Make other clients aware of this new user.
            beautiful.UserJoined(user.Username, user.Rank);
            Broadcast(new msg_NewUser(user));
        }

        private void NetMessageReceived_Public(CServerClient client, CNetMessage message) {
            // For handling PUBLIC net messages. This includes things like chat messages.
        }

        private void User_Disconnected(CServerClient client, string reason) {
            CUser user = (CUser)client;
            
            // Tell all clients this one has disconnected.
            beautiful.UserLeft(user.Username, user.Rank);
            Broadcast(new msg_LostUser(user));
        }

        public void Broadcast(CRatMessage msg) {
            for(int i = 0; i < Clients.Count; i++) {
                Clients[i].SendNetMessage(msg);
            }
        }
    }
}
