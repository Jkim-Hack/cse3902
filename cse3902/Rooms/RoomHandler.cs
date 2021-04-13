using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.HUD;
using cse3902.Doors;

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

        public Vector3 currentRoom { get; set; }
        private Vector3 previousRoom;
        private Vector3 startingRoom;
        public Vector3 startingRoomTranslation { get; }
        private bool startComplete;

        private String url;

        public RoomHandler(Game1 gm)
        {
            this.game = gm;
            rooms = new Dictionary<Vector3, Room>();
            xmlParser = new XMLParser(this, game);
            roomTransitionManager = new RoomTransitionManager(game);
            camera = game.Camera;
            startingRoom = new Vector3(2, 6, 0);
            currentRoom = startingRoom;
            startingRoomTranslation = new Vector3(0, -1, 0);
            startComplete = false;
        }

        public void Initialize()
        {
            url = "XMLParsing/Room1.xml";
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

            List<ITrap> oldTraps = rooms.GetValueOrDefault(currentRoom).Traps;
            RoomTraps.Instance.LoadNewRoom(ref oldTraps, newRoom.Traps, game);
            rooms.GetValueOrDefault(currentRoom).Traps = oldTraps;

            List<INPC> oldNPCs = rooms.GetValueOrDefault(currentRoom).NPCs;
            RoomNPCs.Instance.LoadNewRoom(ref oldNPCs, newRoom.NPCs);
            rooms.GetValueOrDefault(currentRoom).NPCs = oldNPCs;

            List<IBlock> oldBlocks = rooms.GetValueOrDefault(currentRoom).Blocks;
            RoomBlocks.Instance.LoadNewRoom(ref oldBlocks, newRoom.Blocks);
            rooms.GetValueOrDefault(currentRoom).Blocks = oldBlocks;

            List<IDoor> oldDoors = rooms.GetValueOrDefault(currentRoom).Doors;
            RoomDoors.Instance.LoadNewRoom(ref oldDoors, newRoom.Doors);
            rooms.GetValueOrDefault(currentRoom).Doors = oldDoors;

            RoomConditions.Instance.LeaveRoom();
            List<ICondition> oldConditions = rooms.GetValueOrDefault(currentRoom).Conditions;
            RoomConditions.Instance.LoadNewRoom(ref oldConditions, newRoom.Conditions);
            rooms.GetValueOrDefault(currentRoom).Conditions = oldConditions;
            RoomConditions.Instance.EnterRoom();

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
                //if (rooms.ContainsKey(roomChange) && !roomChange.Equals(startingRoom) && rooms.GetValueOrDefault(roomChange).Doors[i].State != IDoor.DoorState.Wall) LoadNewRoom(roomChange, rooms.GetValueOrDefault(roomChange).Doors[i]);
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
                RoomTraps.Instance.Update(gameTime);
                RoomNPCs.Instance.Update(gameTime);
                RoomProjectiles.Instance.Update(gameTime);
                RoomConditions.Instance.Update(gameTime);
            }

            Background.Instance.Update(gameTime);
            RoomBlocks.Instance.Update(gameTime);
        }
        public void Draw()
        {
            if (roomTransitionManager.IsTransitioning())
            {
                RoomDoors.Instance.DrawOld();
                RoomBlocks.Instance.DrawOld();
            }
            else if (!GameStateManager.Instance.InMenu(true) && !GameStateManager.Instance.IsDying())
            {
                RoomItems.Instance.Draw();
                CloudAnimation.Instance.Draw();
                RoomEnemies.Instance.Draw();
                RoomTraps.Instance.Draw();
                RoomNPCs.Instance.Draw();
                RoomProjectiles.Instance.Draw();
            }

            RoomDoors.Instance.Draw();
            RoomBlocks.Instance.Draw();
            Background.Instance.Draw();
        }

        public void CompleteStart()
        {
            startComplete = true;
            rooms.GetValueOrDefault(startingRoom + startingRoomTranslation).Doors[0].ConnectedDoor = null;
            rooms.GetValueOrDefault(startingRoom + startingRoomTranslation).Doors[0].State = IDoor.DoorState.Wall;
        }

        public void Reset(bool healthReset)
        {
            game.Camera.Reset();
            game.Player.Reset(healthReset);
            startComplete = false;
            rooms.GetValueOrDefault(startingRoom + startingRoomTranslation).Doors[0].State = IDoor.DoorState.Open;
            LoadNewRoom(startingRoom + startingRoomTranslation, rooms.GetValueOrDefault(startingRoom + startingRoomTranslation).Doors[0]);

            foreach (Room room in rooms.Values)
            {
                room.Reset();
            }
        }
    }
}
