/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ChatRat.UI;
using ChatRat.UI.Panels;
using ChatRat.Elements;
using ChatRat.UI.InstrumentClusters;
using ChatRat.Network.Server;
using ChatRat.Network.Client;
using ChatRat.Properties;

namespace ChatRat {
    public partial class FMain : Form {
        private CUISwitcher primary;
        private CBeautifulText beautiful;
        private CToolStripController control;

        // A list for holding rooms.
        private List<CRoom> rooms;

        // A bunch of instrument clusters.
        // In other words, buttons and other controls for the context strip.
        private CPrimary primaryCluster;
        
        // The main panel. 
        // Text output and input controls.
        private CMainPanel main;
        private CViewUsers viewUsers;

        // Create Server panel.
        private CCreateServer createServer;
        private CCreateServerInstruments createServerCluster;

        // Join Server panel
        private CJoinServer joinServer;
        private CJoinServerInstruments joinServerCluster;

        // Connected clusters.
        // All use main panel by default.
        private CHostingCluster hostingCluster;
        private CConnectedCluster connectedCluster;
        private CViewUsersCluster viewUsersCluster;

        // Network stuff.
        private CServer server;
        private CClient client;

        // For going back.
        private CBasePanel previousPanel;
        private CInstrumentCluster previousContext;

        public FMain() {
            InitializeComponent();
            this.FormClosing += FMain_FormClosing;
            this.rooms = new List<CRoom>();

            // This will act as a context tool strip.
            // Changing to suit the needs of 'panMain'
            this.control = new CToolStripController(tsPrimary);
            this.primary = new CUISwitcher(panMain);

            // Initialise all panels and register to UI parent.
            this.main = new CMainPanel(panMain, control);
            main.ButtonClicked += Main_ButtonClicked;
            main.InputEntered += Main_InputEntered;
            primary.RegisterElement(main);

            this.viewUsers = new CViewUsers(panMain);
            primary.RegisterElement(viewUsers);

            this.createServer = new CCreateServer(panMain);
            createServer.StartServer += CreateServer_StartServer;
            createServer.Cancel += CreateServer_Cancel;
            primary.RegisterElement(createServer);

            this.joinServer = new CJoinServer(panMain);
            joinServer.JoinServer += JoinServer_JoinServer;
            joinServer.Cancel += JoinServer_Cancel;
            primary.RegisterElement(joinServer);

            // Create instruments.
            CreateInstrumentClusters();

            // Initialise the primary text output.
            main.InitialiseChatbox(ref beautiful);

            // Setup basic network classes.
            server = new CServer(null, beautiful);
            server.UIUpdate += UpdateUI;
            server.RoomAdded += RoomAdded;
            server.RoomRemoved += RoomRemoved;

            client = new CClient(null, beautiful);
            client.UIUpdate += UpdateUI;
            client.RoomAdded += RoomAdded;
            client.RoomRemoved += RoomRemoved;
            client.Disconnected += Client_Disconnected;

            // Select the default one.
            DefaultUI();
        }

        private void Main_InputEntered(string input, DateTime time) {
            // Handling an 'enter' press on the primary text input.
            // Send the input to both the server and client.
            ClientState_e state = GetState();

            if(state == ClientState_e.Server)
                server.HandleInput(input, time);
            else if(state == ClientState_e.Client)
                client.HandleInput(input, time);
        }

        private void RoomButton_Clicked(object sender, EventArgs e) {
            // Handling a room change.
            // Send input to both the server and client.
            ClientState_e state = GetState();

            if (state == ClientState_e.Server)
                server.ChangeRoom((CRoom)((ToolStripMenuItem)sender).Tag);
            else if (state == ClientState_e.Client)
                client.ChangeRoom((CRoom)((ToolStripMenuItem)sender).Tag);
        }

        private void DisplayAvailableRooms(ToolStripDropDownButton btnRooms) {
            btnRooms.DropDownItems.Clear();

            for(int i = 0; i < rooms.Count; i++) {
                ToolStripMenuItem btn = new ToolStripMenuItem();
                btn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btn.Image = Resources.world;
                btn.Text = rooms[i].DisplayName;
                btn.Tag = rooms[i];
                btn.Click += RoomButton_Clicked;

                btnRooms.DropDownItems.Add(btn);
            }
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e) {
            // Should the user not properly disconnect/shutdown the server before closing
            // the form. Which happens more often than you'd think. -.-

            if (client != null && client.Connected)
                client.Shutdown("Disconnect by user.");

            if (server != null && server.Listening)
                server.ShutdownServer();
        }

        private void FMain_Load(object sender, EventArgs e) {
            beautiful.WriteTexts(
                new string[] { "Welcome to ", "ChatRat", "!" },
                new Color[] { Color.DimGray, Color.Orange, Color.DimGray });
        }

