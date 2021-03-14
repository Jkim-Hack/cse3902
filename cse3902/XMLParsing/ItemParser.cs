using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.Items;
using cse3902.Interfaces;
using cse3902.Sprites;
using cse3902.SpriteFactory;
using cse3902.Entities.Enemies;
using cse3902.Entities;
using cse3902.Blocks;
using cse3902.Doors;
using cse3902.Rooms;
using System.Linq;

namespace cse3902.XMLParsing
{
    public class ItemParser
    {
        private Game1 game;

        public ItemParser(Game1 gm)
        {
            game = gm;
        }

        public IItem createItem(String type, Vector2 startPos)
        {
            IItem newItem = null;
            switch (type)
            {
                case "Arrow":
                    newItem = ItemSpriteFactory.Instance.CreateArrowItem(game.spriteBatch, startPos, new Vector2(1, 0));
                    break;
                case "Bow":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBowItem(game.spriteBatch, startPos);
                    break;
                case "Clock":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBowItem(game.spriteBatch, startPos);
                    break;
                case "Compass":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateCompassItem(game.spriteBatch, startPos);
                    break;
                case "Fairy":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateFairyItem(game.spriteBatch, startPos);
                    break;
                case "HeartContainer":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateHeartContainerItem(game.spriteBatch, startPos);
                    break;
                case "Heart":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateHeartItem(game.spriteBatch, startPos);
                    break;
                default:
                    //createdItem = null;
                    break;
            }
            return newItem;
        }


        public void parseItems(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName itemsName = XName.Get("items", doc.Root.Name.NamespaceName);

            XElement items = roomxml.Element(itemsName);
            List<XElement> itemList = items.Elements("item").ToList();

            foreach (XElement item in itemList)
            {
                XElement typeName = item.Element("type");

                XElement xloc = item.Element("xloc");
                XElement yloc = item.Element("yloc");

                int x = Int32.Parse(xloc.Value);
                int y = Int32.Parse(yloc.Value);

                IItem itemAdd = createItem(typeName.Value, new Vector2(x, y));
                roomobj.AddItem(itemAdd);
            }
        }

    }
}
