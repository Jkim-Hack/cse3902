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

        private Texture2D level;

        private Rectangle levelPos;
        
        private int offsetX;
        private int offsetY;

        private bool colorRed;
        private int delay;

        public MiniMapHUDItem(Game1 game, Texture2D level)
        {
            this.game = game;

            this.level = level;

            this.offsetX = 25;
            this.offsetY = DimensionConstants.OriginalWindowHeight - 32;

            this.levelPos = new Rectangle(0, (-level.Bounds.Height / 3) - 7, level.Bounds.Width / 3, level.Bounds.Height / 3 + 1);

            colorRed = false;
            delay = MiniMapConstants.COLOR_DELAY;
        }

        public int Update(GameTime gameTime)
        {
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
            if (game.RoomHandler.currentRoom.Z == 0) DrawCurrentRoom();
        }

        private void DrawLevelLabel()
        {
            HUDUtilities.DrawTexture(game, level, levelPos, offsetX, offsetY, HUDUtilities.MinimapLabelLayer);
        }

        private void DrawMap()
        {
            foreach (Rectangle rec in MiniMapConstants.GetRoomLayout()) HUDUtilities.DrawRectangle(game, rec, MiniMapConstants.RoomColor, offsetX, offsetY, HUDUtilities.MinimapLayer);
        }

        private void DrawTriforce()
        {
            Rectangle triforceRectangle = MiniMapConstants.CalculatePos((int)MiniMapConstants.TriforcePos.X, (int)MiniMapConstants.TriforcePos.Y, MiniMapConstants.Width, MiniMapConstants.Height);
            triforceRectangle.X += (MiniMapConstants.Width - MiniMapConstants.Height) / 2;
            triforceRectangle.Width = MiniMapConstants.Height;
            if (colorRed) HUDUtilities.DrawRectangle(game, triforceRectangle, MiniMapConstants.TriforceRed, offsetX, offsetY, HUDUtilities.MinimapTriforceLayer);
            else HUDUtilities.DrawRectangle(game, triforceRectangle, MiniMapConstants.TriforceGreen, offsetX, offsetY, HUDUtilities.MinimapTriforceLayer);
        }

        private void DrawCurrentRoom()
        {
            Rectangle currentRoomRectangle = MiniMapConstants.CalculatePos((int)game.RoomHandler.currentRoom.X, (int)game.RoomHandler.currentRoom.Y, MiniMapConstants.Width, MiniMapConstants.Height);
            currentRoomRectangle.X += (MiniMapConstants.Width - MiniMapConstants.Height) / 2;
            currentRoomRectangle.Width = MiniMapConstants.Height;
            HUDUtilities.DrawRectangle(game, currentRoomRectangle, MiniMapConstants.CurrentRoomColor, offsetX, offsetY, HUDUtilities.MinimapCurrentRoomLayer);
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

            get => level;
        }

        public ref Rectangle Box {

            get => ref levelPos;
        }
    }
}
