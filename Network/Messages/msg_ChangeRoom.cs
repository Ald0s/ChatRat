/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from client -> signalling a room change.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChatRat.Elements;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_ChangeRoom : CRatMessage {
        public msg_ChangeRoom(CRoom room)
            :base("changeroom") {
            WriteRoom(room);
        }
    }
}
