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
            this.currentRoom = new Rectangle(40 + MiniMapRoomLayout.offsetX, 40 + MiniMapRoomLayout.offsetY, 50, 50);
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            /* Draw whole map first, then current room */
            foreach(Rectangle rec in MiniMapRoomLayout.RoomLayout)
            {
                DrawRectangle(new Rectangle(rec.X + MiniMapRoomLayout.offsetX, rec.Y + MiniMapRoomLayout.offsetY, rec.Width, rec.Height), Color.RoyalBlue);
            }
            DrawRectangle(currentRoom, Color.LimeGreen);
        }

        private void DrawRectangle(Rectangle rectangle, Color color)
        {
            Texture2D txt = new Texture2D(game.GraphicsDevice, 1, 1);
            txt.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(txt, rectangle, color);
        }
    }
}
