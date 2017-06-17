/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A small class that describes a user's ranking within the server.
This will be changed a lot and maybe even saved to the disk.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRat.Elements {
    // A class to represent a user's rank.
    public class CRank {
        public string Name { get { return this.sRankName; } }
        public Color Colour { get { return this.cRankColour; } }

        private string sRankName;
        private Color cRankColour;

        // Default rank here.
        private const string defaultRankName = "User";
        private Color defaultRankColour = Color.Green; 

        // Can pass NULL in to both, rank will be defaulted.
        public CRank(string _rank, Color _colour) {
            sRankName = (_rank == null) ? defaultRankName : _rank;
            cRankColour = (_colour == Color.Empty) ? defaultRankColour : _colour;
        }

        public void UpdateRank(string name, Color colour) {
            this.sRankName = name;
            this.cRankColour = colour;
        }

        public void WipeRank() {
            this.sRankName = defaultRankName;
            this.cRankColour = defaultRankColour;
        }
    }
}
