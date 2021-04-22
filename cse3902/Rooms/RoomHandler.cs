using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;

namespace cse3902.Rooms
{
    public class RoomHandler
    {
        private const int ROOM_WIDTH = RoomUtilities.ROOM_WIDTH;
        private const int ROOM_HEIGHT = RoomUtilities.ROOM_HEIGHT;

        private Game1 game;
        public Dictionary<Vector3, Room> rooms;

        private XMLParser xmlParser;

        private Camera camera;
        public RoomTransitionManager roomTransitionManager;

        public Vector3 currentRoom { get; set; }
        private Dictionary<int,Vector3> startingRooms;
        public Vector3 startingRoomTranslation { get; }
        private bool startComplete;

        public RoomHandler(Game1 gm)
        {
            this.game = gm;
            rooms = new Dictionary<Vector3, Room>();
            xmlParser = new XMLParser(this, game);
            roomTransitionManager = new RoomTransitionManager(game);
            camera = game.Camera;
            startingRooms = new Dictionary<int, Vector3>()
            {
                {0, new Vector3(2,6,0) },
                {2, new Vector3(0,1,2) },
                {4, new Vector3(0,2,4) },
                {6, new Vector3(0,0,6) }
            };
            currentRoom = startingRooms[0];
            startingRoomTranslation = new Vector3(0, -1, 0);
            startComplete = false;
        }

        public void Initialize()
        {
            String level1 = "XMLParsing/Level1.xml";
            String level2 = "XMLParsing/Level2.xml";
            String level3 = "XMLParsing/Level3.xml";
            String level4 = "XMLParsing/Level4.xml";

            xmlParser.ParseXML(level1);
            xmlParser.ParseXML(level2);
            xmlParser.ParseXML(level3);
            xmlParser.ParseXML(level4);
        }

        public void LoadNewRoom(Vector3 newPos, IDoor entranceDoor)
        {
            if(this.currentRoom.Z != newPos.Z && newPos.Z >= 0)
            {
                DungeonMask.Instance.LoadNextMask(((int)newPos.Z) / 2);
            }

            Room newRoom = rooms.GetValueOrDefault(newPos);
            Vector2 convertedRoom = RoomUtilities.ConvertVector(newPos);

            if (currentRoom.Z == newPos.Z && startComplete) camera.SmoothMoveCamera(convertedRoom, RoomUtilities.CAMERA_CYCLES);
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

            List<ISpawner> oldSpawners = rooms.GetValueOrDefault(currentRoom).Spawners;
            RoomSpawners.Instance.LoadNewRoom(ref oldSpawners, newRoom.Spawners);
            rooms.GetValueOrDefault(currentRoom).Spawners = oldSpawners;

            RoomConditions.Instance.LeaveRoom();
            List<ICondition> oldConditions = rooms.GetValueOrDefault(currentRoom).Conditions;
            RoomConditions.Instance.LoadNewRoom(ref oldConditions, newRoom.Conditions);
            rooms.GetValueOrDefault(currentRoom).Conditions = oldConditions;
            RoomConditions.Instance.EnterRoom();

            RoomProjectiles.Instance.Reset();

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
                if (rooms.ContainsKey(roomChange) && !roomChange.Equals(startingRooms[0])) LoadNewRoom(roomChange, rooms.GetValueOrDefault(roomChange).Doors[i]);
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
                RoomSpawners.Instance.Update(gameTime);
            }

            RoomDoors.Instance.Update(gameTime);
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
            rooms.GetValueOrDefault(startingRooms[0] + startingRoomTranslation).Doors[0].ConnectedDoor = null;
            rooms.GetValueOrDefault(startingRooms[0] + startingRoomTranslation).Doors[0].State = IDoor.DoorState.Wall;
        }

        public void Reset(bool healthReset, bool deathReset)
        {
            game.Camera.Reset();
            game.Player.Reset(healthReset);
            startComplete = false;

            if (currentRoom.Z == 0 || currentRoom.Z == -1 || !deathReset)
            {
                rooms.GetValueOrDefault(startingRooms[0] + startingRoomTranslation).Doors[0].State = IDoor.DoorState.Open;
                LoadNewRoom(startingRooms[0] + startingRoomTranslation, rooms.GetValueOrDefault(startingRooms[0] + startingRoomTranslation).Doors[0]);
            }
            else
            {
                LoadNewRoom(startingRooms[(int)currentRoom.Z], rooms.GetValueOrDefault(startingRooms[(int)currentRoom.Z]).Doors[4]);
            }

            foreach (Room room in rooms.Values)
            {
                room.Reset();
            }
        }
    }
}
