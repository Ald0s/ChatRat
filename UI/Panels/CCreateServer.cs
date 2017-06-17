/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ChatRat.Elements;
using ChatRat.Network.Server;
using ZapNetwork;

namespace ChatRat.UI.Panels {
    public partial class CCreateServer : CBasePanel {
        public delegate void StartServer_Delegate();
        public event StartServer_Delegate StartServer;

        public delegate void Cancel_Delegate();
        public event Cancel_Delegate Cancel;

        public CCreateServer(Panel _parent) 
            :base(_parent, "createServer") {
            InitializeComponent();

            txtUsername.SetPlaceholder("Username");
            txtServerName.SetPlaceholder("Server Name");
            this.EventFiredInternal += CCreateServer_EventFiredInternal;
        }

        private void CCreateServer_EventFiredInternal(string name, object[] args) {
            switch (name) {
                case "btnStartServer":
                    if (StartServer != null)
                        StartServer();
                    break;

                case "btnCancel":
                    if (Cancel != null)
                        Cancel();
                    break;
            }
        }

        public string SupplyInformation(ref CServer server) {
            // Does something with the information entered by the user. Returns not null if there was an error.
            // Error checks go HERE.
            if (!txtUsername.ContainsValidText())
                return "Please enter a valid username!";

            if (!txtServerName.ContainsValidText())
                return "Please enter a valid server name!";

            int iListenPort = 0,
                iMaxUsers = 0;
            if(!int.TryParse(txtListenPort.Text, out iListenPort) || !int.TryParse(txtMaxUsers.Text, out iMaxUsers))
                return "Invalid listen port or max users! Please make sure both of these are numbers! (Port must be within 1 and 65535)";

            ServerCfg cfg = new ServerCfg(txtServerName.Text, txtDescription.Text, (checkUsePassword.Checked) ? txtPassword.Text : null,iListenPort, 677, iMaxUsers);
            server.StartServer(cfg);

            return null;
        }
    }
}
