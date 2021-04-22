using cse3902.Interfaces;
using cse3902.Constants;
using cse3902.Utilities;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
        }

        public int Update(GameTime gameTime)
        {
            if (game.RoomHandler.currentRoom.Z == 0) rooms[game.RoomHandler.currentRoom] = 0;

            foreach(Vector3 coords in game.RoomHandler.rooms.Keys)
            {
                if (rooms.ContainsKey(coords)) rooms[coords] = GetRoomIndex(game.RoomHandler.rooms[coords].Doors);
            }

            return 0;
        }

        private int GetRoomIndex(List<IDoor> roomDoors)
        {
            /* List of doors is modeled as (bottom, left, top, right) */
            /* int represents a 4-bit integer, modeled as (left, right, top, bottom), with 0 meaning closed and 1 meaning open */
            int doors = 0;
            doors = IsOpen(roomDoors[1], 0) | IsOpen(roomDoors[3], 1) | IsOpen(roomDoors[2], 2) | IsOpen(roomDoors[0], 3);
            return doors;
        }

        private int IsOpen(IDoor door, int index)
        {
            if (door.State == IDoor.DoorState.Open || door.State == IDoor.DoorState.Bombed) return 0b0001 << (3 - index);
            else return 0b0000;
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
