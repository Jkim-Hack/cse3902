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
        private RoomTransitionManager roomTransitionManager;

        public Vector3 currentRoom { get; set; }
        private Vector3 previousRoom;

        public RoomHandler(Game1 game)
        {
            rooms = new Dictionary<Vector3, Room>();
            xmlParser = new XMLParser(this, game);
            roomTransitionManager = new RoomTransitionManager(game);
            camera = game.camera;
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
                camera.MoveCamera(new Vector2((newPos.X + (NUM_ROOMS_X * newPos.Z)) * ROOM_WIDTH, newPos.Y * ROOM_HEIGHT), new Vector2(ROOM_WIDTH, ROOM_HEIGHT));
            }

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

        public void LoadNewRoom(Vector3 roomChange)
        {
            if (!roomTransitionManager.IsTransitioning())
            {
                roomChange += currentRoom;
                LoadNewRoom(roomChange, rooms.GetValueOrDefault(roomChange).Doors[0]);
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
                ProjectileHandler.Instance.Update(gameTime);
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
                ProjectileHandler.Instance.Draw();
            }

            RoomBlocks.Instance.Draw();
            RoomBackground.Instance.Draw();
        }

        public void Reset()
        {
            ProjectileHandler.Instance.Reset();
        }
    }
}
