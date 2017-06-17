/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A netmessage from server -> client to let them know a user as left the server.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatRat.Elements;
using ChatRat.Network.Server;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_LostUser : CRatMessage {
        public msg_LostUser(CUser _user)
            : base("lostuser") {
            WriteUser(_user);
        }

        public COfflineUser GetUser() {
            return ReadUser();
        }
    }
}
