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
            XName enemiesName = XName.Get("enemies", doc.Root.Name.NamespaceName);

            XElement enemies = roomxml.Element(enemiesName);
            List<XElement> enemyList = enemies.Elements("enemy").ToList();

            foreach (XElement enemy in enemyList)
            {
                XElement typeName = enemy.Element("type");
                XElement xloc = enemy.Element("xloc");
                XElement yloc = enemy.Element("yloc");

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                IEntity enemyAdd = CreateEnemy(typeName.Value, truePos);
                roomobj.AddEnemy(enemyAdd);
            }
        }

        private IEntity CreateEnemy(String type, Vector2 startingPos)
        {
            IEntity trap = new Trap(game, startingPos);
            
            return trap;
        }

    }
}