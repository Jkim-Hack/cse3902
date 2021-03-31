using cse3902.Interfaces;
using cse3902.Constants;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.HUD
{
    public class MiniMapHUDItem : IHUDItem
    {
        private Game1 game;
        private Vector3 currentRoom;

        private Texture2D level;

        private Rectangle levelPos;
        
        private int offsetX;
        private int offsetY;

        private bool colorRed;
        private int delay;

        private bool alreadyChanged;

        private Rectangle box; // placeholder since this HUD item doesn't have a real rectangle

        public MiniMapHUDItem(Game1 game, Texture2D level)
        {
            this.game = game;
            this.currentRoom = game.RoomHandler.currentRoom;
            this.currentRoom.Y++;

            this.level = level;

            this.offsetX = 25;
            this.offsetY = DimensionConstants.OriginalWindowHeight - 33;

            int scaledWidth = (int)(level.Bounds.Width * 0.4f) - 1;
            int scaledHeight = (int)(level.Bounds.Height * 0.4f);
            this.levelPos = new Rectangle(offsetX, offsetY - scaledHeight - 7, scaledWidth, scaledHeight);

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
            DrawLevelLabel();
            if (InventoryManager.Instance.inventory[InventoryManager.ItemType.Map] > 0) DrawMap();
            if (InventoryManager.Instance.inventory[InventoryManager.ItemType.Compass] > 0) DrawTriforce();
            if (currentRoom.Z == 0) DrawCurrentRoom();
        }

        private void DrawLevelLabel()
        {
            game.SpriteBatch.Draw(level, levelPos, Color.White);
        }

        private void DrawMap()
        {
            foreach (Rectangle rec in MiniMapConstants.GetRoomLayout()) HUDUtilities.DrawRectangle(game, rec, MiniMapConstants.RoomColor, offsetX, offsetY); 
        }

        private void DrawTriforce()
        {
            Rectangle triforceRectangle = MiniMapConstants.CalculatePos((int)MiniMapConstants.TriforcePos.X, (int)MiniMapConstants.TriforcePos.Y);
            triforceRectangle.X += (MiniMapConstants.Width - MiniMapConstants.Height) / 2;
            triforceRectangle.Width = MiniMapConstants.Height;
            if (colorRed) HUDUtilities.DrawRectangle(game, triforceRectangle, MiniMapConstants.TriforceRed, offsetX, offsetY);
            else HUDUtilities.DrawRectangle(game, triforceRectangle, MiniMapConstants.TriforceGreen, offsetX, offsetY);
        }

        private void DrawCurrentRoom()
        {
            Rectangle currentRoomRectangle = MiniMapConstants.CalculatePos((int)currentRoom.X, (int)currentRoom.Y, MiniMapConstants.Width, MiniMapConstants.Height);
            currentRoomRectangle.X += (MiniMapConstants.Width - MiniMapConstants.Height) / 2;
            currentRoomRectangle.Width = MiniMapConstants.Height;
            HUDUtilities.DrawRectangle(game, currentRoomRectangle, MiniMapConstants.CurrentRoomColor, offsetX, offsetY);
        }

        public void Erase() {} // needs to be deleted once isprite is updated

        public Vector2 Center {

            get => new Vector2(offsetX, offsetY);

            set {
                offsetX = (int) value.X;
                offsetY = (int) value.Y;
            }
        }

        /* minimap doesn't have a texture other than the level label */
        public Texture2D Texture {

            get => null;
        }

        public ref Rectangle Box {

            get => ref box;
        }
    }
}