        private void DefaultUI() {
            primary.SelectUI(main.PanelName);
            control.SelectCluster(primaryCluster);
        }

        private void Back() {
            if (previousPanel != null)
                primary.SelectUI(previousPanel.PanelName);

            if (previousContext != null)
                control.SelectCluster(previousContext);
        }

        private void CreateInstrumentClusters() {
            primaryCluster = new CPrimary(main);
            control.RegisterCluster(primaryCluster);

            createServerCluster = new CCreateServerInstruments(createServer);
            control.RegisterCluster(createServerCluster);

            joinServerCluster = new CJoinServerInstruments(joinServer);
            control.RegisterCluster(joinServerCluster);

            hostingCluster = new CHostingCluster(main);
            control.RegisterCluster(hostingCluster);

            connectedCluster = new CConnectedCluster(main);
            control.RegisterCluster(connectedCluster);

            viewUsersCluster = new CViewUsersCluster(main);
            control.RegisterCluster(viewUsersCluster);
        }
        
        // Handles clicks on context items from the main panel.
        private void Main_ButtonClicked(string name, object[] args) {
            switch (name) {
                // Server Stuff.
                case "btnCreateServer":
                    // Display create server panel.
                    primary.SelectUI(createServer.PanelName);
                    control.SelectCluster(createServerCluster);
                    break;

                case "btnStopServer":
                    server.ShutdownServer();
                    DefaultUI();
                    break;
                // ===

                // Client stuff.
                case "btnJoinServer":
                    // Display join server panel.
                    primary.SelectUI(joinServer.PanelName);
                    control.SelectCluster(joinServerCluster);
                    break;

                case "btnDisconnect":
                    client.Shutdown("Disconnect by user.");
                    break;
                // ===

                // Both stuff.
                case "btnChangeRoom":
                    DisplayAvailableRooms((ToolStripDropDownButton)args[0]);
                    break;

                case "btnViewUsers":
                    ShowConnectedUsers();
                    break;

                case "btnViewChat":
                    Back();
                    break;
                // ===
            }
        }

        // Change to connected users context.
        // For hosting, this will also allow administrative options.
        private void ShowConnectedUsers() {
            this.previousPanel = primary.SelectedPanel;
            this.previousContext = control.SelectedCluster;

            viewUsers.Update(server, client, GetLocalRoom(), GetState());

            primary.SelectUI(viewUsers.PanelName);
            control.SelectCluster(viewUsersCluster);
        }

        // Creating your own server.
        private void CreateServer_StartServer() {
            string error = null;

            // Let's start!
            // Interact with net here, set user interface back to primary and select hosting context cluster.
            if ((error = createServer.SupplyInformation(ref server)) != null) {
                beautiful.WriteText(error, Color.Red);
                DefaultUI();

                return;
            }

            primary.SelectUI(main.PanelName);
            control.SelectCluster(hostingCluster);
        }

        private void CreateServer_Cancel() {
            // Nope! Cancel!!!
            DefaultUI();
        }

        // Joining another user's server.
        private void JoinServer_JoinServer() {
            string error = null;

            if ((error = joinServer.SupplyInformation(ref client)) != null) {
                beautiful.WriteText(error, Color.Red);
                DefaultUI();

                return;
            }

            primary.SelectUI(main.PanelName);
            control.SelectCluster(connectedCluster);
        }

        private void JoinServer_Cancel() {
            DefaultUI();
        }

        private void Client_Disconnected(string reason) {
            DefaultUI();
        }

        private void UpdateUI() {
            // Update the view users interface.
            viewUsers.Update(server, client, GetLocalRoom(), GetState());
        }

        // A macro for getting the room we're in.
        // From both server OR client.
        private CRoom GetLocalRoom() {
            ClientState_e state = GetState();

            if (state == ClientState_e.Server)
                return server.GetLocalhost().Room;
            else if (state == ClientState_e.Client)
                return client.Room;

            return null;
        }

        private ClientState_e GetState() {
            if (server != null && server.Listening)
                return ClientState_e.Server;
            else if (client != null && client.Connected)
                return ClientState_e.Client;
            else
                return ClientState_e.None;
        }

        private void RoomAdded(CRoom room) {
            if (GetRoom(room.Name) != -1)
                return;

            rooms.Add(room);
        }

        private void RoomRemoved(CRoom room) {
            int idx = -1;
            if ((idx = GetRoom(room.Name)) == -1)
                return;

            CRoom target = rooms[idx];
            if (target.Locked)
                return;

            rooms.RemoveAt(idx);
        }

        private int GetRoom(string name) {
            for(int i = 0; i < rooms.Count; i++) {
                if (rooms[i].Name == name)
                    return i;
            }
            return -1;
        }
    }

    public enum ClientState_e {
        Server,
        Client,
        None
    }
}
