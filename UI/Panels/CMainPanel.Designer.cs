namespace ChatRat.UI.Panels {
    partial class CMainPanel {
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
            this.txtMainOutput = new System.Windows.Forms.RichTextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMainOutput
            // 
            this.txtMainOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMainOutput.Location = new System.Drawing.Point(6, 1);
            this.txtMainOutput.Name = "txtMainOutput";
            this.txtMainOutput.ReadOnly = true;
            this.txtMainOutput.Size = new System.Drawing.Size(524, 144);
            this.txtMainOutput.TabIndex = 0;
            this.txtMainOutput.TabStop = false;
            this.txtMainOutput.Text = "";
            this.txtMainOutput.Enter += new System.EventHandler(this.txtMainOutput_Enter);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(3, 150);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(524, 23);
            this.txtInput.TabIndex = 1;
            // 
            // CMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.txtMainOutput);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Name = "CMainPanel";
            this.Size = new System.Drawing.Size(530, 176);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtMainOutput;
        private System.Windows.Forms.TextBox txtInput;
    }
}
