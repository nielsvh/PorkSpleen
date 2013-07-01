using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PorkSpleenClient
{
    public partial class Form1 : Form
    {
        public enum clientState { CONNECT, LOGIN, ADVENTURE, STORE, GAME };
        clientState ClientState;

        ChatControl chatControl;
        ConnectControl connectControl;
        LoadContentControl loadContentControl;
        LogonControl logonControl;
        OverWorldControl overWorldControl;
        NetClient client;
        Player player;

        public bool playerBuilt, worldBuilt, collectionBuilt, decksBuilt;

        public bool WorldBuilt
        {
            get { return worldBuilt; }
            set { worldBuilt = value; }
        }

        public bool PlayerBuilt
        {
            get { return playerBuilt; }
            set { playerBuilt = value; }
        }

        public OverWorldControl OverWorld
        {
            get { return overWorldControl; }
            set { overWorldControl = value; }
        }

        internal Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public Form1()
        {
            connectControl = new ConnectControl();
            loadContentControl = new LoadContentControl();
            logonControl = new LogonControl();
            overWorldControl = new OverWorldControl(this);
            chatControl = new ChatControl();
            ClientState = clientState.CONNECT;
            InitializeComponent();
            connectControl.connectButton.Click += new System.EventHandler(this.ConnectClick);
            logonControl.LogonButton.Click += new System.EventHandler(this.LogonClick);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.Controls.Add(connectControl);
            this.Controls.Add(loadContentControl);
            this.Controls.Add(logonControl);
            this.Controls.Add(overWorldControl);
            this.Controls.Add(chatControl);
            overWorldControl.LoadContent();
            logonControl.Visible = chatControl.Visible = overWorldControl.Visible = loadContentControl.Visible = false;
            playerBuilt = worldBuilt = collectionBuilt = decksBuilt = false;

            client = new NetClient(this);
            client.Setup();
            player = new Player(this.overWorldControl);
        }

        public void ConnectClick(object sender, EventArgs e)
        {
            client.Run(connectControl.addressTextBox.Text);
        }

        public void LogonClick(object sender, EventArgs e)
        {
            client.SendLogon(logonControl.UserNameTextBox.Text, logonControl.PasswordTextBox.Text);
        }

        public void ChangeState(clientState newState)
        {

            if (!InvokeRequired)
            {
                switch (ClientState)
                {
                    case clientState.CONNECT:
                        if (newState == clientState.LOGIN)
                        {
                            this.connectControl.Visible = false;
                            this.logonControl.Visible = true;
                            ClientState = newState;
                        }
                        break;
                    case clientState.LOGIN:
                        if (newState == clientState.ADVENTURE)
                        {
                            ClientState = newState;

                            Loading();
                        }
                        break;
                    case clientState.ADVENTURE:
                        break;
                    case clientState.STORE:
                        break;
                    case clientState.GAME:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //if(message is string && sender is User && roomID is int && userID is int)
                Invoke(new ChangeStateDel(ChangeState), new object[] { newState });
            }
        }
        public delegate void ChangeStateDel(clientState newState);

        public void Loading()
        {
            loadContentControl.Visible = true;
            //while (!playerBuilt&&!worldBuilt)
            {

            }
            loadContentControl.Visible = false;

            this.logonControl.Visible = false;
            this.overWorldControl.Visible = true;
            this.chatControl.Visible = true;
            this.chatControl.Location = new Point(this.chatControl.Location.X, overWorldControl.Height);
            overWorldControl.Draw();
            this.logonControl.Enabled = false;
            this.connectControl.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClientState != clientState.CONNECT)
            {
                client.Stop();
            }
        }

        public void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                case 's':
                case 'a':
                case 'd':
                    if (this.overWorldControl.MoveAvatar(0, e.KeyChar))
                    {
                        this.player.MoveAvatar(e.KeyChar);
                        this.overWorldControl.Draw();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}