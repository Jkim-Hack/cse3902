using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.Rooms;
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

        private Vector2 center;
        private Vector2 previousCenter;
        private int travelDistance;

        private Rectangle detectionBox1;
        private Rectangle detectionBox2;
        private Rectangle currentDetectionBox;
        private int counter;

        private Boolean triggered;
        private ICollidable collidable;

        public Trap(Game1 game, Vector2 start, Vector2 direction)
        {
            this.game = game;

            center = start;
            previousCenter = center;


            trapSprite = (TrapSprite)EnemySpriteFactory.Instance.CreateTrapSprite(game.SpriteBatch, start);
            this.direction = direction;
            speed = 50.0f;
            travelDistance = 50;

            
            ConstructDetectionBoxes(direction);
            currentDetectionBox = detectionBox1;
            counter = 0;

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
            set
            {
                this.PreviousCenter = this.center;
                this.center = value;
                this.trapSprite.Center = value;
            }
        }

        public Vector2 PreviousCenter
        {
            get => this.previousCenter;
            set => this.previousCenter = value;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
        
        public void Update(GameTime gameTime)
        {
            counter++;
            if (counter > 60)
            {
                counter = 0;

                if (currentDetectionBox == detectionBox1)
                {
                    currentDetectionBox = detectionBox2;
                }
                else
                {
                    currentDetectionBox = detectionBox1;
                }
            }


            this.trapSprite.Update(gameTime);
        }

        public void Draw()
        {
            this.trapSprite.Draw();
        }

        private void ConstructDetectionBoxes(Vector2 direction)
        { 
            if (direction.X == 1)
            {
                detectionBox1 = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, (RoomUtilities.NUM_BLOCKS_X * RoomUtilities.BLOCK_SIDE), RoomUtilities.BLOCK_SIDE);
            }
            else
            {
                detectionBox1 = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, (RoomUtilities.NUM_BLOCKS_X * RoomUtilities.BLOCK_SIDE), RoomUtilities.BLOCK_SIDE);
                detectionBox1.Offset(-((RoomUtilities.NUM_BLOCKS_X-1) * RoomUtilities.BLOCK_SIDE), 0);
            }

            if (direction.Y == 1)
            {
                detectionBox2  = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, RoomUtilities.BLOCK_SIDE, (RoomUtilities.NUM_BLOCKS_Y * RoomUtilities.BLOCK_SIDE));
            } else
            {
                detectionBox2 = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, RoomUtilities.BLOCK_SIDE, (RoomUtilities.NUM_BLOCKS_Y * RoomUtilities.BLOCK_SIDE));
                detectionBox2.Offset(0, -((RoomUtilities.NUM_BLOCKS_Y - 1) * RoomUtilities.BLOCK_SIDE));
            }

        }

    }
}
