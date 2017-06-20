using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ChatRat.Properties;
using ChatRat.Elements;

namespace ChatRat.UI.InstrumentClusters {
    public class CViewUsersCluster : CInstrumentCluster {
        public CViewUsersCluster(CBasePanel _parent)
            :base(_parent, null) {
            ToolStripButton back = new ToolStripButton();
            back.Image = Resources.comment;
            back.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            back.Name = "btnViewChat";
            back.Text = "Back to Chat";
            back.Click += ObjectClicked;
            this.AddControl(back);
        }
    }
}
