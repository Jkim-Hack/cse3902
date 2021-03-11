using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace cse3902.Sprites
{
    public class NormalDownOpenDoorSprite : IDoorSprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private int frameWidth;
        private int frameHeight;

        private List<Rectangle> hitboxes;
        private Rectangle door;

        private const float sizeIncrease = 1f;

        public NormalDownOpenDoorSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Rectangle wantedDoor)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            center = startingPosition;

            door = wantedDoor;
            hitboxes = new List<Rectangle>();
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, door, Color.White, 0, origin, SpriteEffects.None, 0.9f);
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
                int height = (int)(sizeIncrease * frameHeight) / 2;
                Rectangle destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                destination.Offset(-destination.Width / 2, 0);
                hitboxes.Add(destination);

                int openWidth = 18;
                width = (width - openWidth) / 2;

                destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                destination.Offset(-frameWidth / 2, -frameHeight / 2);
                hitboxes.Add(destination);

                destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                destination.Offset(openWidth / 2, -frameHeight / 2);
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