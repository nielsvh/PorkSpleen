using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PorkSpleen
{
    class NetServer
    {
        public enum ClientPacketTypes { LOGON = 0, };
        public enum ServerPacketTypes { LOGONSUCCEDE = 50, LOGONFAIL, SENDPLAYER, SENDWORLD, SENDCARDS, SENDDECK, };
        // ensure that only one connection to the sqlserver is done at once.
        static readonly object _sqlLocker = new object();
        static readonly object _sendLocker = new object();

        // maximum number of player on the server at any given time
        public const int MAXAWAITING = 20;

        // connection to the sql server
        MySqlConnection _sqlConnection;

        // boolean telling thread whether or not they are to continue running or not.
        bool _runTcp;

        // Reference to the main form so that we can write to it when anything happens or something
        Form1 _mainForm;

        // Main thread listens for incoming connections and deals with tem accordingly
        Thread _tcpListener;

        // struct that represents an active connection to a client. Stores the thread info, the socket, the user's id and their name.
        public struct activeConnection {
            public bool Active;
            public TcpClient C;
            public StreamReader R;
            public StreamWriter W;
            public Thread T;
            public int UserId;
            public string Name;
            public int Contenent, Region, Screen;
        };

        // hash of contenents, hash of regions, hash of screens, hash of players;
        Hashtable contenents;

        activeConnection[] awaitingLogin;

        public NetServer(Form1 mainForm)
        {
            contenents = new Hashtable();
            awaitingLogin = new activeConnection[MAXAWAITING];
            _mainForm = mainForm;
        }

        ~NetServer()
        {
            //_sqlConnection.Close();
        }


        /// <summary>
        /// Tells the server to stop. Shutdown threads set run variables to false, etc.
        /// </summary>
        public void Stop()
        {
            _runTcp = false;
        }

        /// <summary>
        /// Setup the new instance of the NetServer.
        /// </summary>
        /// <returns>True if the NetServer setup is a success. Otherwise it is a failure.</returns>
        public bool Setup()
        {
            // setup new connection to the mysql database
            string connStr = "server=localhost;user=root;database=world;port=3306;password=root;";
            _sqlConnection = new MySqlConnection(connStr);
            try
            {
                //string command;
                //MySqlDataReader reader;
                //MySqlDataReader tmp;
                //int i = 0;
                //int[] contId = new int[NUMCONT];
                //int[] regId = new int[NUMREGS];
                //int[] scrId = new int[NUMSCRS];
                //_mainForm.PostMessage("Connecting to MySQL...");
                //_mainForm.PostMessage("Creating connection table...");
                //command = "select count(*) from contenent";
                //reader = SendSqlCommand(command);
                //while (reader.Read()) players = new Hashtable[reader.GetInt32(0)][][];

                //command = "select contenentId,count(*) fom contenents group by contenentId";
                //reader = SendSqlCommand(command);
                
                //while (reader.Read())
                //{
                //    contId[i] = reader.GetInt32(0);
                //    i++;
                //}
                //i = 0;
                //for (int j = 0; j < contId.Length; j++)
                //{
                //    command = "select region.id fom contenent join region";
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            // create the TcpListener thread
            _tcpListener = new Thread(TcpListen);
            _runTcp = true;
            return true;
        }

        /// <summary>
        /// Send a command to the MySql database. This method should be threadsafe.
        /// </summary>
        /// <param name="mysql">Command to be sent to the server</param>
        private MySqlDataReader SendSqlCommand(string mysql)
        {
            _sqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand(mysql, _sqlConnection);
            MySqlDataReader rdr;
            lock (_sqlLocker) rdr = cmd.ExecuteReader();
            //_sqlConnection.Close();
            return rdr;
        }



        private void CleanupSqlCommand(MySqlDataReader rdr)
        {
            rdr.Close();
            _sqlConnection.Close();
        }
         
        // start main function
        public void Run()
        {
            _tcpListener.Start();  
        }

        // listen for TCP connections
        // on TCP connection make a client
        // on TCP close delete client
        public void TcpListen()
        {
            IPHostEntry he = Dns.GetHostEntry("localhost");
            IPAddress Ip = he.AddressList[0];
            // if needed for forcing ipv4 or ipv6
            /*IPAddress Ip = null;
            for (int i = 0; i < he.AddressList.Length; i++)
            {
                Ip = he.AddressList[i];
                if (Ip.AddressFamily == AddressFamily.InterNetwork)
                    break;
            }*/

            TcpListener listener = new TcpListener(Ip, 5550);
            listener.Start();
            int openConnection;
            while (_runTcp)
            {
                TcpClient client = listener.AcceptTcpClient();
                int open = 0;
                for (; open < awaitingLogin.Length; open++)
                {
                    if (!awaitingLogin[open].Active)
                    {
                        break;
                    }
                    if (open == awaitingLogin.Length-1)
                    {
                        open = -1;
                        break;
                    }
                }
                if (open >= 0)
                {
                    // give the client its socket
                    awaitingLogin[open].C = client;
                    // make the client active
                    awaitingLogin[open].Active = true;
                    // use lamda notation to call clientrun with the slot of the current player
                    Thread newClient = new Thread(() => ClientRun(open));
                    // give the client information about its own thread
                    awaitingLogin[open].T = newClient;
                    // start the thread and listen for more clients
                    newClient.Start();
                }
            }
            listener.Stop();
        }

        void ParsePacket(string packet, int clientId)
        {
            int type = int.Parse("" + packet[0]);
            string dataString = null;
            switch (type)
            {
                case (int)ClientPacketTypes.LOGON:
                    int posX, posY;
                    if (CheckLogon(packet, clientId, out posX, out posY))
                    {
                        // SENDPLAYER, SENDWORLD, SENDCARDS, SENDDECK,
                        SendPlayerInfo(clientId, posX, posY);
                        SendWorldInfo(clientId);
                        SendCards(clientId);
                        SendDecks(clientId);
                    }
                    break;
                default:
                    break;
            }
        }

        private void SendDecks(int clientId)
        {

        }

        private void SendCards(int clientId)
        {

        }

        private void SendPlayerInfo(int clientId, int posX, int posY)
        {
            sendMessage(packPacket((int)ServerPacketTypes.SENDPLAYER, new string[] { awaitingLogin[clientId].UserId + "", awaitingLogin[clientId].Name + "", awaitingLogin[clientId].Contenent + "", awaitingLogin[clientId].Region + "", awaitingLogin[clientId].Screen + "", posX + "", posY + "" }), clientId);
        }

        private void SendWorldInfo(int clientId)
        {
            MySqlDataReader rdr = SendSqlCommand("select screen.screenData, region.screenMap from regions inner join screen inner join contenents inner join region inner join user on regions.screenId = \"" + awaitingLogin[clientId].UserId + "\" and \"" + awaitingLogin[clientId].UserId + "\" = regions.regionId and contenents.contenentId = \"" + awaitingLogin[clientId].UserId + "\";");
            bool first = true;
            List<string> list = new List<string>();
            while (rdr.Read())
            {
                if (first)
                {
                    first = false;
                    list.Add(rdr.GetString(1));
                }
                list.Add(rdr.GetString(0));
            }
            CleanupSqlCommand(rdr);
            sendMessage(packPacket((int)ServerPacketTypes.SENDWORLD, list.ToArray()), clientId);
        }

        private bool CheckLogon(string packet, int clientId, out int posX, out int posY)
        {
            posX = 0;
            posY = 0;
            string[] splitstring = packet.Split('|');
            MySqlDataReader rdr = SendSqlCommand("select * from user where name = \"" + splitstring[1] + "\" and password = \"" + splitstring[2] + "\";");
            if (rdr.FieldCount >= 1)
            {
                sendMessage(packPacket((int)ServerPacketTypes.LOGONSUCCEDE, null), clientId);
                while (rdr.Read())
                {
                    awaitingLogin[clientId].UserId = rdr.GetInt32(0);
                    awaitingLogin[clientId].Name = rdr.GetString(1);
                    awaitingLogin[clientId].Contenent = rdr.GetInt32(3);
                    awaitingLogin[clientId].Region = rdr.GetInt32(4);
                    awaitingLogin[clientId].Screen = rdr.GetInt32(5);
                    posX = rdr.GetInt32(6);
                    posY = rdr.GetInt32(7);
                }
                CleanupSqlCommand(rdr);

                return true;
            }
            return false;
        }

        void ParsePacket(string[] packets, int clientId)
        {
            for (int i = 0; i < packets.Length-1;i++ )
            {
                ParsePacket(packets[i], clientId);
            }
        }

        public void ClientRun(int slotId)
        {
            int slot = slotId;
            connections[slot].R = new StreamReader(connections[slot].C.GetStream());
            connections[slot].W = new StreamWriter(connections[slot].C.GetStream());
            string readLine;

            while (connections[slot].Active)
            {
                try
                {
                    readLine = connections[slot].R.ReadLine();
                    UnpackMessage(readLine, slot);
                }
                catch (Exception)
                {
                    connections[slot].Active = false;
                }
            }
        }

        public string packPacket(int type, string[] args)
        {
            string returnString = "" + type + "|";
            if (args != null)
                for (int i = 0; i < args.Length; i++)
                {
                    returnString += args[i] + "|";
                }
            returnString += "@";
            return returnString;
        }

        private void UnpackMessage(string readLine, int clientId)
        {
            string[] packets = readLine.Split('@');
            if (packets.Length == 2)
            {
                ParsePacket(packets[0], clientId);
            }
            else
            {
                ParsePacket(packets, clientId);
            }

        }
        public void sendMessage(string message, int clientId)
        {
            lock (_sendLocker)
            {
                connections[clientId].W.WriteLine(message);
                connections[clientId].W.Flush();
            }

        }
    }
}
