/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from
server -> client
client -> server
that contains a chat message.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatRat.Network.Server;
using ChatRat.Elements;

namespace ChatRat.Network.Messages {
    // Net message for SENDING chat message.
    [Serializable]
    public class msg_CreateMessage : CRatMessage {
        // Constructor for client -> server.
        public msg_CreateMessage(string _msg, DateTime _time)
            : base("create_chatmsg") {
            WriteString(_msg);
            WriteDouble((double)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }
    }

    // The server's rebroadcasted net message.
    [Serializable]
    public class msg_SendMessage : CRatMessage {
        public msg_SendMessage(CUser _user, string _msg, double _time)
            : base("sent_chatmsg") {
            WriteUser(_user);
            WriteString(_msg);
            WriteDouble(_time);
        }
    }
}
