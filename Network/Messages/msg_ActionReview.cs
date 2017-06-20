/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A net message from server -> client summarising the result of a request.
This could be an admin command.
*/

using ChatRat.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChatRat.Network.Messages {
    [Serializable]
    public class msg_ActionReview : CRatMessage {
        public msg_ActionReview(string _text, Color _color)
            : base("actionreview") {
            WriteString(_text);
            WriteColour(_color);
        }

        public void Process(CBeautifulText output) {
            output.WriteText(
                ReadString(),
                ReadColour());
        }
    }
}
