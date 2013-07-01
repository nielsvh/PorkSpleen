using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorkSpleenClient
{
    public class Avatar
    {
        string name;
        int posX, posY;

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }
        public Avatar()
        {
            posX = posY = 0;
        }
    }
}
