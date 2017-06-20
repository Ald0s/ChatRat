/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A bunch of net messages concerning rooms and their interaction.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatRat.Elements;
using ChatRat.Network.Server;

namespace ChatRat.Network.Messages {
    // A class for when someone else leaves/joins the room you're in.
    [Serializable]
    public class msg_MoveRoom : CRatMessage {
        /*
        boolean us - is this net message aimed at US
        boolean join - is the following information related to a join/leave scenario
        user         - who, if not toward us, is this message aimed?
        room         - old room
        room         - new room
        */
        
        public msg_MoveRoom(bool _us, bool _join, CUser _user, CRoom _old, CRoom _new)
            : base("moveroom") {
            WriteBool(_us);
            WriteBool(_join);
            
            if(!_us) {
               WriteUser(_user);
            }
            
            WriteRoom(_old);
            WriteRoom(_new);
        }
    }
}
