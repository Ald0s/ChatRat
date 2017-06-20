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

namespace ChatRat {
    public partial class FMain : Form {
        private CUISwitcher primary;
        private CBeautifulText beautiful;
        private CToolStripController control;

        // A bunch of instrument clusters.
        // In other words, buttons and other controls for the context strip.
        private CPrimary primaryCluster;
        
        // The main panel. 
        // Text output and input controls.
        private CMainPanel main;

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

        // Network stuff.
        private CServer server;
        private CClient client;

        public FMain() {
            InitializeComponent();
            this.FormClosing += FMain_FormClosing;

            // This will act as a context tool strip.
            // Changing to suit the needs of 'panMain'
            this.control = new CToolStripController(tsPrimary);
            this.primary = new CUISwitcher(panMain);

            // Initialise all panels and register to UI parent.
            this.main = new CMainPanel(panMain, control);
            main.ButtonClicked += Main_ButtonClicked;
            main.InputEntered += Main_InputEntered;
            primary.RegisterElement(main);

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
            client = new CClient(null, beautiful);
            client.Disconnected += Client_Disconnected;

            // Select the default one.
            DefaultUI();
        }

        private void Main_InputEntered(string input, DateTime time) {
            // Handling an 'enter' press on the primary text input.
            // Send the input to both the server and client.

            if(server != null && server.Listening) {
                server.HandleInput(input, time);
                return;
            }

            if(client != null && client.Connected) {
                client.HandleInput(input, time);
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
            }
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
    }
}
