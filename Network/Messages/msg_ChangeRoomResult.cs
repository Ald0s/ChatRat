/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from server -> client summarising the result of a change room request.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChatRat.Elements;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_ChangeRoomResult : CRatMessage {
        public msg_ChangeRoomResult(CRoom _room, bool _success)
            : base("changeroom_result") {
            WriteRoom(_room);
            WriteBool(_success);
        }
    }
}
