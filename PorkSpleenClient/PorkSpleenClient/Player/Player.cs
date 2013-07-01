using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorkSpleenClient
{
    class Player
    {
        Collection collection;
        Deck deck;
        int userId;
        string name;
        int contenent, region, screen;

        Avatar avatar;
        OverWorldControl _overWorld;

        public Player(OverWorldControl overWorld)
        {
            _overWorld = overWorld;
            avatar = new Avatar();
        }

        internal void FromString(string[] splitMessage)
        {
            userId = int.Parse(splitMessage[1]);
            name = splitMessage[2];
            contenent = int.Parse(splitMessage[3]);
            region = int.Parse(splitMessage[4]);
            screen = int.Parse(splitMessage[5]);
            avatar.PosX = int.Parse(splitMessage[6]);
            avatar.PosY = int.Parse(splitMessage[7]);
            _overWorld.AddAvatar(this.avatar);
        }

        internal void MoveAvatar(char p)
        {
            switch (p)
            {
                case 'w':
                    avatar.PosY -= 1;
                    if (avatar.PosY<0)
                    {
                        avatar.PosY = 7;
                    }
                    break;
                case 's':
                    avatar.PosY += 1;
                    if (avatar.PosY>7)
                    {
                        avatar.PosY = 0;
                    }
                    break;
                case 'a':
                    avatar.PosX -= 1;
                    if (avatar.PosX<0)
                    {
                        avatar.PosX = 13;
                    }
                    break;
                case 'd':
                    avatar.PosX += 1;
                    if (avatar.PosX>13)
                    {
                        avatar.PosX = 0;
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
