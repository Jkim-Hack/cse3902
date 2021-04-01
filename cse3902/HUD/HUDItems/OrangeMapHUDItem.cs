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
        private Vector3 currentRoom;

        private Texture2D map;
        private Rectangle mapPos;
        private int scaledMapWidth;
        private int scaledMapHeight;

        private Texture2D roomsTexture;
        private Rectangle[] roomFrames;
        
        private int offsetX;
        private int offsetY;

        private bool alreadyChanged;

        public OrangeMapHUDItem(Game1 game, Texture2D map, Texture2D roomsTexture)
        {
            this.game = game;
            this.currentRoom = game.RoomHandler.currentRoom;
            this.currentRoom.Y++;

            this.map = map;
            this.roomsTexture = roomsTexture;

            this.offsetX = (int)(DimensionConstants.OriginalWindowWidth / 2.25f);
            this.offsetY = (int)(DimensionConstants.OriginalWindowHeight / 2.15f);

            this.scaledMapWidth = (int)(map.Bounds.Width / 1.3f);
            this.scaledMapHeight = (int)(map.Bounds.Height / 1.3f);

            this.mapPos = new Rectangle(0, 0, scaledMapWidth, scaledMapHeight);

            this.roomFrames = SpriteUtilities.distributeFrames(16, 1, 8, 8);

            this.alreadyChanged = false;
        }

        public int Update(GameTime gameTime)
        {
            return 0;
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
            HUDUtilities.DrawTexture(game, roomsTexture, GetRoomPosition(new Vector3(2, 5, 0)), 0, 0, HUDUtilities.OrangeMapRoomLayer, roomFrames[15]);
            HUDUtilities.DrawTexture(game, roomsTexture, GetRoomPosition(new Vector3(1, 5, 0)), 0, 0, HUDUtilities.OrangeMapRoomLayer, roomFrames[15]);
            HUDUtilities.DrawTexture(game, roomsTexture, GetRoomPosition(new Vector3(3, 5, 0)), 0, 0, HUDUtilities.OrangeMapRoomLayer, roomFrames[15]);
        }

        private void DrawCurrentRoom()
        {
            HUDUtilities.DrawRectangle(game, GetRoomPosition(currentRoom), Color.White, offsetX, offsetY, HUDUtilities.OrangeMapCurrentRoomLayer);
        }

        private Rectangle GetRoomPosition(Vector3 coords)
        {
            int scaled = (int)(8 / 1.5f);
            int x = (int)(offsetX + scaledMapWidth / 2 + (coords.X - 2) * scaled);
            int y = (int)(offsetY + scaledMapHeight / 1.5f + (coords.Y - 5) * scaled);
            return new Rectangle(x, y, scaled, scaled);
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
