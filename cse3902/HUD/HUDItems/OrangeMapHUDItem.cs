using cse3902.Interfaces;
using cse3902.Constants;
using cse3902.Utilities;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.HUD
{
    public class OrangeMapHUDItem : IHUDItem
    {
        private Game1 game;

        private Texture2D map;
        private Rectangle mapPos;
        private int scaledMapWidth;
        private int scaledMapHeight;

        private Texture2D roomsTexture;
        private Rectangle[] roomFrames;

        private Dictionary<Vector3, int> rooms;

        private int offsetX;
        private int offsetY;

        private bool alreadyChanged;

        public OrangeMapHUDItem(Game1 game, Texture2D map, Texture2D roomsTexture)
        {
            this.game = game;

            this.map = map;
            this.roomsTexture = roomsTexture;

            this.offsetX = (int)(DimensionConstants.OriginalWindowWidth / 2.25f);
            this.offsetY = (int)(DimensionConstants.OriginalWindowHeight / 2.15f);

            this.scaledMapWidth = (int)(map.Bounds.Width / 1.3f);
            this.scaledMapHeight = (int)(map.Bounds.Height / 1.3f);

            this.mapPos = new Rectangle(0, 0, scaledMapWidth, scaledMapHeight);

            this.roomFrames = SpriteUtilities.distributeFrames(16, 1, 8, 8);
            this.rooms = new Dictionary<Vector3, int>();

            this.alreadyChanged = false;
        }

        public int Update(GameTime gameTime)
        {
            if (game.RoomHandler.currentRoom.Z == 0 && !rooms.ContainsKey(game.RoomHandler.currentRoom))
            {
                /* change to add correct # for doors */
                rooms.Add(game.RoomHandler.currentRoom, GetRoomIndex(game.RoomHandler.rooms[game.RoomHandler.currentRoom].Doors));
            }

            return 0;
        }

        private int GetRoomIndex(List<IDoor> roomDoors)
        {
            /* List of doors is modeled as (bottom, left, top, right) */
            /* int represents a 4-bit integer, modeled as (left, right, top, bottom), with 0 meaning closed and 1 meaning open */
            int doors = 0;

            doors |= IsOpen(roomDoors[1]);
            doors <<= 1;
            doors |= IsOpen(roomDoors[3]);
            doors <<= 1;
            doors |= IsOpen(roomDoors[2]);
            doors <<= 1;
            doors |= IsOpen(roomDoors[0]);

            return doors;
        }

        private int IsOpen(IDoor door)
        {
            if (door.State == IDoor.DoorState.Open || door.State == IDoor.DoorState.Bombed) return 0b1;
            else return 0b0;
        }

        public void Draw()
        {
            DrawMap();
            DrawRooms();
            if (game.RoomHandler.currentRoom.Z == 0) DrawCurrentRoom();
        }

        private void DrawMap()
        {
            HUDUtilities.DrawTexture(game, map, mapPos, offsetX, offsetY, HUDUtilities.OrangeMapLayer);
        }

        private void DrawRooms()
        {
            // foreach (Vector3 coords in rooms.Keys)
            // {
            //     Rectangle destination = OrangeMapConstants.CalculatePos(coords, OrangeMapConstants.RoomSize, scaledMapWidth, scaledMapHeight);
            //     HUDUtilities.DrawTexture(game, roomsTexture, destination, offsetX, offsetY, HUDUtilities.OrangeMapRoomLayer, roomFrames[rooms[coords]]);
            // }
            foreach (Vector3 coords in game.RoomHandler.rooms.Keys)
            {
                if (game.RoomHandler.rooms[coords].Doors.Count != 4) continue;
                Rectangle frame = roomFrames[GetRoomIndex(game.RoomHandler.rooms[coords].Doors)];
                Rectangle destination = OrangeMapConstants.CalculatePos(coords, OrangeMapConstants.RoomSize, scaledMapWidth, scaledMapHeight);
                HUDUtilities.DrawTexture(game, roomsTexture, destination, offsetX, offsetY, HUDUtilities.OrangeMapRoomLayer, frame);
            }
        }

        private void DrawCurrentRoom()
        {
            Rectangle destination = OrangeMapConstants.CalculatePos(game.RoomHandler.currentRoom, OrangeMapConstants.CurrentRoomSize, scaledMapWidth, scaledMapHeight);
            HUDUtilities.DrawRectangle(game, destination, OrangeMapConstants.CurrentRoomColor, offsetX, offsetY, HUDUtilities.OrangeMapCurrentRoomLayer);
        }

        public void Erase() {} // needs to be deleted once isprite is updated

        public Vector2 Center {

            get => new Vector2(offsetX, offsetY);

            set {
                offsetX = (int) value.X;
                offsetY = (int) value.Y;
            }
        }

        public Texture2D Texture {

            get => map;
        }

        public ref Rectangle Box {

            get => ref mapPos;
        }
    }
}
