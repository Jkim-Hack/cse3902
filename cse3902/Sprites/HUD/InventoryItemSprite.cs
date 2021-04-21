using System;
using cse3902.HUD;
using cse3902.Interfaces;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class InventoryItemSprite : ISprite
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private (int X, int Y) center;

        private Rectangle destination;

        private int currentFrame;

        private int frameWidth;
        private int frameHeight;
        private Rectangle[] frames;

        private InventoryManager.ItemType itemType;

        public InventoryItemSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 center, InventoryManager.ItemType type)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.center.X = (int) center.X;
            this.center.Y = (int)center.Y;

            int rows = 3;
            int columns = 5;
            frameWidth = texture.Width / columns;
            frameHeight = texture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);

            changeItem(type);
        }
	    
	    public void changeItem(InventoryManager.ItemType type)
        {
            if (HUDUtilities.HUDItemIndexDict.ContainsKey(type)){
                currentFrame = HUDUtilities.HUDItemIndexDict[type];
            }
            else
            {
                this.itemType = InventoryManager.ItemType.None;
            }
        }
        

        public Vector2 Center 
	    { 
	        get => new Vector2(center.X, center.Y);
            set
            {
                this.center.X = (int)value.X;
                this.center.Y = (int)value.Y;
            }
		}

        public Texture2D Texture
        {
            get => texture;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = frameWidth;
                int height = frameHeight;
                Rectangle Destination = new Rectangle(center.X, center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(center.X, center.Y, frameWidth, frameHeight);
            spriteBatch.Draw(texture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, HUDUtilities.InventoryItemLayer-.1f);
        }

        public void Erase()
        {
            texture.Dispose();
	    }
        public InventoryManager.ItemType ItemType
        {
            get => itemType;
        }

        public int Update(GameTime gameTime)
        {
            // Does nothing just displays
            return 0;
        } 
    }
}
