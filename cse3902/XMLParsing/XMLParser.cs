﻿using System.Collections.Generic;
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

        public XMLParser(RoomHandler roomHand, Game1 gm)
        {
            roomHandler = roomHand;
            game = gm;
            itemParser = new ItemParser(gm);
            enemyNPCParser = new EnemyNPCParser(gm);
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

                Vector3 connectingRoom = convertToVector3(connRoom.Value);
                HandleDoorConnection(roomobj.roomPos, connectingRoom);

                // TODO: update once the Vector3 to Vector2 method is written
                Vector2 center = RoomUtilities.calculateDoorCenter(new Vector2(roomobj.roomPos.X, roomobj.roomPos.Y), FindDoorPos(typeName.Value));

                IDoor doorAdd = createDoor(typeName.Value, center);
                roomobj.AddDoor(doorAdd);
            }
        }

        private void LinkDoors()
        {

        }


        private void HandleDoorConnection(Vector3 currRoom, Vector3 connectingRoom)
        {

        }


        private RoomUtilities.DoorPos FindDoorPos (String pos)
        {
            RoomUtilities.DoorPos doorPos = RoomUtilities.DoorPos.Down;
            switch (pos)
            {
                case "Up":
                    doorPos = RoomUtilities.DoorPos.Up;
                    break;
                case "Down":
                    doorPos = RoomUtilities.DoorPos.Down;
                    break;
                case "Left":
                    doorPos = RoomUtilities.DoorPos.Left;
                    break;
                case "Right":
                    doorPos = RoomUtilities.DoorPos.Right;
                    break;
                case "StairDown":
                    doorPos = RoomUtilities.DoorPos.Down;
                    break;
                case "OffscreenUp":
                    doorPos = RoomUtilities.DoorPos.Up;
                    break;
                default:
                    break;
            }

            return doorPos;
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

                itemParser.parseItems(currentRoom, room, doc);
                enemyNPCParser.parseEnemies(currentRoom, room, doc);
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
