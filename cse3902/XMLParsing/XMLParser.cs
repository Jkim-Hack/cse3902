using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.XMLParsing;
using System.IO;
using cse3902.Constants;

namespace cse3902.Rooms
{
    public class XMLParser
    {
        private RoomHandler roomHandler;
        private ItemParser itemParser;
        private EnemyParser enemyParser;
        private TrapsParser trapParser;
        private NPCParser npcParser;
        private BlockParser blockParser;
        private DoorParser doorParser;
        private ConditionParser conditionParser;
        private SpawnerParser spawnerParser;

        public XMLParser(RoomHandler roomHand, Game1 gm)
        {
            roomHandler = roomHand;
            itemParser = new ItemParser(gm);
            enemyParser = new EnemyParser(gm);
            trapParser = new TrapsParser(gm);
            npcParser = new NPCParser(gm);
            blockParser = new BlockParser(gm);
            doorParser = new DoorParser(gm, roomHandler);
            conditionParser = new ConditionParser();
            spawnerParser = new SpawnerParser(gm);
        }

        public void ParseXML(String filename)
        {
            String workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            XDocument doc = XDocument.Load(workingDirectory + "/" + filename);
            XElement map = XElement.Load(workingDirectory + "/" + filename);

            XName roomName = XName.Get("room", doc.Root.Name.NamespaceName);

            foreach (XElement room in map.Elements(roomName))
            {
                Room currentRoom;

                XName num = XName.Get("number", doc.Root.Name.NamespaceName);
                XName sprite = XName.Get("sprite", doc.Root.Name.NamespaceName);
                int spriteNum = Int32.Parse(room.Element(sprite).Value);

                Vector3 roomTup = RoomUtilities.ConvertToVector3(room.Element(num).Value);
                if (roomTup.Z == 0) MiniMapConstants.RoomListZ0.Add(new Vector2(roomTup.X, roomTup.Y));

                currentRoom = new Room(roomTup, spriteNum);

                itemParser.ParseItems(currentRoom, room, doc);
                enemyParser.ParseEnemies(currentRoom, room, doc);
                trapParser.ParseTraps(currentRoom, room, doc);
                npcParser.ParseNPCs(currentRoom, room, doc);
                blockParser.ParseBlocks(currentRoom, room, doc);
                doorParser.ParseDoors(currentRoom, room, doc);
                conditionParser.ParseCondtions(currentRoom, room, doc);
                //spawnerParser.ParseSpawners(currentRoom, room, doc);

                roomHandler.rooms.Add(roomTup, currentRoom);
            }

            MiniMapConstants.RoomListZ0.Remove(new Vector2(roomHandler.currentRoom.X, roomHandler.currentRoom.Y));
        }
    }
}
