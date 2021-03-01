using System.Collections.Generic;
using System;
using System.Xml.Linq;

namespace cse3902.Rooms
{
    public class RoomHandler
    {
        private List<Room> rooms;

        private int roomIndex = 0;

        public RoomHandler()
        {


        }

        public void Initialize()
        {
            String url = "https://raw.githubusercontent.com/Jkim-Hack/cse3902/master/cse3902/Rooms/Room1.xml";

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

        public void parseXML(String filename)
        {
            //string text = System.IO.File.ReadAllText(@"../Room1.xml");

            XDocument doc = XDocument.Load(filename);

            XElement map = XElement.Load(filename);

            XName roomName = XName.Get("rooms", doc.Root.Name.NamespaceName);

            foreach (var room in map.Elements(roomName))
            {
                //foreach (var )
            }


            //var messagesElement = XElement.Parse(text);
            //var messagesList = (from message in messagesElement.Elements("room")
            //                    select new
            //                    {
            //                        Subclass = message.Attribute("subclass").Value,
            //                        Context = message.Attribute("context").Value,
            //                        Key = message.Attribute("key").Value
            //                    }).ToList();
        }
            
    }
}
