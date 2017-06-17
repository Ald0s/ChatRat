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
    // Context for hosting a server.
    public class CHostingCluster : CInstrumentCluster {
        public CHostingCluster(CBasePanel _parent)
            :base(_parent, null) {
            ToolStripButton stop = new ToolStripButton();
            stop.Image = Resources.server_delete;
            stop.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            stop.Name = "btnStopServer";
            stop.Text = "Stop Server";
            stop.Click += ObjectClicked;
            this.AddControl(stop);
        }
    }
}
