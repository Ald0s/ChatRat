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
    public class CJoinServerInstruments : CInstrumentCluster {
        public CJoinServerInstruments(CBasePanel _parent)
            :base(_parent, null) {
            ToolStripButton join = new ToolStripButton();
            join.Image = Resources.connect;
            join.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            join.Name = "btnJoinServer";
            join.Text = "Join Server";
            join.Click += ObjectClicked;
            this.AddControl(join);

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
