using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using cse3902.Items;
using cse3902.Interfaces;
using cse3902.Sprites;
using cse3902.SpriteFactory;
using cse3902.Entities.Enemies;
using cse3902.Entities;
using cse3902.Blocks;
using cse3902.Doors;
using System.Linq;

namespace cse3902.Rooms
{
    public class XMLParser
    {
        private RoomHandler roomHandler;
        private Game1 game;

        public XMLParser(RoomHandler roomHand, Game1 gm)
        {
            roomHandler = roomHand;
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

        public IEntity createEnemy(String type, Vector2 startingPos)
        {
            IEntity newEnemy = null;
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

        public IBlock createBlock(String type, String dir, Vector2 startingPos)
        {
            IBlock newBlock = null;
            switch (type)
            {
                case "Normal":
                    if (dir.Equals("Still"))
                    {
                        newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 10, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.spriteBatch, startingPos));
                    }
                    else if (dir.Equals("Down"))
                    {
                        newBlock = new NormalBlock(game, IBlock.PushDirection.Down, 10, BlockSpriteFactory.Instance.CreateNormalBlockSprite(game.spriteBatch, startingPos));
                    }
                    break;
                case "Water":
                    newBlock = new NormalBlock(game, IBlock.PushDirection.Still, 10, BlockSpriteFactory.Instance.CreateWaterBlockSprite(game.spriteBatch, startingPos));
                    break;
                default:
                    //createdItem = null;
                    break;
            }
            return newBlock;
        }

        public IDoor createDoor(String type, Vector2 startingPos)
        {
            IDoor newDoor = null;
            switch (type)
            {
                case "Up":
                    newDoor = new NormalUpDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "Down":
                    newDoor = new NormalDownDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "Left":
                    newDoor = new NormalLeftDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "Right":
                    newDoor = new NormalRightDoor(game, startingPos, IDoor.DoorState.Open);
                    break;
                case "StairDown":
                    newDoor = new DownStaircaseDoor(game, startingPos);
                    break;
                case "OffscreenUp":
                    newDoor = new OffscreenUpDoor(game, startingPos);
                    break;
                default:
                    break;
            }

            return newDoor;
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

        public void parseEnemies(Room roomobj, XElement roomxml, XDocument doc)
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

                IEntity enemyAdd = createEnemy(typeName.Value, new Vector2(x, y));
                roomobj.AddEnemy(enemyAdd);
            }
        }

        public void parseBlocks(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName blocksName = XName.Get("blocks", doc.Root.Name.NamespaceName);

            XElement blocks = roomxml.Element(blocksName);
            List<XElement> blockList = blocks.Elements("block").ToList();

            foreach (XElement block in blockList)
            {
                XElement typeName = block.Element("type");
                XElement dir = block.Element("dir");
                XElement xloc = block.Element("xloc");
                XElement yloc = block.Element("yloc");

                int x = Int32.Parse(xloc.Value);
                int y = Int32.Parse(yloc.Value);

                IBlock blockAdd = createBlock(typeName.Value, dir.Value, new Vector2(x, y));
                roomobj.AddBlock(blockAdd);
            }
  
        }

        public void parseDoors(Room roomobj, XElement roomxml, XDocument doc)
        {
            XName doorsName = XName.Get("doors", doc.Root.Name.NamespaceName);

            XElement doors = roomxml.Element(doorsName);
            List<XElement> doorList = doors.Elements("door").ToList();

            foreach (XElement door in doorList)
            {
                XElement typeName = door.Element("type");
                XElement connRoom = door.Element("connRoom");
                XElement xloc = door.Element("xloc");
                XElement yloc = door.Element("yloc");

                int x = Int32.Parse(xloc.Value);
                int y = Int32.Parse(yloc.Value);

                IDoor doorAdd = createDoor(typeName.Value, new Vector2(x, y));
                roomobj.AddDoor(doorAdd);
            }

        }

        private void LinkDoors()
        {

        }

        private void HandleDoorConnection(Vector3 currRoom)
        {

        }

        private Vector3 convertToVector3(String str)
        {
            int comma = str.IndexOf(',');
            int comma2 = str.IndexOf(',', comma + 1);

            Vector3 roomTup = new Vector3(Int32.Parse(str.Substring(0, comma)), Int32.Parse(str.Substring(comma + 1, 1)), Int32.Parse(str.Substring(comma2+1)));

            return roomTup;
        }

        public void parseXML(String filename)
        {
            XDocument doc = XDocument.Load(filename);
            XElement map = XElement.Load(filename);

            XName roomName = XName.Get("room", doc.Root.Name.NamespaceName);

            foreach (XElement room in map.Elements(roomName))
            {
                Room currentRoom;

                //XName chil = XName.Get("room", doc.Root.Name.NamespaceName);
                XName num = XName.Get("number", doc.Root.Name.NamespaceName);

                Vector3 roomTup = convertToVector3(room.Element(num).Value);

                currentRoom = new Room(roomTup);

                parseItems(currentRoom, room, doc);
                parseEnemies(currentRoom, room, doc);
                parseBlocks(currentRoom, room, doc);
                parseDoors(currentRoom, room, doc);

                roomHandler.rooms.Add(roomTup, currentRoom);

                if (roomHandler.rooms.Count == 1)
                {
                    roomHandler.currentRoom = roomTup;
                }
            }
        }
    }
}
