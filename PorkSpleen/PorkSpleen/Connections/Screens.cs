using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PorkSpleen
{
    class Screens
    {
        Hashtable connections;
        public Screens()
        {
            connections = new Hashtable();
        }

        public void insertConnection(NetServer.activeConnection connection)
        {
            connections.Add((object)connection.UserId, connection);
        }

        public NetServer.activeConnection getConnection(int id)
        {
            return (NetServer.activeConnection)connections[(object)id];
        }
    }
}
