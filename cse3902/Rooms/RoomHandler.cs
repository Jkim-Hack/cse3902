using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using cse3902.Items;
using cse3902.Interfaces;
using cse3902.Sprites;
using cse3902.SpriteFactory;

namespace cse3902.Rooms
{
    public class RoomHandler
    {
        private SpriteBatch spriteBatch;
        public Dictionary<Tuple<int, int, int>, Room> rooms;

        private int roomIndex = 0;

        private Tuple<int, int, int> currentRoom;

        public RoomHandler(SpriteBatch sb)
        {
            spriteBatch = sb;
            rooms = new Dictionary<Tuple<int, int, int>, Room>();
        }

        public void Initialize()
        {
            String url = "https://raw.githubusercontent.com/Jkim-Hack/cse3902/file-input/cse3902/Rooms/Room1.xml";
            parseXML(url);
        }

        public void CycleNext()
        {
            roomIndex++;
            if (roomIndex >= rooms.Count)
            {
                roomIndex = 0;
            }
        }

        public void CyclePrev()
        {
            roomIndex--;
            if (roomIndex < 0)
            {
                roomIndex = rooms.Count - 1;
            }
        }

        public IItem createItem(String type, Vector2 startPos)
        {
            IItem newItem = null;
            //return ItemSpriteFactory.Instance.CreateArrowItem();
            switch (type)
            {
                case "Arrow":
                    newItem = (IItem) ItemSpriteFactory.Instance.CreateArrowItem(spriteBatch, startPos, new Vector2(1, 0));
                    break;
                case "Bow":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBowItem(spriteBatch, startPos);
                    break;
                case "Clock":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateBowItem(spriteBatch, startPos);
                    break;
                case "Compass":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateCompassItem(spriteBatch, startPos);
                    break;
                case "Fairy":
                    //newItem = ItemSpriteFactory.Instance.CreateArrowItem();
                    break;
                case "HeartContainer":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateHeartContainerItem(spriteBatch, startPos);
                    break;
                case "Heart":
                    newItem = (IItem)ItemSpriteFactory.Instance.CreateHeartItem(spriteBatch, startPos);
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
            //return ItemSpriteFactory.Instance.CreateArrowItem();
            switch (type)
            {
                //case "Aquamentus":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Gel":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Goriya":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Keese":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Stalfos":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "WallMaster":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "OldMan":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "MedicineWoman":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Merchant":
                //    newEnemy = EnemySpriteFactory.Instance.CreateArrowItem();
                //    break;
                //default:
                //    //createdItem = null;
                //    break;
            }
            return newEnemy;
        }

        public BlockSprite createBlock(String type)
        {
            BlockSprite newBlock = null;
            //return ItemSpriteFactory.Instance.CreateArrowItem();
            switch (type)
            {
                //case "Aquamentus":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Gel":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Goriya":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Keese":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Stalfos":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "WallMaster":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "OldMan":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "MedicineWoman":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //case "Merchant":
                //    createdItem = ItemSpriteFactory.Instance.CreateArrowItem();
                //    break;
                //default:
                //    //createdItem = null;
                //    break;
            }
            return newBlock;
        }

        public void parseItems(Room room, XElement xmlElem, XDocument doc)
        {

            XName items = XName.Get("items", doc.Root.Name.NamespaceName);
            foreach (var item in xmlElem.Elements(items))
            {
                XName type = XName.Get("type", doc.Root.Name.NamespaceName);

                XName xloc = XName.Get("xloc", doc.Root.Name.NamespaceName);
                XName yloc = XName.Get("yloc", doc.Root.Name.NamespaceName);

                int x = Int32.Parse(item.Element(xloc).Value);
                int y = Int32.Parse(item.Element(yloc).Value);

                IItem itemAdd = createItem(item.Element(type).Value, new Vector2(x, y));
                room.AddItem(itemAdd);
            }
            
        }

        public void parseEnemies(Room room, XElement xmlElem, XDocument doc)
        {

            XName items = XName.Get("enemies", doc.Root.Name.NamespaceName);
            foreach (var item in xmlElem.Elements(items))
            {
                XName type = XName.Get("type", doc.Root.Name.NamespaceName);

                XName xloc = XName.Get("xloc", doc.Root.Name.NamespaceName);
                XName yloc = XName.Get("yloc", doc.Root.Name.NamespaceName);

                int x = Int32.Parse(item.Element(xloc).Value);
                int y = Int32.Parse(item.Element(yloc).Value);

                IEntity enemyAdd = createEnemy(item.Element(type).Value, new Vector2(x, y));
                room.AddEnemy(enemyAdd);
            }

        }

        public void parseBlocks(Room room, XElement xmlElem, XDocument doc)
        {

            XName items = XName.Get("items", doc.Root.Name.NamespaceName);
            foreach (var item in xmlElem.Elements(items))
            {
                XName type = XName.Get("type", doc.Root.Name.NamespaceName);

                XName xloc = XName.Get("xloc", doc.Root.Name.NamespaceName);
                XName yloc = XName.Get("yloc", doc.Root.Name.NamespaceName);

                int x = Int32.Parse(item.Element(xloc).Value);
                int y = Int32.Parse(item.Element(yloc).Value);

                IItem itemAdd = createItem(item.Element(type).Value, new Vector2(x, y));
                room.AddItem(itemAdd);
            }

        }

        public void parseXML(String filename)
        {
            XDocument doc = XDocument.Load(filename);
            XElement map = XElement.Load(filename);

            int roomNum;

            XName roomName = XName.Get("rooms", doc.Root.Name.NamespaceName);

            foreach (var room in map.Elements(roomName))
            {
                Room currentRoom;

                XName chil = XName.Get("room", doc.Root.Name.NamespaceName);
                XName num = XName.Get("number", doc.Root.Name.NamespaceName);

                int comma = room.Element(num).Value.IndexOf(',');
                int comma2 = room.Element(num).Value.IndexOf(',', comma+1);

                Tuple<int, int, int> roomTup = new Tuple<int, int, int>(Int32.Parse(room.Element(num).Value.Substring(0, comma)), Int32.Parse(room.Element(num).Value.Substring(comma+1, comma2)), Int32.Parse(room.Element(num).Value.Substring(comma2)));

                currentRoom = new Room(roomTup);

                rooms.Add(roomTup, currentRoom);

                parseItems(currentRoom, room, doc);
            }


        }

        public void LoadNewRoom(Tuple<int, int, int> newPos)
        {
            Room newRoom = rooms.GetValueOrDefault(newPos);

            List<IItem> oldItems = rooms.GetValueOrDefault(currentRoom).Items;
            RoomItems.Instance.LoadNewRoom(ref oldItems, newRoom.Items);
            rooms.GetValueOrDefault(currentRoom).Items = oldItems;

            List<IEntity> oldEnemies = rooms.GetValueOrDefault(currentRoom).Enemies;
            RoomEnemyNPCs.Instance.LoadNewRoom(ref oldEnemies, newRoom.Enemies);
            rooms.GetValueOrDefault(currentRoom).Enemies = oldEnemies;

            currentRoom = newPos;
            rooms.GetValueOrDefault(newPos).SetToVisited();
        }

    }
}
