/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from server -> clients signaling that another user has moved rooms.
*/

using ChatRat.Elements;
using ChatRat.Network.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_JoinLeave : CRatMessage {
        public msg_JoinLeave(CUser target, CRoom subject, bool join_or_leave)
            :base("joinleave") {
            WriteUser(target);
            WriteRoom(subject);
            WriteBool(join_or_leave);
        }
    }
}
