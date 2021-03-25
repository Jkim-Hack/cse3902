﻿using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Projectiles;

namespace cse3902.Rooms
{
    public class RoomHandler
    {
        public const int ROOM_WIDTH = RoomUtilities.ROOM_WIDTH;
        public const int ROOM_HEIGHT = RoomUtilities.ROOM_HEIGHT;
        public const int NUM_ROOMS_X = RoomUtilities.NUM_ROOMS_X;
        public const int NUM_ROOMS_Y = RoomUtilities.NUM_ROOMS_Y;
        public const int CAMERA_CYCLES = RoomUtilities.CAMERA_CYCLES;

        private Game1 game;
        public Dictionary<Vector3, Room> rooms;

        private XMLParser xmlParser;

        private Camera camera;
        public RoomTransitionManager roomTransitionManager;
        public RoomConditionList roomConditionList { get; }

        public Vector3 currentRoom { get; set; }
        private Vector3 previousRoom;
        private Vector3 startingRoom;
        public Vector3 startingRoomTranslation { get; }
        private bool startComplete;

        public RoomHandler(Game1 gm)
        {
            this.game = gm;
            rooms = new Dictionary<Vector3, Room>();
            xmlParser = new XMLParser(this, game);
            roomTransitionManager = new RoomTransitionManager(game);
            roomConditionList = new RoomConditionList(this);
            camera = game.Camera;
            startingRoom = new Vector3(2, 6, 0);
            currentRoom = startingRoom;
            startingRoomTranslation = new Vector3(0, -1, 0);
            startComplete = false;
        }

        public void Initialize()
        {
            String url = "XMLParsing/Room1.xml";
            xmlParser.ParseXML(url);
        }

        public void LoadNewRoom(Vector3 newPos, IDoor entranceDoor)
        {
            Room newRoom = rooms.GetValueOrDefault(newPos);
            Vector2 convertedRoom = RoomUtilities.ConvertVector(newPos);

            if (currentRoom.Z == newPos.Z && startComplete) camera.SmoothMoveCamera(convertedRoom, CAMERA_CYCLES);
            else camera.MoveCamera(convertedRoom, new Vector2(ROOM_WIDTH, ROOM_HEIGHT));

            List<IItem> oldItems = rooms.GetValueOrDefault(currentRoom).Items;
            RoomItems.Instance.LoadNewRoom(ref oldItems, newRoom.Items);
            rooms.GetValueOrDefault(currentRoom).Items = oldItems;

            List<IEntity> oldEnemies = rooms.GetValueOrDefault(currentRoom).Enemies;
            RoomEnemies.Instance.LoadNewRoom(ref oldEnemies, newRoom.Enemies, game);
            rooms.GetValueOrDefault(currentRoom).Enemies = oldEnemies;

            List<INPC> oldNPCs = rooms.GetValueOrDefault(currentRoom).NPCs;
            RoomNPCs.Instance.LoadNewRoom(ref oldNPCs, newRoom.NPCs);
            rooms.GetValueOrDefault(currentRoom).NPCs = oldNPCs;

            List<IBlock> oldBlocks = rooms.GetValueOrDefault(currentRoom).Blocks;
            RoomBlocks.Instance.LoadNewRoom(ref oldBlocks, newRoom.Blocks);
            rooms.GetValueOrDefault(currentRoom).Blocks = oldBlocks;

            List<IDoor> oldDoors = rooms.GetValueOrDefault(currentRoom).Doors;
            RoomDoors.Instance.LoadNewRoom(ref oldDoors, newRoom.Doors);
            rooms.GetValueOrDefault(currentRoom).Doors = oldDoors;

            List<RoomConditionList.CheckingCondition> oldConditions = rooms.GetValueOrDefault(currentRoom).Conditions;
            RoomConditions.Instance.LoadNewRoom(ref oldConditions, newRoom.Conditions);
            rooms.GetValueOrDefault(currentRoom).Conditions = oldConditions;

            previousRoom = currentRoom;
            currentRoom = newPos;
            rooms.GetValueOrDefault(newPos).SetToVisited();

            roomTransitionManager.StartTransitionManager(entranceDoor);
        }

        public void LoadNewRoom(Vector3 roomChange, int i)
        {
            if (!roomTransitionManager.IsTransitioning())
            {
                roomChange += currentRoom;
                if (rooms.ContainsKey(roomChange) && !roomChange.Equals(startingRoom)) LoadNewRoom(roomChange, rooms.GetValueOrDefault(roomChange).Doors[i]);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (roomTransitionManager.IsTransitioning())
            {
                roomTransitionManager.Update();
            }
            else
            {
                RoomItems.Instance.Update(gameTime);
                CloudAnimation.Instance.Update(gameTime);
                RoomEnemies.Instance.Update(gameTime);
                RoomNPCs.Instance.Update(gameTime);
                RoomProjectiles.Instance.Update(gameTime);
                RoomConditions.Instance.Update(gameTime);
            }

            RoomBackground.Instance.Update(gameTime);
            RoomBlocks.Instance.Update(gameTime);
        }
        public void Draw()
        {
            if (roomTransitionManager.IsTransitioning())
            {
                
            }
            else
            {
                RoomItems.Instance.Draw();
                CloudAnimation.Instance.Draw();
                RoomEnemies.Instance.Draw();
                RoomNPCs.Instance.Draw();
                RoomProjectiles.Instance.Draw();
            }

            RoomDoors.Instance.Draw();
            RoomBlocks.Instance.Draw();
            RoomBackground.Instance.Draw();
        }

        public void CompleteStart()
        {
            startComplete = true;
            rooms.GetValueOrDefault(startingRoom + startingRoomTranslation).Doors[0].State = IDoor.DoorState.Wall;
        }

        public void Reset()
        {
            ProjectileHandler.Instance.Reset();
        }
    }
}
