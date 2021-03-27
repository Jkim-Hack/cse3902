using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.HUD
{
    public class MiniMapHUDItem
    {
        private Game1 game;

        private Vector3 currentRoom;
        
        private int offsetX;
        private int offsetY;

        private Boolean alreadyChanged;

        public MiniMapHUDItem(Game1 game)
        {
            this.game = game;
            this.currentRoom = game.RoomHandler.currentRoom;

            this.offsetX = DimensionConstants.Scale * 40;
            this.offsetY = DimensionConstants.WindowHeight - 40 * DimensionConstants.Scale;

            this.alreadyChanged = false;
        }

        public void Update()
        {
            if (game.RoomHandler.roomTransitionManager.IsTransitioning() && !alreadyChanged)
            {
                currentRoom += game.RoomHandler.RoomChangeDirection;
                alreadyChanged = true;
            }

            if (!game.RoomHandler.roomTransitionManager.IsTransitioning())
            {
                alreadyChanged = false;
            }
        }

        public void Draw()
        {
            /* Draw background (just for testing) */
            DrawRectangle(new Rectangle(0 - offsetX, 0 - offsetY, DimensionConstants.WindowWidth,DimensionConstants.WindowHeight), Color.Black);

            /* Draw entire map */
            foreach(Rectangle rec in MiniMapConstants.GetRoomLayout()) DrawRectangle(rec, MiniMapConstants.RoomColor);

            /* Only draw current room square if not in item room */
            if (currentRoom.Z == 0)
            {
                Rectangle currentRoomRectangle = MiniMapConstants.CalculatePos((int)currentRoom.X, (int)currentRoom.Y, MiniMapConstants.Width, MiniMapConstants.Height);
                currentRoomRectangle.X += (MiniMapConstants.Width - MiniMapConstants.Height) / 2;
                currentRoomRectangle.Width = MiniMapConstants.Height;
                DrawRectangle(currentRoomRectangle, MiniMapConstants.CurrentRoomColor);
            }
        }

        private void DrawRectangle(Rectangle rec, Color color)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + offsetX, rec.Y + offsetY, rec.Width, rec.Height), color);
        }
    }
}
