/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
ChatRat child class of CServerClient.
This class extends the inbuilt server client to expose chatrat related information,
such as the user's in-server name and their rank. It'll also describe things such as;
is the user muted? Which chat room is the user in?
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using ZapNetwork.Server;
using ChatRat.Elements;
using ZapNetwork.Shared;
using ZapNetwork;
using ChatRat.Network.Messages;

namespace ChatRat.Network.Server {
    public class CUser : CServerClient {
        public string Username { get { return this.sUsername; } }
        public CRank Rank { get { return this.rank; } }
        public CRoom Room { get { return this.room; } }

        private CRank rank;
        private CRoom room;
        private string sUsername;

        public CUser(CServerMain _main, TcpClient _client)
            :base(_main, _client) {
            if(_main == null && _client == null) {
                // Verify we never really use main in a localhost class.
                // It should only be used for disconnections - which by definition doesn't apply to localhost.

                // Passing NULL to parent should signal a localhost relationship. 
                // This means no outward communication will be employed or allowed.

                this.rank = new CRank("Owner", Color.Purple, true); // Set to OWNER. This can't be changed.
            } else {
                this.rank = new CRank(null, Color.Empty); // Default to NULL right now. Which means default rank (user - green)
            }
            
            this.NetMessageReceived += CUser_NetMessageReceived;
        }

        public void BeginLocalhost(string username) {
            CNetMessage setup = new CNetMessage("setup");
            setup.WriteString(username);

            // Force an authentication for localhost.
            HandleNetMessageInternal(setup);
        }

        public void UpdateRoom(CRoom room) {
            this.room = room;
        }
        
        // Information we want to SEND the client.
        // In this case, rank information is number 1!
        protected override void SendSetupInfo(CNetMessage setup) {
            setup.WriteString(rank.Name);
            setup.WriteColour(rank.Colour);

            base.SendSetupInfo(setup);
        }

        // For initial data RECEIVED from the client.
        protected override void SetupReceived(CNetMessage setup) {
            sUsername = setup.ReadString();

            base.SetupReceived(setup);
        }

        private void CUser_NetMessageReceived(CServerClient client, ZapNetwork.Shared.CNetMessage message) {
            switch (message.GetMessageName()) {

            }
        }
    }
}
