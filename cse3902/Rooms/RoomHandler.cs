using System.Collections.Generic;
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

        public Dictionary<Vector3, Room> rooms;

        private XMLParser xmlParser;

        private Camera camera;
        public RoomTransitionManager roomTransitionManager;

        public Vector3 currentRoom { get; set; }
        private Vector3 previousRoom;
        private Vector3 startingRoom;
        public Vector3 startingRoomTranslation { get; }
        private bool startComplete;

        public RoomHandler(Game1 game)
        {
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
            RoomEnemyNPCs.Instance.LoadNewRoom(ref oldEnemies, newRoom.Enemies);
            rooms.GetValueOrDefault(currentRoom).Enemies = oldEnemies;

            List<IBlock> oldBlocks = rooms.GetValueOrDefault(currentRoom).Blocks;
            RoomBlocks.Instance.LoadNewRoom(ref oldBlocks, newRoom.Blocks);
            rooms.GetValueOrDefault(currentRoom).Blocks = oldBlocks;

            List<IDoor> oldDoors = rooms.GetValueOrDefault(currentRoom).Doors;
            RoomDoors.Instance.LoadNewRoom(ref oldDoors, newRoom.Doors);
            rooms.GetValueOrDefault(currentRoom).Doors = oldDoors;

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
                if (rooms.ContainsKey(roomChange)) LoadNewRoom(roomChange, rooms.GetValueOrDefault(roomChange).Doors[i]);
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
                RoomEnemyNPCs.Instance.Update(gameTime);
                RoomProjectiles.Instance.Update(gameTime);
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
                RoomEnemyNPCs.Instance.Draw();
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
