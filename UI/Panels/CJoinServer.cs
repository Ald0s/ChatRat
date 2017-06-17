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
using ChatRat.Network.Client;
using System.Net;
using ZapNetwork;

namespace ChatRat.UI.Panels {
    public partial class CJoinServer : CBasePanel {
        public delegate void JoinServer_Delegate();
        public event JoinServer_Delegate JoinServer;

        public delegate void Cancel_Delegate();
        public event Cancel_Delegate Cancel;

        public CJoinServer(Panel _parent)
            : base(_parent, "joinServer") {
            InitializeComponent();

            txtUsername.SetPlaceholder("Username");
            txtIPAddress.SetPlaceholder("Server IP Address");

            this.EventFiredInternal += CJoinServer_EventFiredInternal;
        }

        private void CJoinServer_EventFiredInternal(string name, object[] args) {
            switch (name) {
                case "btnJoinServer":
                    if (JoinServer != null)
                        JoinServer();
                    break;

                case "btnCancel":
                    if (Cancel != null)
                        Cancel();
                    break;
            }
        }

        public string SupplyInformation(ref CClient client) {
            if (!txtUsername.ContainsValidText())
                return "Please enter a valid username!";

            if (!txtIPAddress.ContainsValidText())
                return "Please enter a valid IP Address!";

            int iListenPort = 0;
            if(!int.TryParse(txtListenPort.Text, out iListenPort)) {
                return "Listen port is invalid. Please make sure its a number between 1 and 65535";
            }

            IPAddress ipAddy = IPAddress.None;
            if(!IPAddress.TryParse(txtIPAddress.Text, out ipAddy)) {
                return "IP Address is invalid!";
            }

            ClientCfg cfg = new ClientCfg(ipAddy, iListenPort, txtPassword.Text);
            client.Connect(txtUsername.Text, cfg);

            return null;
;        }
    }
}
