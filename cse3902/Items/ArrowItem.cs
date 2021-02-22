using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Items
{
    public class ArrowItem : ISprite, IItem, IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 startingPosition;
        private Vector2 center;

        private int currentX;
        private int currentY;

        private float angle = 0;

        private bool animationComplete;

        private enum Direction
        {
            Up, Down, Left, Right
        }

        private Direction direction;

        public ArrowItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            startingPosition = startingPos;

            if (dir.X > 0)
            {
                direction = Direction.Right;
                angle = 1.57f;
            }
            else if (dir.X < 0)
            {
                direction = Direction.Left;
                angle = 4.71f;
            }
            else if (dir.Y > 0)
            {
                direction = Direction.Down;
                angle = 3.14f;
            }
            else
            {
                direction = Direction.Up;
                angle = 0;
            }

            animationComplete = false;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, new Vector2(currentX, currentY), null, Color.White, angle, origin, 2.0f, SpriteEffects.None, 1);
            spriteBatch.End();
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public void Update(GameTime gameTime, onAnimCompleteCallback animationCompleteCallback)
        {
            if (direction == Direction.Right)
            {
                currentX += 2;
                if (currentX > 800)
                {
                    currentX = 0;
                    animationComplete = true;
                }
            }
            else if (direction == Direction.Left)
            {
                currentX -= 2;
                if (currentX < 0)
                {
                    currentX = 800;
                    animationComplete = true;
                }
            }
            else if (direction == Direction.Down)
            {
                currentY += 2;
                if (currentY > 480)
                {
                    currentY = 0;
                    animationComplete = true;
                }
            }
            else
            {
                currentY -= 2;
                if (currentY < 0)
                {
                    currentY = 480;
                    animationComplete = true;
                }
            }
        }

        public Vector2 StartingPosition
        {
            get => startingPosition;

            set
            {
                startingPosition = value;
                center = value;
                currentX = (int)value.X;
                currentY = (int)value.Y;
            }
        }

        public Vector2 Center
        {
            get => center;

            set
            {
                center = value;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public bool AnimationComplete
        {
            get => animationComplete;

            set => animationComplete = value;
        }
    }
}