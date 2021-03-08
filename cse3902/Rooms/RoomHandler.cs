﻿using System.Collections.Generic;
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
        public const int ROOM_WIDTH = 252;
        public const int ROOM_HEIGHT = 172;
        public const int CAMERA_CYCLES = 30;

        private Camera camera;
        public Dictionary<Vector3, Room> rooms;

        private XMLParser xmlParser;

        public Vector3 currentRoom { get; set; }

        public RoomHandler(SpriteBatch sb, Camera cam)
        {
            rooms = new Dictionary<Vector3, Room>();
            camera = cam;
            xmlParser = new XMLParser(this, sb);
        }

        public void Initialize()
        {
            String url = "https://raw.githubusercontent.com/Jkim-Hack/cse3902/file-input/cse3902/Rooms/Room1.xml";
            xmlParser.parseXML(url);
        }

        public void LoadNewRoom(Vector3 newPos)
        {
            Room newRoom = rooms.GetValueOrDefault(newPos);

            if (currentRoom.Z == newPos.Z)
            {
                camera.SmoothMoveCamera(new Vector2( (newPos.X - currentRoom.X) * ROOM_WIDTH, (newPos.Y - currentRoom.Y) * ROOM_HEIGHT), CAMERA_CYCLES);
            }
            else
            {
                camera.MoveCamera(new Vector2( (newPos.X - (ROOM_WIDTH * newPos.Z)) * ROOM_WIDTH , newPos.Y * ROOM_HEIGHT), ROOM_WIDTH, ROOM_HEIGHT);
            }
            
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
