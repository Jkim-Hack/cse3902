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

        private Rectangle detectionBoxX;
        private Rectangle detectionBoxY;
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
            currentDetectionBox = detectionBoxX;
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
            set => this.direction = value;
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
            if (counter > 20)
            {
                counter = 0;

                if (currentDetectionBox == detectionBoxX)
                {
                    currentDetectionBox = detectionBoxY;
                }
                else
                {
                    currentDetectionBox = detectionBoxX;
                }
            }

            if (this.IsTriggered)
            {
                this.Center += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            this.trapSprite.Update(gameTime);
        }

        public void Draw()
        {
            this.trapSprite.Draw();
        }

        public void Trigger()
        {
            this.IsTriggered = true;

            if (currentDetectionBox == detectionBoxX)
            {
                this.Direction = new Vector2(this.Direction.X, 0);
            } else
            {
                this.Direction = new Vector2(0, this.Direction.Y);
            }
        }

        private void ConstructDetectionBoxes(Vector2 direction)
        { 
            if (direction.X == 1)
            {
                detectionBoxX = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, (RoomUtilities.NUM_BLOCKS_X * RoomUtilities.BLOCK_SIDE), RoomUtilities.BLOCK_SIDE);
            }
            else
            {
                detectionBoxX = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, (RoomUtilities.NUM_BLOCKS_X * RoomUtilities.BLOCK_SIDE), RoomUtilities.BLOCK_SIDE);
                detectionBoxX.Offset(-((RoomUtilities.NUM_BLOCKS_X-1) * RoomUtilities.BLOCK_SIDE), 0);
            }

            if (direction.Y == 1)
            {
                detectionBoxY  = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, RoomUtilities.BLOCK_SIDE, (RoomUtilities.NUM_BLOCKS_Y * RoomUtilities.BLOCK_SIDE));
            } else
            {
                detectionBoxY = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, RoomUtilities.BLOCK_SIDE, (RoomUtilities.NUM_BLOCKS_Y * RoomUtilities.BLOCK_SIDE));
                detectionBoxY.Offset(0, -((RoomUtilities.NUM_BLOCKS_Y - 1) * RoomUtilities.BLOCK_SIDE));
            }

        }

    }
}
