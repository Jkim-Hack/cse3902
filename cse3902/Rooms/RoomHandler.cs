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
        public const int ROOM_WIDTH = RoomUtilities.ROOM_WIDTH;
        public const int ROOM_HEIGHT = RoomUtilities.ROOM_HEIGHT;
        public const int NUM_ROOMS_X = RoomUtilities.NUM_ROOMS_X;
        public const int NUM_ROOMS_Y = RoomUtilities.NUM_ROOMS_Y;
        public const int CAMERA_CYCLES = RoomUtilities.CAMERA_CYCLES;

        public Dictionary<Vector3, Room> rooms;

        private XMLParser xmlParser;

        private Camera camera;
        private RoomTransitionManager roomTransitionManager;

        public Vector3 currentRoom { get; set; }

        public RoomHandler(Game1 game)
        {
            rooms = new Dictionary<Vector3, Room>();
            xmlParser = new XMLParser(this, game.spriteBatch);
            roomTransitionManager = new RoomTransitionManager(game);
            this.camera = game.camera;
        }

        public void Initialize()
        {
            String url = "https://raw.githubusercontent.com/Jkim-Hack/cse3902/file-input/cse3902/Rooms/Room1.xml";
            xmlParser.parseXML(url);
        }

        public void LoadNewRoom(Vector3 newPos, IDoor entranceDoor)
        {
            Room newRoom = rooms.GetValueOrDefault(newPos);

            if (currentRoom.Z == newPos.Z)
            {
                camera.SmoothMoveCamera(new Vector2((newPos.X + (NUM_ROOMS_X * newPos.Z)) * ROOM_WIDTH, newPos.Y * ROOM_HEIGHT), CAMERA_CYCLES);
            }
            else
            {
                camera.MoveCamera(new Vector2( (newPos.X + (NUM_ROOMS_X * newPos.Z)) * ROOM_WIDTH , newPos.Y * ROOM_HEIGHT), new Vector2(ROOM_WIDTH, ROOM_HEIGHT));
            }
            
            List<IItem> oldItems = rooms.GetValueOrDefault(currentRoom).Items;
            RoomItems.Instance.LoadNewRoom(ref oldItems, newRoom.Items);
            rooms.GetValueOrDefault(currentRoom).Items = oldItems;

            List<IEntity> oldEnemies = rooms.GetValueOrDefault(currentRoom).Enemies;
            RoomEnemyNPCs.Instance.LoadNewRoom(ref oldEnemies, newRoom.Enemies);
            rooms.GetValueOrDefault(currentRoom).Enemies = oldEnemies;

            currentRoom = newPos;
            rooms.GetValueOrDefault(newPos).SetToVisited();

            roomTransitionManager.StartTransitionManager(entranceDoor);
        }

        public void Update()
        {
            if (roomTransitionManager.IsTransitioning()) roomTransitionManager.Update();
            else
            {
                //update things normally
            }
        }
        public void Draw()
        {
            if (!roomTransitionManager.IsTransitioning())
            {
                //draw things normally
            }
        }
    }
}
