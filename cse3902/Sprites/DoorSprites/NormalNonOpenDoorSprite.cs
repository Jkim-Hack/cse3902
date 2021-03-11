using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace cse3902.Sprites
{
    public class NormalNonOpenDoorSprite : IDoorSprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private int frameWidth;
        private int frameHeight;

        private List<Rectangle> hitboxes;
        private Rectangle door;

        private const float sizeIncrease = 1f;

        public NormalNonOpenDoorSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Rectangle wantedDoor)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            center = startingPosition;

            hitboxes = new List<Rectangle>();
            door = wantedDoor;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, door, Color.White, 0, origin, SpriteEffects.None, 0.8f);
        }

        public int Update(GameTime gameTime)
        {
            //nothing to update
            return 0;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public List<Rectangle> Boxes
        {
            get
            {
                hitboxes = new List<Rectangle>();

                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                destination.Offset(-destination.Width / 2, -destination.Height / 2);
                hitboxes.Add(destination);

                return hitboxes;
            }
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }
    }
}