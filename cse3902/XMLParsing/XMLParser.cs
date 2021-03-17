﻿using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using cse3902.XMLParsing;
using System.IO;

namespace cse3902.Rooms
{
    public class XMLParser
    {
        private RoomHandler roomHandler;
        private ItemParser itemParser;
        private EnemyParser enemyParser;
        private NPCParser npcParser;
        private BlockParser blockParser;
        private DoorParser doorParser;

        public XMLParser(RoomHandler roomHand, Game1 gm)
        {
            roomHandler = roomHand;
            itemParser = new ItemParser(gm);
            enemyParser = new EnemyParser(gm);
            npcParser = new NPCParser(gm);
            blockParser = new BlockParser(gm);
            doorParser = new DoorParser(gm, roomHandler);
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

                currentRoom = new Room(roomTup, spriteNum);

                itemParser.ParseItems(currentRoom, room, doc);
                enemyParser.ParseEnemies(currentRoom, room, doc);
                npcParser.ParseNPCs(currentRoom, room, doc);
                blockParser.ParseBlocks(currentRoom, room, doc);
                doorParser.ParseDoors(currentRoom, room, doc);

                roomHandler.rooms.Add(roomTup, currentRoom);
            }
        }
    }
}
