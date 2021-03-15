using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.XMLParsing;

namespace cse3902.Rooms
{
    public class XMLParser
    {
        private RoomHandler roomHandler;
        private ItemParser itemParser;
        private EnemyNPCParser enemyNPCParser;
        private BlockParser blockParser;
        private DoorParser doorParser;

        public XMLParser(RoomHandler roomHand, Game1 gm)
        {
            roomHandler = roomHand;
            itemParser = new ItemParser(gm);
            enemyNPCParser = new EnemyNPCParser(gm);
            blockParser = new BlockParser(gm);
            doorParser = new DoorParser(gm, roomHandler);
        }

        public void ParseXML(String filename)
        {
            XDocument doc = XDocument.Load(filename);
            XElement map = XElement.Load(filename);

            XName roomName = XName.Get("room", doc.Root.Name.NamespaceName);

            foreach (XElement room in map.Elements(roomName))
            {
                Room currentRoom;

                XName num = XName.Get("number", doc.Root.Name.NamespaceName);

                Vector3 roomTup = RoomUtilities.ConvertToVector3(room.Element(num).Value);

                currentRoom = new Room(roomTup);

                itemParser.ParseItems(currentRoom, room, doc);
                enemyNPCParser.ParseEnemies(currentRoom, room, doc);
                blockParser.ParseBlocks(currentRoom, room, doc);
                doorParser.ParseDoors(currentRoom, room, doc);

                roomHandler.rooms.Add(roomTup, currentRoom);
            }
        }
    }
}
