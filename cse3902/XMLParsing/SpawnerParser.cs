using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;
using cse3902.Interfaces;
using cse3902.Rooms;
using System.Linq;
using cse3902.Spawners;

namespace cse3902.XMLParsing
{
    public class SpawnerParser
    {
        private Game1 game;

        public SpawnerParser(Game1 gm)
        {
            game = gm;
        }

        public void ParseSpawners(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName spawnersName = XName.Get("spawners", doc.Root.Name.NamespaceName);

            XElement spawners = roomxml.Element(spawnersName);
            List<XElement> spawnerList = spawners.Elements("spawner").ToList();

            foreach (XElement spawner in spawnerList)
            {
                XElement typeName = spawner.Element("type");
                XElement count = spawner.Element("count");
                XElement xloc = spawner.Element("xloc");
                XElement yloc = spawner.Element("yloc");

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);

                int enemyCount = int.Parse(count.Value);

                // Use block center because spawners will just be invis blocks that spawn enemies
                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                ISpawner spawnerAdd = CreateSpawner(typeName.Value, enemyCount, truePos);
                roomobj.AddSpawner(spawnerAdd);
            }
        }

        private ISpawner CreateSpawner(String type, int count, Vector2 startPos)
        {
            ISpawner spawner = null;
            switch (type)
            {
                case "keese":
                    spawner = new KeeseSpawner(game, startPos, count);
                    break;
                case "stalfos":
                    spawner = new StalfosSpawner(game, startPos, count);
                    break;
                case "goryia":
                    spawner = new GoriyaSpawner(game, startPos, count);
                    break;
                case "gel":
                    spawner = new GelSpawner(game, startPos, count);
                    break;
            }
            return spawner;
        }
    }
}
