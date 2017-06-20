/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A custom UI switcher class working with panels.
This object coordinates the switching of custom user controls in a Forms panel.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChatRat.Elements;
using System.Windows.Forms;

namespace ChatRat.UI {
    public class CUISwitcher {
        public CBasePanel ChosenPanel { get { return this.selectedUI; } }
        public CBasePanel SelectedPanel { get { return this.selectedUI; } }

        private Panel pParent;
        private List<CBasePanel> lControls;

        private CBasePanel selectedUI;

        public CUISwitcher(Panel _parent) {
            this.pParent = _parent;
            lControls = new List<CBasePanel>();
        }

        public void RegisterElement(CBasePanel panel) {
            if (lControls.Contains(panel))
                return;
            lControls.Add(panel);
        }

        public void SelectUI(string _name) {
            for (int i = 0; i < lControls.Count; i++) {
                if (lControls[i].PanelName == _name) {
                    if (selectedUI != null && selectedUI.PanelName == _name)
                        return;

                    SwitchUIInternal(lControls[i]);
                    return;
                }
            }

            Console.WriteLine("UI Selected: " + _name);
        }

        public void LinkUIElement(Button btnInvoker, CBasePanel ucPanel) {
            ucPanel.Tag = btnInvoker;
            btnInvoker.Tag = ucPanel;

            btnInvoker.Click += HandleClick;
            lControls.Add(ucPanel);
        }

        public void UnlinkUIElement(Button btnInvoker) {
            btnInvoker.Click -= HandleClick;
        }

        private void HandleClick(object sender, EventArgs e) {
            string s = sender.GetType().Name;
            if (sender == null || sender.GetType() != typeof(Button))
                return;

            object tag = ((Button)sender).Tag;
            if (tag == null)
                return;

            SwitchUIInternal((CBasePanel)tag);
        }

        private void SwitchUIInternal(CBasePanel ucPanel) {
            if (selectedUI != null) {
                SetUIActive(false);
                pParent.Controls.Clear();
            }

            selectedUI = ucPanel;
            SetUIActive(true);

            pParent.Controls.Add(selectedUI);
        }

        private void SetUIActive(bool active) {
            selectedUI.Visible = active;
            selectedUI.Enabled = active;
            selectedUI.BringToFront();

            if (active) {
                selectedUI.Show();
            } else {
                selectedUI.Hide();
            }
        }
    }
}
