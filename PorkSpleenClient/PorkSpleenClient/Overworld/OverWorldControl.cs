using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PorkSpleenClient
{
    public partial class OverWorldControl : UserControl
    {
        OWScreen screen;
        public enum TileTypes { UNPASSABLE, PASSABLE };
        Bitmap[] tiles;
        Bitmap player;
        List<Avatar> avatars;
        Form1 _mainForm;

        public OverWorldControl(Form1 mainForm)
        {
            _mainForm = mainForm;
            avatars = new List<Avatar>();
            InitializeComponent();
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].KeyPress += new KeyPressEventHandler(_mainForm.Form1_KeyPress);
            }
        }

        public void LoadContent()
        {
            tiles = new Bitmap[2];
            tiles[0] = new Bitmap(@"Assets/Tiles/unpassable.png");
            tiles[1] = new Bitmap(@"Assets/Tiles/dirt.png");
            player = new Bitmap(@"Assets/avatar.png");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            while (screen == null)
            {

            }
            Point drawLocation = Point.Empty;
            for (int i = 0; i < screen.Tiles.GetLength(1); i++)
            {
                for (int j = 0; j < screen.Tiles.GetLength(0); j++)
                {
                    drawLocation.Y = i * 64;
                    drawLocation.X = j * 64;
                    switch (screen.Tiles[j, i])
                    {
                        case 'p':
                            e.Graphics.DrawImage((Image)tiles[(int)TileTypes.PASSABLE], drawLocation);
                            break;
                        case 'u':
                            e.Graphics.DrawImage((Image)tiles[(int)TileTypes.UNPASSABLE], drawLocation);
                            break;
                        default:
                            break;
                    }
                }
            }
            for (int i = 0; i < avatars.Count; i++)
            {
                drawLocation = new Point(avatars[i].PosX * 64, avatars[i].PosY * 64);
                e.Graphics.DrawImage((Image)player, drawLocation);
            }
        }

        public void AddAvatar(Avatar toAdd)
        {
            avatars.Add(toAdd);
        }

        internal void FromString(string[] splitMessage)
        {
            List<OWScreen> tmpQ = new List<OWScreen>();
            for (int i = 2; i < splitMessage.Length - 1; i++)
            {
                tmpQ.Add(new OWScreen(splitMessage[i]));
            }

            // which screen we are on
            for (int i = 0; i < tmpQ.Count; i++)
            {
                // give that screen a row of the adjacency matrix
                string row = splitMessage[1].Substring(i * tmpQ.Count, tmpQ.Count);
                // step through the matrix row
                for (int j = 0; j < tmpQ.Count; j++)
                {
                    if (row[j] == 'n')
                    {
                        tmpQ[i].North = tmpQ[j];
                    }
                    else if (row[j] == 's')
                    {
                        tmpQ[i].South = tmpQ[j];
                    }
                    else if (row[j] == 'e')
                    {
                        tmpQ[i].East = tmpQ[j];
                    }
                    else if (row[j] == 'w')
                    {
                        tmpQ[i].West = tmpQ[j];
                    }
                }
            }
            screen = tmpQ[0];
            Draw();
        }

        public void Draw()
        {
            this.drawingBox.Invalidate();
        }
        public delegate void DrawDelegate();

        private void button2_Click(object sender, EventArgs e)
        {
            Draw();
        }

        internal bool MoveAvatar(int player, char keyPressed)
        {
            switch (keyPressed)
            {
                case 'w':
                    if (avatars[player].PosY != 0)
                    {
                        if (screen.Tiles[avatars[player].PosX, avatars[player].PosY - 1] != 'u')
                        {
                            return true;
                        }
                    }
                    else
                    {
                        screen = screen.North;
                        return true;
                    }
                    break;
                case 's':
                    if (avatars[player].PosY != 7)
                    {
                        if (screen.Tiles[avatars[player].PosX, avatars[player].PosY + 1] != 'u')
                        {
                            return true;
                        }
                    }
                    else
                    {
                        screen = screen.South;
                        return true;
                    }
                    break;
                case 'a':
                    if (avatars[player].PosX != 0)
                    {
                        if (screen.Tiles[avatars[player].PosX - 1, avatars[player].PosY] != 'u')
                        {
                            return true;
                        }
                    }
                    else
                    {
                        screen = screen.West;
                        return true;
                    }
                    break;
                case 'd':
                    if (avatars[player].PosX != 13)
                    {
                        if (screen.Tiles[avatars[player].PosX + 1, avatars[player].PosY] != 'u')
                        {
                            return true;
                        }
                    }
                    else
                    {
                        screen = screen.East;
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
