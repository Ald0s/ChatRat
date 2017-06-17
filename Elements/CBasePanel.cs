/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
Our most base type of panel.
This exposes an internal event within the panel that will be called by
the context tool strip when controls are clicked.
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

namespace ChatRat.Elements {
    public partial class CBasePanel : UserControl {
        public string PanelName { get { return this.sPanelName; } }
        private string sPanelName = null;

        private Panel parent;

        protected delegate void EventFiredInternal_Delegate(string name, object[] args);
        protected event EventFiredInternal_Delegate EventFiredInternal;

        public CBasePanel(Panel _parent, string _name) {
            InitializeComponent();

            this.sPanelName = _name;
            if (_parent == null)
                return;

            this.Size = _parent.Size;
            this.parent = _parent;
        }

        public CBasePanel() {
            InitializeComponent();
        }

        // Never ever call this. There's no need to.
        public void EventFired_Internal(string name, object[] args) {
            // Call the internal event handler.
            if (EventFiredInternal != null)
                EventFiredInternal(name, args);
        }
    }
}
