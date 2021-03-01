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
        private List<Room> rooms;

        private int roomIndex = 0;

        public RoomHandler(SpriteBatch sb)
        {
            spriteBatch = sb;
            rooms = new List<Room>();
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

                roomNum = Int32.Parse(room.Element(num).Value);

                Console.WriteLine();
            }


        }

    }
}
