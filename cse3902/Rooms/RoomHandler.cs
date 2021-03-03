using System.Collections.Generic;
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Items;
using cse3902.Interfaces;

namespace cse3902.Rooms
{
    public class RoomHandler
    {
        private SpriteBatch spriteBatch;
        public Dictionary<Tuple<int, int>, Room> rooms;

        private int roomIndex = 0;

        private Tuple<int, int> currentRoom;

        public RoomHandler(SpriteBatch sb)
        {
            spriteBatch = sb;
            rooms = new Dictionary<Tuple<int, int>, Room>();
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

        public IItem createItem(String type)
        {
            return null;
            //return ItemSpriteFactory.Instance.CreateArrowItem();
        }

        public void parseItems(Room room, XElement xmlElem, XDocument doc)
        {

            XName items = XName.Get("items", doc.Root.Name.NamespaceName);
            foreach (var item in xmlElem.Elements(items))
            {
                XName type = XName.Get("type", doc.Root.Name.NamespaceName);

                IItem itemAdd = createItem(item.Element(type).Value);
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

                Tuple<int, int> roomTup = new Tuple<int, int>(Int32.Parse(room.Element(num).Value.Substring(0, comma)), Int32.Parse(room.Element(num).Value.Substring(comma+1)));
 
                //parseItems(currentRoom, )

                Console.WriteLine();



            }


        }

        public void LoadNewRoom(Tuple<int, int> newPos)
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
