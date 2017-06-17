/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using ChatRat.Elements;
using ChatRat.Properties;

namespace ChatRat.UI.InstrumentClusters {
    // Represents the context strip that'll provide users with in-server options.
    // Like view other players/administrate.
    public class CConnectedCluster : CInstrumentCluster {
        public CConnectedCluster(CBasePanel _parent)
            :base(_parent, null) {
            ToolStripButton leave = new ToolStripButton();
            leave.Image = Resources.disconnect;
            leave.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            leave.Name = "btnDisconnect";
            leave.Text = "Leave Server";
            leave.Click += ObjectClicked;
            this.AddControl(leave);
        }
    }
}
