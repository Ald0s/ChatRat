/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from server -> client loading them with info about the server.
This is the net message used on new users.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChatRat.Elements;
using ZapNetwork.Server;
using ChatRat.Network.Server;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_ServerInfo : CRatMessage {
        public msg_ServerInfo(List<CServerClient> _clients, List<CRoom> _rooms)
            : base("serverinfo") {
            List<CUser> users = new List<CUser>();
            for(int i = 0; i < _clients.Count; i++) {
                users.Add((CUser)_clients[i]);
            }

            WriteUsers(users.ToArray());
            WriteRooms(_rooms.ToArray());
        }
    }
}
