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

using System.Drawing;
using System.Windows.Forms;

namespace ChatRat.UI.Controls {
    public class CUserInput : TextBox {
        public string Placeholder { get; set; }

        private Color colOld = Color.Empty;
        private int iNumTyped = 0;

        public CUserInput() {
            this.KeyPress += CUserInput_KeyPress;

            this.Enter += CUserInput_Enter;
            this.Leave += CUserInput_Leave;

            PlaceHolder();
        }

        private void CUserInput_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)Keys.Back) {
                iNumTyped--;
                return;
            }

            iNumTyped++;
        }

        private void CUserInput_Enter(object sender, EventArgs e) {
            RemoveHolder();
        }

        private void CUserInput_Leave(object sender, EventArgs e) {
            PlaceHolder();
        }

        private void PlaceHolder() {
            if (Text.Length > 0)
                return;

            this.colOld = ForeColor;

            this.Text = Placeholder;
            this.ForeColor = Color.DimGray;
        }

        private void RemoveHolder() {
            if (iNumTyped > 0)
                return;

            this.Clear();
            this.ForeColor = (colOld == Color.Empty) ? Color.Black : colOld;
        }

        public void SetPlaceholder(string input) {
            this.Placeholder = input;
            PlaceHolder();
        }

        public bool ContainsValidText() {
            return iNumTyped > 0;
        }
    }
}
