using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Entities.Enemies
{
    public class Trap: ITrap
    {
        private TrapSprite trapSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;

        private Rectangle detectionBox1;
        private Rectangle detectionBox2;
        private Rectangle currentDetectionBox;

        private Boolean triggered;
        private ICollidable collidable;

        public Trap(Game1 game, Vector2 start, Vector2 direction)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;

            trapSprite = (TrapSprite)EnemySpriteFactory.Instance.CreateTrapSprite(game.SpriteBatch, startingPos);
            this.direction = direction;
            speed = 50.0f;
            travelDistance = 50;

            ConstructDetectionBoxes(direction);

            this.triggered = false;
            this.collidable = new TrapCollidable(this);
        }

        public int Damage
        {
            get => 3;
        }

        public Boolean IsTriggered
        {
            get => this.triggered;
            set => this.triggered = value;
        }

        public ref Rectangle Bounds
        {
            get
            {
                if (this.IsTriggered)
                {
                    return ref this.trapSprite.Box;
                } else
                {
                    return ref currentDetectionBox;
                }
            }
        }

        public Vector2 Direction
        {
            get => this.direction;
        }

        public Vector2 Center
        {
            get => this.center;
            set => this.center = value;
        }

        public Vector2 PreviousCenter
        {
            //todo: not yet implemented correctly for trap
            get => this.center;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
        
        public void Update(GameTime gameTime)
        {
            if (currentDetectionBox == detectionBox1)
            {
                currentDetectionBox = detectionBox2;
            } else
            {
                currentDetectionBox = detectionBox1;
            }

            this.trapSprite.Update(gameTime);
        }

        public void Draw()
        {
            this.trapSprite.Draw();
        }

        private void ConstructDetectionBoxes(Vector2 direction)
        {
            //todo: test these magic number values
            if (direction.X == 1)
            {
                detectionBox1 = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, 300, 800);


            }
            else
            {
                detectionBox1 = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, 300, 800);
                detectionBox1.Offset(-1000, 0);
            }

            if (direction.Y == 1)
            {
                detectionBox2  = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, 800, 300);
            } else
            {
                detectionBox2 = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, 800, 300);
                detectionBox2.Offset(0, 1000);
            }

        }

    }
}
