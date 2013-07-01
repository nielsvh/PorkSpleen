using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorkSpleenClient
{
    class OWScreen
    {
        OWScreen north, south, east, west;

        internal OWScreen West
        {
            get { return west; }
            set { west = value; }
        }

        internal OWScreen East
        {
            get { return east; }
            set { east = value; }
        }

        internal OWScreen South
        {
            get { return south; }
            set { south = value; }
        }

        internal OWScreen North
        {
            get { return north; }
            set { north = value; }
        }
        char[,] tiles;

        public char[,] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        public OWScreen(string tiles)
        {
            this.tiles = new char[14, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    this.tiles[j, i] = tiles[i*14+j];
                }
            }
        }

    }
}
