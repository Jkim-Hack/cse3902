using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Entities.Enemies;
using cse3902.Rooms;
using System.Linq;

namespace cse3902.XMLParsing
{
    public class EnemyParser
    {
        private Game1 game;

        public EnemyParser(Game1 gm)
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

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                IEntity enemyAdd = CreateEnemy(typeName.Value, truePos, new Vector2(x,y));
                roomobj.AddEnemy(enemyAdd);
            }
        }

        private IEntity CreateEnemy(String type, Vector2 startingPos, Vector2 abstractPos)
        {
            IEntity newEnemy = new Keese(game, startingPos, IEntity.EnemyType.X);
            switch (type)
            {
                case "Aquamentus":
                    newEnemy = new Aquamentus(game, startingPos, IEntity.EnemyType.D);
                    break;
                case "Gel":
                    newEnemy = new Gel(game, startingPos, IEntity.EnemyType.X);
                    break;
                case "Goriya":
                    newEnemy = new Goriya(game, startingPos, IEntity.EnemyType.D);
                    break;
                case "Keese":
                    newEnemy = new Keese(game, startingPos, IEntity.EnemyType.X);
                    break;
                case "Stalfos":
                    newEnemy = new Stalfos(game, startingPos, IEntity.EnemyType.C);
                    break;
                case "Wallmaster":
                    newEnemy = new WallMaster(game, startingPos, abstractPos, IEntity.EnemyType.C);
                    break;
                default:
                    break;
            }
            return newEnemy;
        }

    }
}
