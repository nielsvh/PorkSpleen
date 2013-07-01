using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PorkSpleen
{
    class Contenent
    {
        Hashtable regions;
        public Contenent()
        {
            regions = new Hashtable();
        }

        public void insertConnection(NetServer.activeConnection connection, int region, int screen)
        {
            if (regions[(object)region] == null)
            {
                regions.Add((object)region, new Regions());
            }
            ((Regions)regions[(object)region]).insertConnection(connection, screen);
        }

        public NetServer.activeConnection getConnection(int region, int screen, int id)
        {
            return ((Regions)regions[(object)region]).getConnection(screen,id);
        }
    }
}
