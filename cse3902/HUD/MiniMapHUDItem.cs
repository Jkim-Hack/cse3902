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
            this.currentRoom = new Vector3(0, -1, 0);

            this.offsetX = 125;
            this.offsetY = game.GraphicsDevice.Viewport.Height - 30;

            this.alreadyChanged = false;
        }

        public void Update()
        {
            if (game.RoomHandler.roomTransitionManager.IsTransitioning() && !alreadyChanged)
            {
                currentRoom += (game.RoomHandler.RoomChangeDirection * new Vector3(1, -1, 1));
                Console.WriteLine(currentRoom);
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
            DrawRectangle(new Rectangle(0 - offsetX, 0 - offsetY, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.Black);

            /* Draw whole map first, then current room */
            foreach(Rectangle rec in MiniMapConstants.GetRoomLayout()) DrawRectangle(rec, MiniMapConstants.roomColor);

            Rectangle currentRoomRectangle = MiniMapConstants.CalculatePos((int)currentRoom.X, (int)currentRoom.Y, MiniMapConstants.greenSize, MiniMapConstants.greenSize);
            currentRoomRectangle.X += 5;
            DrawRectangle(currentRoomRectangle, MiniMapConstants.currentRoomColor);
        }

        private void DrawRectangle(Rectangle rec, Color color)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + offsetX, rec.Y + offsetY, rec.Width, rec.Height), color);
        }
    }
}
