using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ChatRat.Elements;
using ChatRat.Network.Server;
using ChatRat.Network.Client;

namespace ChatRat.UI.Panels {
    public partial class CViewUsers : CBasePanel {
        private ClientState_e state;

        private CRoom room;

        private CServer server;
        private CClient client;

        private int currentOption = 0;

        public CViewUsers(Panel _parent)
            : base(_parent, "viewUsers") {
            InitializeComponent();

            this.comboType.Items.Add("All Users");
            this.comboType.SelectedIndexChanged += ComboType_SelectedIndexChanged;
        }

        public void Update(CServer _server, CClient _client, CRoom _room, ClientState_e state) {
            this.client = _client;
            this.server = _server;
            this.state = state;
            this.room = _room;

            // Trick an update instead of writing separate logic to differentiate between the options.
            comboType.SelectedIndex = 0;

            SelectOption();
        }

        private void LoadAllUsers() {
            lstUsers.Items.Clear();

            if (state == ClientState_e.Server) {
                for (int i = 0; i < server.Clients.Count; i++) {
                    CUser user = (CUser)server.Clients[i];
                    AddUser(user);
                }
            } else if (state == ClientState_e.Client) {
                for (int i = 0; i < client.Users.Count; i++) {
                    COfflineUser user = client.Users[i];
                    AddUser(user);
                }
            }
        }

        private void LoadCurrentRoomUsers() {
            lstUsers.Items.Clear();

            if (state == ClientState_e.Server) {
                for (int i = 0; i < server.Clients.Count; i++) {
                    CUser user = (CUser)server.Clients[i];
                    if (user.Room.Name != room.Name)
                        continue;

                    AddUser(user);
                }
            } else if (state == ClientState_e.Client) {
                for (int i = 0; i < client.Users.Count; i++) {
                    COfflineUser user = client.Users[i];
                    if (user.Room.Name != room.Name)
                        continue;

                    AddUser(user);
                }
            }
        }

        private void AddUser(COfflineUser user) {
            ListViewItem j = new ListViewItem();
            j.Text = user.Username;
            j.SubItems.Add(user.Rank.Name);
            j.SubItems.Add(user.Room.DisplayName);
            j.SubItems[0].ForeColor = user.Rank.Colour;
            j.Tag = user;

            lstUsers.Items.Add(j);
        }

        private void AddUser(CUser user) {
            ListViewItem j = new ListViewItem();
            j.Text = user.Username;
            j.SubItems.Add(user.Rank.Name);
            j.SubItems.Add(user.Room.DisplayName);
            j.SubItems[0].ForeColor = user.Rank.Colour;
            j.Tag = user;

            lstUsers.Items.Add(j);
        }

        private void ComboType_SelectedIndexChanged(object sender, EventArgs e) {
            SelectOption();
        }

        private void SelectOption() {
            SelectionType_e type = (SelectionType_e)comboType.SelectedIndex;
            currentOption = (int)type;

            switch (type) {
                case SelectionType_e.All:
                    LoadAllUsers();
                    break;

                case SelectionType_e.CurrentRoom:
                    LoadCurrentRoomUsers();
                    break;
            }
        }

        public enum SelectionType_e {
            All = 0,
            CurrentRoom
        }
    }
}
