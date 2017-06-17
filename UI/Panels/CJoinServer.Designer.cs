namespace ChatRat.UI.Panels {
    partial class CJoinServer {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtUsername = new ChatRat.UI.Controls.CUserInput();
            this.txtIPAddress = new ChatRat.UI.Controls.CUserInput();
            this.txtListenPort = new ChatRat.UI.Controls.CUserInput();
            this.txtPassword = new ChatRat.UI.Controls.CUserInput();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Join a Server";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 524F));
            this.tableLayoutPanel1.Controls.Add(this.txtPassword, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtUsername, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtIPAddress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtListenPort, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(524, 154);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.ForeColor = System.Drawing.Color.Black;
            this.txtUsername.Location = new System.Drawing.Point(3, 3);
            this.txtUsername.MaxLength = 30;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Placeholder = "Username";
            this.txtUsername.Size = new System.Drawing.Size(188, 23);
            this.txtUsername.TabIndex = 4;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.ForeColor = System.Drawing.Color.Black;
            this.txtIPAddress.Location = new System.Drawing.Point(3, 33);
            this.txtIPAddress.MaxLength = 30;
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Placeholder = "Server IP Address";
            this.txtIPAddress.Size = new System.Drawing.Size(143, 23);
            this.txtIPAddress.TabIndex = 5;
            // 
            // txtListenPort
            // 
            this.txtListenPort.ForeColor = System.Drawing.Color.Black;
            this.txtListenPort.Location = new System.Drawing.Point(3, 63);
            this.txtListenPort.MaxLength = 30;
            this.txtListenPort.Name = "txtListenPort";
            this.txtListenPort.Placeholder = "Server IP Address";
            this.txtListenPort.Size = new System.Drawing.Size(46, 23);
            this.txtListenPort.TabIndex = 6;
            this.txtListenPort.Text = "9988";
            // 
            // txtPassword
            // 
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(3, 93);
            this.txtPassword.MaxLength = 30;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Placeholder = "Password (if required)";
            this.txtPassword.Size = new System.Drawing.Size(188, 23);
            this.txtPassword.TabIndex = 7;
            // 
            // CJoinServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Name = "CJoinServer";
            this.Size = new System.Drawing.Size(530, 176);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.CUserInput txtUsername;
        private Controls.CUserInput txtIPAddress;
        private Controls.CUserInput txtListenPort;
        private Controls.CUserInput txtPassword;
    }
}
