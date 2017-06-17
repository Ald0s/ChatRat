namespace ChatRat {
    partial class FMain {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panMain = new System.Windows.Forms.Panel();
            this.tsPrimary = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panMain.Location = new System.Drawing.Point(0, 28);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(530, 176);
            this.panMain.TabIndex = 0;
            // 
            // tsPrimary
            // 
            this.tsPrimary.Location = new System.Drawing.Point(0, 0);
            this.tsPrimary.Name = "tsPrimary";
            this.tsPrimary.Size = new System.Drawing.Size(530, 25);
            this.tsPrimary.TabIndex = 1;
            this.tsPrimary.Text = "toolStrip1";
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 204);
            this.Controls.Add(this.tsPrimary);
            this.Controls.Add(this.panMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.Text = "ChatRat";
            this.Load += new System.EventHandler(this.FMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.ToolStrip tsPrimary;
    }
}