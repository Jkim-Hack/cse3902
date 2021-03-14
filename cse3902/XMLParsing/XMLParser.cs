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
using cse3902.XMLParsing;
using System.Linq;

namespace cse3902.Rooms
{
    public class XMLParser
    {
        private RoomHandler roomHandler;
        private Game1 game;
        private ItemParser itemParser;
        private EnemyNPCParser enemyNPCParser;
        private BlockParser blockParser;
        private DoorParser doorParser;

        public XMLParser(RoomHandler roomHand, Game1 gm)
        {
            roomHandler = roomHand;
            game = gm;
            itemParser = new ItemParser(gm);
            enemyNPCParser = new EnemyNPCParser(gm);
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

                Vector3 roomTup = RoomUtilities.convertToVector3(room.Element(num).Value);

                currentRoom = new Room(roomTup);

                itemParser.parseItems(currentRoom, room, doc);
                enemyNPCParser.parseEnemies(currentRoom, room, doc);
                blockParser.parseBlocks(currentRoom, room, doc);
                doorParser.parseDoors(currentRoom, room, doc);

                roomHandler.rooms.Add(roomTup, currentRoom);

                if (roomHandler.rooms.Count == 1)
                {
                    roomHandler.currentRoom = roomTup;
                }
            }
        }
    }
}
