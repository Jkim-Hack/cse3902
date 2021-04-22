using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.HUD;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites;
using System;
using cse3902.Constants;
using cse3902.Utilities;

namespace cse3902.Items
{
    public class SwordItem : IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private (int x, int y) current;

        int swordType;

        private bool collided;
        private Rectangle destination;
        private Rectangle source;

        private Vector2 direction;
        private ICollidable collidable;

        private bool isKept;
        private bool isResetKept;

        public SwordItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, bool kept, bool resetKept, int swordType)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            int cols = ItemConstants.SWORDCOLS;
            int rows = ItemConstants.SWORDROWS;
            int frameWidth = spriteTexture.Width/ cols;
            int frameHeight = spriteTexture.Height/ rows;

            current.x = (int)startingPos.X;
            current.y = (int)startingPos.Y;
            Rectangle[] rectangles = SpriteUtilities.distributeFrames(cols, rows, frameWidth, frameHeight);

            this.source = rectangles[swordType];
            this.swordType = swordType;

            isKept = kept;
            isResetKept = resetKept;

            this.collidable = new ItemCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(source.Width / 2f, source.Height / 2f);
            Rectangle Destination = new Rectangle(current.x, current.y, source.Width, source.Height);
            spriteBatch.Draw(spriteTexture, Destination, source, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.ItemLayer);
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
                int width = source.Width;
                int height = source.Height;
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
            get => InventoryUtilities.convertIntToSword(SwordType);
        }

        public bool IsKept
        {
            get => isKept;
        }

        public bool IsResetKept
        {
            get => isResetKept;
        }

        public int SwordType
        {
            get => swordType;
        }
    }
}