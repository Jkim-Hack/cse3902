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

        /* Each room is stored as its coordinates and doors - int represents a 4-bit integer, modeled as (left, right, top, bottom), with 0 meaning closed and 1 meaning open */
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
                rooms.Add(game.RoomHandler.currentRoom, 15);
            }

            return 0;
        }

        public void UpdateRoomDoors(Vector3 coords, int newDoors)
        {
            rooms[coords] = newDoors;
        }

        public void Draw()
        {
            DrawMap();
            DrawRooms();
            DrawCurrentRoom();
        }

        private void DrawMap()
        {
            HUDUtilities.DrawTexture(game, map, mapPos, offsetX, offsetY, HUDUtilities.OrangeMapLayer);
        }

        private void DrawRooms()
        {
            foreach (Vector3 coords in rooms.Keys)
            {
                Rectangle destination = OrangeMapConstants.CalculatePos(coords, OrangeMapConstants.RoomSize, scaledMapWidth, scaledMapHeight);
                HUDUtilities.DrawTexture(game, roomsTexture, destination, offsetX, offsetY, HUDUtilities.OrangeMapRoomLayer, roomFrames[rooms[coords]]);
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
