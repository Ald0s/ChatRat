/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from server -> client telling them a user has joined.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatRat.Network.Server;
using ChatRat.Elements;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_NewUser : CRatMessage {
        public msg_NewUser(CUser _user)
            : base("newuser") {
            WriteUser(_user);
        }

        public COfflineUser GetUser() {
            return ReadUser();
        }
    }
}
