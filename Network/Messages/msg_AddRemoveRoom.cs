/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A message from server -> client to signal a new room,
or a loss of one.
*/

using ChatRat.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_AddRemoveRoom : CRatMessage {
        public msg_AddRemoveRoom(CRoom subject, bool add_or_remove)
            : base("addremoveroom") {
            WriteRoom(subject);
            WriteBool(add_or_remove);
        }
    }
}
