/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
An offline mirror of the CUser class.
Check below for a very cool summary.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRat.Elements {
    // Stored by all clients connected to a server.
    // There are better ways to do this; one way would be to have a class that has an internal event handler,
    // that listens in on all net messages received from the server, and updates itself according to any messages, thus updating information
    // internally.

    public class COfflineUser {
        public int ClientID { get { return this.iClientID; } }
        public string Username { get { return this.sUsername; } }
        public CRank Rank { get { return this.rRank; } }

        private int iClientID;
        private string sUsername;
        private CRank rRank;

        public COfflineUser(int _id, string _user, CRank _rank) {
            this.iClientID = _id;
            this.sUsername = _user;
            this.rRank = _rank;
        }

        public void UpdateUsername(string _user) {
            this.sUsername = _user;
        }

        public void UpdateRank(CRank _rank) {
            this.rRank = _rank;
        }
    }
}
