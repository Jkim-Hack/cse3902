using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;
using cse3902.Interfaces;
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

        public void ParseItems(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName itemsName = XName.Get("items", doc.Root.Name.NamespaceName);

            XElement items = roomxml.Element(itemsName);
            List<XElement> itemList = items.Elements("item").ToList();

            foreach (XElement item in itemList)
            {
                XElement typeName = item.Element("type");
                XElement xloc = item.Element("xloc");
                XElement yloc = item.Element("yloc");

                float x = float.Parse(xloc.Value);
                float y = float.Parse(yloc.Value);

                Vector2 truePos = RoomUtilities.CalculateBlockCenter(roomobj.RoomPos, new Vector2(x, y));

                IItem itemAdd = CreateItem(typeName.Value, truePos);
                roomobj.AddItem(itemAdd);
            }
        }

        private IItem CreateItem(String type, Vector2 startPos)
        {
            IItem newItem = (IItem)ItemSpriteFactory.Instance.CreateHeartItem(game.SpriteBatch, startPos, true, true);
            switch (type)
            {
                case "Arrow":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateArrowItem(game.SpriteBatch, startPos);
                    break;
                case "BluePotion":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBluePotionItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "BlueRing":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBlueRingItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Bomb":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBombItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Boomerang":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBoomerangItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Bow":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBowItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Clock":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateClockItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Compass":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateCompassItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Fairy":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateFairyItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "HeartContainer":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateHeartContainerItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Heart":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateHeartItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Key":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateKeyItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "MagicBook":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateMagicBookItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Map":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateMapItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "Rupee":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateRupeeItem(game.SpriteBatch, startPos, true, true);
                    break;
                case "WoodSword":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateSwordItem(game.SpriteBatch, startPos, true, true, 0);
                    break;
                case "WhiteSword":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateSwordItem(game.SpriteBatch, startPos, true, true, 1);
                    break;
                case "MagicSword":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateSwordItem(game.SpriteBatch, startPos, true, true, 2);
                    break;
                case "MagicRod":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateSwordItem(game.SpriteBatch, startPos, true, true, 3);
                    break;
                case "Triforce":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateTriforceItem(game.SpriteBatch, startPos, true, true);
                    break;
                default:
                    break;
            }
            return newItem;
        }


    }
}
