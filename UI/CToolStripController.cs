/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s
*/

using ChatRat.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRat.UI {
    public class CToolStripController {
        public CInstrumentCluster SelectedCluster { get { return this.selectedCluster; } }

        private ToolStrip container;
        private List<CInstrumentCluster> clusters;

        private CInstrumentCluster selectedCluster;

        public CToolStripController(ToolStrip _target) {
            this.container = _target;
            this.clusters = new List<CInstrumentCluster>();
        }

        public void RegisterCluster(CInstrumentCluster cluster) {
            if (clusters.Contains(cluster))
                return;

            clusters.Add(cluster);
        }

        public void SelectCluster(CInstrumentCluster cluster) {
            if(!clusters.Contains(cluster)) {
                RegisterCluster(cluster);
            }

            selectedCluster = cluster;
            container.Items.Clear();
            cluster.Update(container);
        }

        public void RemoveCluster(CInstrumentCluster cluster) {
            clusters.Remove(cluster);
        }
    }

    public class CInstrumentCluster {
        private CBasePanel parent;
        private List<ToolStripItem> controls;

        public CInstrumentCluster(CBasePanel _parent, ToolStripItem[] _def) {
            this.parent = _parent; // Establish a relationship with the cluster's friend panel.
            controls = new List<ToolStripItem>();

            if (_def != null)
                controls.AddRange(_def);
        }

        public void Update(ToolStrip container) {
            for(int i = 0; i < controls.Count; i++) {
                container.Items.Add(controls[i]);
            }
        }

        public void AddControl(ToolStripItem control) {
            if (controls.Contains(control))
                return;

            controls.Add(control);
        }

        public void RemoveControl(ToolStripItem control) {
            controls.Remove(control);
        }

        protected void FireEvent_Internal(string name, object[] args) {
            parent.EventFired_Internal(name, args);
        }

        protected void ObjectClicked(object sender, EventArgs e) {
            this.FireEvent_Internal(((ToolStripItem)sender).Name, new object[] { sender, e });
        }
    }
}
