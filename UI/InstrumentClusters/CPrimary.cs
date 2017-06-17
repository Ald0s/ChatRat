/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

Primary Instrument Cluster
Contains 'Create Server', 'Join Server', Settings
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
    public class CPrimary : CInstrumentCluster {
        public CPrimary(CBasePanel _parent)
            :base(_parent, null) {
            ToolStripButton create = new ToolStripButton();
            create.Image = Resources.server_add;
            create.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            create.Name = "btnCreateServer";
            create.Text = "Create Server";
            create.Click += ObjectClicked;
            this.AddControl(create);

            ToolStripButton join = new ToolStripButton();
            join.Image = Resources.connect;
            join.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            join.Name = "btnJoinServer";
            join.Text = "Join Server";
            join.Click += ObjectClicked;
            this.AddControl(join);

            ToolStripButton settings = new ToolStripButton();
            settings.Image = Resources.wrench;
            settings.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            settings.Name = "btnSettings";
            settings.Text = "Settings";
            settings.Click += ObjectClicked;
            //this.AddControl(settings);
        }
    }
}
