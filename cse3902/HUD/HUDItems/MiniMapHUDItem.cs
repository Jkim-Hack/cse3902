using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.HUD
{
    public class MiniMapHUDItem : IHUDItem
    {
        private Game1 game;

        private Vector3 currentRoom;
        
        private int offsetX;
        private int offsetY;

        private bool colorRed;
        private int delay;

        private bool alreadyChanged;

        private Rectangle box; // placeholder since this HUD item doesn't have a real rectangle

        public MiniMapHUDItem(Game1 game)
        {
            this.game = game;
            this.currentRoom = game.RoomHandler.currentRoom;

            this.offsetX = 40;
            this.offsetY = DimensionConstants.OriginalWindowHeight - 40;

            this.alreadyChanged = false;
            colorRed = false;
            delay = MiniMapConstants.COLOR_DELAY;

            box = new Rectangle();
        }

        public int Update(GameTime gameTime)
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

            delay--;
            if (delay < 0)
            {
                delay = MiniMapConstants.COLOR_DELAY;
                colorRed = !colorRed;
            }

            return 0;
        }

        public void Draw()
        {
            /* Draw background (just for testing) */
            DrawRectangle(new Rectangle(0 - offsetX, 0 - offsetY, DimensionConstants.OriginalWindowWidth, DimensionConstants.OriginalWindowHeight), Color.Black);

            if (InventoryManager.Instance.inventory[InventoryManager.ItemType.Map] > 0)
            {
                /* Draw entire map */
                foreach (Rectangle rec in MiniMapConstants.GetRoomLayout()) DrawRectangle(rec, MiniMapConstants.RoomColor);
            }

            if (InventoryManager.Instance.inventory[InventoryManager.ItemType.Compass] > 0)
            {
                Rectangle triforceRectangle = MiniMapConstants.CalculatePos((int)MiniMapConstants.TriforcePos.X, (int)MiniMapConstants.TriforcePos.Y);
                triforceRectangle.X += (MiniMapConstants.Width - MiniMapConstants.Height) / 2;
                triforceRectangle.Width = MiniMapConstants.Height;
                if (colorRed) DrawRectangle(triforceRectangle, MiniMapConstants.TriforceRed);
                else DrawRectangle(triforceRectangle, MiniMapConstants.TriforceGreen);
            }

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

        public void Erase() {} // needs to be deleted once isprite is updated

        public Vector2 Center {

            get => new Vector2(offsetX, offsetY);

            set {
                offsetX = (int) value.X;
                offsetY = (int) value.Y;
            }
        }

        public Texture2D Texture {

            get => null;
        }

        public ref Rectangle Box {

            get => ref box;
        }
    }
}
