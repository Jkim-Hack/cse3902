using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Entities;
using cse3902.Entities.Enemies;
using cse3902.Rooms;
using System.Linq;

namespace cse3902.XMLParsing
{
    public class TrapsParser
    {
        private Game1 game;

        public TrapsParser(Game1 game)
        {
            this.game = game;
        }

        public void ParseTraps(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName trapsName = XName.Get("traps", doc.Root.Name.NamespaceName);

            XElement traps = roomxml.Element(trapsName);
            List<XElement> trapList = traps.Elements("trap").ToList();

            foreach (XElement trap in trapList)
            {
                XElement xloc = trap.Element("xloc");
                XElement yloc = trap.Element("yloc");
                XElement xdir = trap.Element("xdir");
                XElement ydir = trap.Element("ydir");

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);

                int xdirection = (int) float.Parse(xdir.Value);
                int ydirection = (int) float.Parse(ydir.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                ITrap trapAdd = CreateTrap(truePos, new Vector2(xdirection, ydirection));
                roomobj.AddTrap(trapAdd);
            }
        }

        private ITrap CreateTrap(Vector2 startingPos, Vector2 direction)
        {
            ITrap trap = new Trap(game, startingPos, direction);
            
            return trap;
        }

    }
}