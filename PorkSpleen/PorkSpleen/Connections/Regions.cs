using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PorkSpleen
{
    class Regions
    {
        Hashtable screens;
        public Regions()
        {
            screens = new Hashtable();
        }

        public void insertConnection(NetServer.activeConnection connection, int screen)
        {
            if (screens[(object)screen] == null)
            {
                screens.Add((object)screen, new Screens());
            }
            ((Screens)screens[(object)screen]).insertConnection(connection);
        }

        public NetServer.activeConnection getConnection(int screen, int id)
        {
            return ((Screens)screens[(object)screen]).getConnection(id);
        }
    }
}
