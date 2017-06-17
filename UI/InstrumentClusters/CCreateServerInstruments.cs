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

using ChatRat.Elements;
using System.Windows.Forms;
using ChatRat.Properties;

namespace ChatRat.UI.InstrumentClusters {
    public class CCreateServerInstruments : CInstrumentCluster {
        public CCreateServerInstruments(CBasePanel _parent)
            :base(_parent, null) {
            ToolStripButton start = new ToolStripButton();
            start.Image = Resources.server_add;
            start.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            start.Name = "btnStartServer";
            start.Text = "Start Server";
            start.Click += ObjectClicked;
            this.AddControl(start);

            ToolStripButton cancel = new ToolStripButton();
            cancel.Image = Resources.cancel;
            cancel.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            cancel.Name = "btnCancel";
            cancel.Text = "Cancel";
            cancel.Click += ObjectClicked;
            this.AddControl(cancel);
        }
    }
}
