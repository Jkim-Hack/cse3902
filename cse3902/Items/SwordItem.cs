using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.HUD;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites;
using System;
using cse3902.Constants;

namespace cse3902.Items
{
    public class SwordItem : IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private (int x, int y) current;
        private int frameWidth;
        private int frameHeight;

        int swordType;

        private bool collided;
        private Rectangle destination;
        private Rectangle source;
        private const float sizeIncrease = .5f;

        private Vector2 direction;
        private ICollidable collidable;

        public SwordItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, int type)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            current.x = (int)startingPos.X;
            current.y = (int)startingPos.Y;
            Rectangle[] rectangles = SpriteUtilities.distributeFrames(ItemConstants.SWORDROWS, ItemConstants.SWORDCOLS, texture.Width, texture.Height);

            this.source = rectangles[type];
            this.swordType = type;


            this.collidable = new ItemCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(current.x, current.y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, source, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.ItemLayer);
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
                return new Vector2(current.x, current.y);
            }
            set
            {
                current.x = (int)value.X;
                current.y = (int)value.Y;
            }
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle Destination = new Rectangle(current.x, current.y, width, height);
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
            get => InventoryManager.ItemType.Sword;
        }

        public bool IsKept
        {
            get => false;
        }

        public bool IsResetKept
        {
            get => false;
        }

        public int SwordType
        {
            get => swordType;
        }
    }
}