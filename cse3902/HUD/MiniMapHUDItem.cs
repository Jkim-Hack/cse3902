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

        public MiniMapHUDItem(Game1 game)
        {
            this.game = game;
            this.currentRoom = new Rectangle(0, 0, 0, 0);
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            /* Draw background (just for testing) */
            DrawRectangle(new Rectangle(0 - MiniMapRoomLayout.offsetX, 0 - MiniMapRoomLayout.offsetY, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.Black);

            /* Draw whole map first, then current room */
            foreach(Rectangle rec in MiniMapRoomLayout.RoomLayout) DrawRectangle(rec, Color.RoyalBlue);
            DrawRectangle(currentRoom, Color.LimeGreen);
        }

        private void DrawRectangle(Rectangle rec, Color color)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + MiniMapRoomLayout.offsetX, rec.Y + MiniMapRoomLayout.offsetY, rec.Width, rec.Height), color);
        }
    }
}
