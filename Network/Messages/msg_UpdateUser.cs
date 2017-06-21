/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from server -> clients updating information.
This is broadcasted but modified information can relate to specific parts of the client, hidden from all.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChatRat.Network.Server;
using ChatRat.Elements;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_UpdateUser : CRatMessage {
        public msg_UpdateUser(CUser user, int sw, CRoom _room, CRank _rank)
            :base("updateuser") {
            WriteInt(sw);
            WriteUser(user);
            
            if (_room != null)
                WriteRoom(_room);

            if (_rank != null)
                WriteRank(_rank);
        }
    }
}
