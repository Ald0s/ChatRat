/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatRat.Elements;
using System.Windows.Forms;
using System.Drawing;

namespace ChatRat.UI.Panels {
    public partial class CMainPanel : CBasePanel {
        // Fired when internally, a button is clicked. This includes the context tool strip.
        public delegate void ButtonClicked_Delegate(string name, object[] args);
        public event ButtonClicked_Delegate ButtonClicked;

        private CToolStripController control;

        public CMainPanel(Panel _base, CToolStripController _control)
            : base(_base, "mainPanel") {
            InitializeComponent();

            this.EventFiredInternal += CMainPanel_EventFiredInternal;

            this.control = _control;
            txtInput.Focus();
        }

        public void InitialiseChatbox(ref CBeautifulText beautiful) {
            // Initialises the beautiful text class encapsulating the primary text output control,
            // but references the main form's instance for any other distant classes that also may want to interact via
            // primary beautiful text.
            
            if(beautiful == null) {
                beautiful = new CBeautifulText(txtMainOutput);
            }
        }

        private void CMainPanel_EventFiredInternal(string name, object[] args) {
            if (ButtonClicked != null)
                ButtonClicked(name, args);
        }

        private void txtMainOutput_Enter(object sender, EventArgs e) {
            // This is a visual-only control.
            // We never want focus to belong to this.

            txtInput.Focus();
        }
    }
}
