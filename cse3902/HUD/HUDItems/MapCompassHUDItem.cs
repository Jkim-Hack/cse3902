using cse3902.Interfaces;
using cse3902.Constants;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.HUD
{
    public class MapCompassHUDItem : IHUDItem
    {
        private Game1 game;

        private Texture2D label;
        private Texture2D compass;

        private Rectangle labelPos;
        private Rectangle compassPos;
        private Rectangle mapCover;
        
        private int offsetX;
        private int offsetY;

        public MapCompassHUDItem(Game1 game, Texture2D label, Texture2D compass)
        {
            this.game = game;

            this.label = label;
            this.compass = compass;

            this.offsetX = 35;
            this.offsetY = DimensionConstants.OriginalWindowHeight / 2;

            int scaledLabelWidth = label.Bounds.Width / DimensionConstants.DrawScale;
            int scaledLabelHeight = label.Bounds.Height / DimensionConstants.DrawScale;
            this.labelPos = new Rectangle(offsetX, offsetY, scaledLabelWidth, scaledLabelHeight);

            int scaledCompassWidth = (int)(compass.Bounds.Width / 1.3f);
            int scaledCompassHeight = (int)(compass.Bounds.Height / 1.3f);
            this.compassPos = new Rectangle(offsetX + (scaledLabelWidth / 2) - (scaledCompassWidth / 2), offsetY + scaledLabelHeight + 5, scaledCompassWidth, scaledCompassHeight);

            this.mapCover = new Rectangle(0, scaledLabelHeight / 4, scaledLabelWidth, scaledLabelHeight / 2);
        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }

        public void Draw()
        {
            DrawLabel();
            if (InventoryManager.Instance.inventory[InventoryManager.ItemType.Map] == 0) DrawMapCover();
            if (InventoryManager.Instance.inventory[InventoryManager.ItemType.Compass] > 0) DrawCompass();
        }

        public void DrawLabel()
        {
            game.SpriteBatch.Draw(label, labelPos, Color.White);
        }

        public void DrawMapCover()
        {
            HUDUtilities.DrawRectangle(game, mapCover, Color.Black, offsetX, offsetY);
        }

        public void DrawCompass()
        {
            game.SpriteBatch.Draw(compass, compassPos, Color.White);
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

            get => label;
        }

        public ref Rectangle Box {

            get => ref labelPos;
        }
    }
}
