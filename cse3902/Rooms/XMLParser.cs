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

        public IDoor createDoor(String type, String dir, Vector2 startingPos)
        {
            IDoor newDoor = null;
            switch (type)
            {

            }

            return newDoor;
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

            XName enemies = XName.Get("enemies", doc.Root.Name.NamespaceName);
            foreach (var enemy in xmlElem.Elements(enemies))
            {
                XName type = XName.Get("type", doc.Root.Name.NamespaceName);

                XName xloc = XName.Get("xloc", doc.Root.Name.NamespaceName);
                XName yloc = XName.Get("yloc", doc.Root.Name.NamespaceName);

                int x = Int32.Parse(enemy.Element(xloc).Value);
                int y = Int32.Parse(enemy.Element(yloc).Value);

                IEntity enemyAdd = createEnemy(enemy.Element(type).Value, new Vector2(x, y));
                room.AddEnemy(enemyAdd);
            }

        }

        public void parseBlocks(Room room, XElement xmlElem, XDocument doc)
        {
            XName blocks = XName.Get("blocks", doc.Root.Name.NamespaceName);
            foreach (var block in xmlElem.Elements(blocks))
            {
                XName type = XName.Get("type", doc.Root.Name.NamespaceName);
                XName dir = XName.Get("dir", doc.Root.Name.NamespaceName);
                XName xloc = XName.Get("xloc", doc.Root.Name.NamespaceName);
                XName yloc = XName.Get("yloc", doc.Root.Name.NamespaceName);

                int x = Int32.Parse(block.Element(xloc).Value);
                int y = Int32.Parse(block.Element(yloc).Value);

                IBlock blockAdd = createBlock(block.Element(type).Value, block.Element(dir).Value, new Vector2(x, y));
                room.AddBlock(blockAdd);
            }

        }

        public void parseDoors(Room room, XElement xmlElem, XDocument doc)
        {
            XName doors = XName.Get("doors", doc.Root.Name.NamespaceName);
            foreach (var door in xmlElem.Elements(doors))
            {
                XName type = XName.Get("type", doc.Root.Name.NamespaceName);
                XName dir = XName.Get("dir", doc.Root.Name.NamespaceName);
                XName xloc = XName.Get("xloc", doc.Root.Name.NamespaceName);
                XName yloc = XName.Get("yloc", doc.Root.Name.NamespaceName);

                int x = Int32.Parse(door.Element(xloc).Value);
                int y = Int32.Parse(door.Element(yloc).Value);

                IDoor doorAdd = createDoor(door.Element(type).Value, door.Element(dir).Value, new Vector2(x, y));
                room.AddDoor(doorAdd);
            }

        }

        public void parseXML(String filename)
        {
            XDocument doc = XDocument.Load(filename);
            XElement map = XElement.Load(filename);

            XName roomName = XName.Get("rooms", doc.Root.Name.NamespaceName);

            foreach (var room in map.Elements(roomName))
            {
                Room currentRoom;

                XName chil = XName.Get("room", doc.Root.Name.NamespaceName);
                XName num = XName.Get("number", doc.Root.Name.NamespaceName);

                int comma = room.Element(num).Value.IndexOf(',');
                int comma2 = room.Element(num).Value.IndexOf(',', comma + 1);

                Vector3 roomTup = new Vector3(Int32.Parse(room.Element(num).Value.Substring(0, comma)), Int32.Parse(room.Element(num).Value.Substring(comma + 1, comma2)), Int32.Parse(room.Element(num).Value.Substring(comma2)));

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
