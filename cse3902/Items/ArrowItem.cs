using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.HUD;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites;
using System;

namespace cse3902.Items
{
    public class ArrowItem : IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private (int X, int Y) current;
        private int frameWidth;
        private int frameHeight;

        private float angle = 0;

        private bool collided;
        private Rectangle destination;
        private const float sizeIncrease = 1f;

        private Vector2 direction;
        private ICollidable collidable;

        public ArrowItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            direction = dir;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            current.X = (int)startingPos.X;
            current.Y = (int)startingPos.Y;

            this.collidable = new ItemCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(current.X, current.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(current.X, current.Y);
            }
            set
            {
                current.X = (int)value.X;
                current.Y = (int)value.Y;
            }
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                double cos = Math.Abs(Math.Cos(angle));
                double sin = Math.Abs(Math.Sin(angle));
                Rectangle Destination = new Rectangle(current.X, current.Y, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public Vector2 Direction
        {
            get => this.direction;
            set => this.direction = value;

        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public bool Collided
        {
            get => collided;
            set => collided = value;
        }

        public InventoryManager.ItemType ItemType
        {
            get => InventoryManager.ItemType.Arrow;
        }

        public bool IsKept
        {
            get => false;
        }

        public bool IsResetKept
        {
            get => false;
        }
    }
}
