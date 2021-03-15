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
    public class EnemyNPCParser
    {
        private Game1 game;

        public EnemyNPCParser(Game1 gm)
        {
            game = gm;
        }

        public void ParseEnemies(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName enemiesName = XName.Get("enemies", doc.Root.Name.NamespaceName);

            XElement enemies = roomxml.Element(enemiesName);
            List<XElement> enemyList = enemies.Elements("enemy").ToList();

            foreach (XElement enemy in enemyList)
            {
                XElement typeName = enemy.Element("type");

                XElement xloc = enemy.Element("xloc");
                XElement yloc = enemy.Element("yloc");

                int x = Int32.Parse(xloc.Value);
                int y = Int32.Parse(yloc.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.roomPos, new Vector2(x, y));

                IEntity enemyAdd = CreateEnemy(typeName.Value, truePos);
                roomobj.AddEnemy(enemyAdd);
            }
        }

        private IEntity CreateEnemy(String type, Vector2 startingPos)
        {
            IEntity newEnemy = new OldManNPC(game, startingPos);
            switch (type)
            {
                case "Aquamentus":
                    newEnemy = new Aquamentus(game, startingPos);
                    break;
                case "Gel":
                    newEnemy = new Gel(game, startingPos);
                    break;
                case "Goriya":
                    newEnemy = new Goriya(game, startingPos);
                    break;
                case "Keese":
                    newEnemy = new Keese(game, startingPos);
                    break;
                case "Stalfos":
                    newEnemy = new Stalfos(game, startingPos);
                    break;
                case "WallMaster":
                    newEnemy = new WallMaster(game, startingPos);
                    break;
                case "OldMan":
                    newEnemy = new OldManNPC(game, startingPos);
                    break;
                case "MedicineWoman":
                    newEnemy = new MedicineWomanNPC(game, startingPos);
                    break;
                case "Merchant":
                    newEnemy = new MerchantNPC(game, startingPos);
                    break;
                default:
                    //createdItem = null;
                    break;
            }
            return newEnemy;
        }

    }
}
