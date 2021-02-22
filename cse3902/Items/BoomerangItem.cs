using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Items
{
    public class BoomerangItem : ISprite, IItem, IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 startingPosition;
        private Vector2 center;
        private float angle;

        private int currentX;
        private int currentY;

        private int turns;

        private enum Direction
        {
            Up, Down, Left, Right
        }

        private Direction direction;

        private bool animationComplete;

        public BoomerangItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            startingPosition = startingPos - new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);

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

            turns = 0;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            animationComplete = false;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, new Vector2(currentX+ Texture.Width/2, currentY+ Texture.Height/2), null, Color.White, angle, origin, 2.0f, SpriteEffects.None, 1);
            spriteBatch.End();
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public void Update(GameTime gameTime, onAnimCompleteCallback animationCompleteCallback)
        {
            int offset = 0;
            if(turns % 2 == 0)
            {
                offset = 50;
            }
            if (direction == Direction.Right)
            {
                currentX += 2;
                if (currentX > startingPosition.X + offset)
                {
                    direction = Direction.Left;
                    turns++;
                }
            }
            else if (direction == Direction.Left)
            {
                currentX -= 2;
                if (currentX < startingPosition.X - offset)
                {
                    direction = Direction.Right;
                    turns++;
                }
            }
            else
            if (direction == Direction.Down)
            {
                currentY += 2;
                if (currentY > startingPosition.Y + offset)
                {
                    direction = Direction.Up;
                    turns++;
                }
            }
            else
            {
                currentY -= 2;
                if (currentY < startingPosition.Y - offset)
                {
                    direction = Direction.Down;
                    turns++;
                }
            }

            if (turns == 2)
            {
                animationComplete = true;
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