/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message base for ChatRat specifically.
Provides some chat related network functionality, specifically macros for reading/writing users and other
similar objects.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZapNetwork.Shared;
using ChatRat.Elements;
using ChatRat.Network.Server;

namespace ChatRat.Network.Messages {
    // Custom type for CNetMessage.
    // Exposes some additional functionality.

    [Serializable]
    public class CRatMessage : CNetMessage {
        public CRatMessage(string _name)
            : base(_name) {

        }

        #region User
        public void WriteUser(CUser user) {
            WriteInt(user.ClientID);
            WriteString(user.Username);
            WriteRank(user.Rank);
        }

        public COfflineUser ReadUser() {
            return new COfflineUser(
                ReadInt(),
                ReadString(),
                ReadRank());
        }
        #endregion

        #region Rank
        public void WriteRank(CRank rank) {
            WriteString(rank.Name);
            WriteColour(rank.Colour);
        }

        public CRank ReadRank() {
            return new CRank(
                ReadString(), 
                ReadColour());
        }
        #endregion
    }
}
