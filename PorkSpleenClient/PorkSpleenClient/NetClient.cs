using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace PorkSpleenClient
{
    class NetClient
    {
        public enum ClientPacketTypes { LOGON = 0, MOVED, CHANGEDSCREEN, CHANGEDREGION};
        public enum ServerPacketTypes { LOGONSUCCEDE = 50, LOGONFAIL, PLAYERJOINED, SENDPLAYER, SENDWORLD, SENDCARDS, SENDDECK, };
        Form1 _mainForm;
        Thread _tcpListen;
        string _serverIp;
        StreamReader _streamReader;
        StreamWriter _streamWriter;
        TcpClient client;
        object _sendLocker = new object();
        bool connected;

        public NetClient(Form1 mainForm)
        {
            _mainForm = mainForm;
            connected = false;
        }

        public bool Setup()
        {
            _tcpListen = new Thread(TcpListen);
            return true;
        }

        public void TcpListen()
        {
            try
            {
                IPHostEntry he = Dns.GetHostEntry(_serverIp);
                client = new TcpClient(_serverIp, 5550);
                connected = true;
                NetworkStream stream = client.GetStream();

                _streamReader = new StreamReader(stream);
                _streamWriter = new StreamWriter(stream);
                _mainForm.ChangeState(Form1.clientState.LOGIN);

                string readLine;
                while (connected)
                {
                    readLine = _streamReader.ReadLine();
                    UnpackMessage(readLine);
                }

            }
            catch (IOException ie)
            {

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Run(string serverIp)
        {
            _serverIp = serverIp;
            _tcpListen.Start();
        }

        public void sendMessage(string message)
        {
            lock (_sendLocker)
            {
                _streamWriter.WriteLine(message);
                _streamWriter.Flush();
            }

        }

        internal void SendLogon(string userName, string password)
        {
            string[] sendString = {userName, password};
            sendMessage(packMessage((int)ClientPacketTypes.LOGON, sendString));
        }

        public string packMessage(int type, string[] args)
        {
            string returnString = "" + type + "|";
            for (int i = 0; i < args.Length; i++)
            {
                returnString += args[i] + "|";
            }
            returnString += "@";
            return returnString;
        }

        private void UnpackMessage(string readLine)
        {
            string[] packets = readLine.Split('@');
            if (packets.Length == 2)
            {
                ParsePacket(packets[0]);
            }
            else
            {
                ParsePacket(packets);
            }

        }

        void ParsePacket(string packet)
        {
            string[] splitMessage = packet.Split('|');
            int type = int.Parse(splitMessage[0]);
            switch (type)
            {
                case (int)ClientPacketTypes.MOVED:

                    break;
                case (int)ClientPacketTypes.CHANGEDSCREEN:
                    break;
                case (int)ClientPacketTypes.CHANGEDREGION:
                    break;
                case (int)ServerPacketTypes.LOGONSUCCEDE:
                    _mainForm.ChangeState(Form1.clientState.ADVENTURE);
                    break;
                case (int)ServerPacketTypes.LOGONFAIL:
                    // display popup for failed login
                    break;
                case (int)ServerPacketTypes.SENDPLAYER:
                    _mainForm.Player.FromString(splitMessage);
                    _mainForm.PlayerBuilt = true;
                    break;
                case (int)ServerPacketTypes.SENDWORLD:
                    _mainForm.OverWorld.FromString(splitMessage);
                    _mainForm.WorldBuilt = true;
                    break;
                case (int)ServerPacketTypes.SENDCARDS:
                    break;
                case (int)ServerPacketTypes.SENDDECK:
                    break;
                default:
                    break;
            }
        }

        void ParsePacket(string[] packets)
        {
            for (int i = 0; i < packets.Length-1; i++)
            {
                ParsePacket(packets[i]);
            }
        }

        internal void Stop()
        {
            connected = false;
            client.Close();
            _streamReader.Close();
        }
    }
}