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

        private Rectangle currentRoom;
        
        private int offsetX;
        private int offsetY;

        public MiniMapHUDItem(Game1 game)
        {
            this.game = game;
            this.currentRoom = new Rectangle();

            this.offsetX = 125;
            this.offsetY = game.GraphicsDevice.Viewport.Height - 30;
        }

        public void Update()
        {
            currentRoom = MiniMapConstants.CalculatePos(0, 0, MiniMapConstants.greenSize, MiniMapConstants.greenSize);
            currentRoom.X += MiniMapConstants.greenSize / 2;
        }

        public void Draw()
        {
            /* Draw background (just for testing) */
            DrawRectangle(new Rectangle(0 - offsetX, 0 - offsetY, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.Black);

            /* Draw whole map first, then current room */
            foreach(Rectangle rec in MiniMapConstants.GetRoomLayout()) DrawRectangle(rec, Color.RoyalBlue);
            DrawRectangle(currentRoom, Color.LimeGreen);
        }

        private void DrawRectangle(Rectangle rec, Color color)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + offsetX, rec.Y + offsetY, rec.Width, rec.Height), color);
        }
    }
}
