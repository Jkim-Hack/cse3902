using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.Rooms;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;

namespace cse3902.Entities.Enemies
{
    public class Trap : ITrap
    {
        private TrapSprite trapSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private Vector2 originalDirection;
        private Vector2 triggerDirection;

        private (Vector2 previous, Vector2 current) center;

        private int triggerDistance;
        private Boolean inReverse;

        private Rectangle detectionBoxX;
        private Rectangle detectionBoxY;
        private Rectangle currentDetectionBox;
        private int counter;

        private Boolean triggered;
        private ICollidable collidable;

        public Trap(Game1 game, Vector2 start, Vector2 direction)
        {
            this.game = game;

            center.current = start;
            center.previous = center.current;
            originalDirection = direction;

            trapSprite = (TrapSprite)EnemySpriteFactory.Instance.CreateTrapSprite(game.SpriteBatch, start);
            this.direction = direction;
            this.triggerDirection = direction;

            triggerDistance = 0;
            inReverse = false;

            ConstructDetectionBoxes(direction);
            currentDetectionBox = detectionBoxX;
            counter = 0;

            this.triggered = false;
            this.collidable = new TrapCollidable(this);
        }

        public int Damage
        {
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.TrapDamage);
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
                }
                else
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
            get => this.center.current;
            set
            {
                this.PreviousCenter = this.center.current;
                this.center.current = value;
                this.trapSprite.Center = value;
            }
        }

        public Vector2 PreviousCenter
        {
            get => this.center.previous;
            set => this.center.previous = value;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public void Update(GameTime gameTime)
        {
            counter++;
            if (counter > MovementConstants.TrapTime)
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
                TriggerMovement(gameTime);   
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
                this.Direction = new Vector2(this.triggerDirection.X, 0);
                this.triggerDistance = MovementConstants.TrapTriggerDistanceX;
            }
            else
            {
                this.Direction = new Vector2(0, this.triggerDirection.Y);
                this.triggerDistance = MovementConstants.TrapTriggerDistanceY;
            }
        }

        public ITrap Duplicate()
        {
            return new Trap(game, center.current, originalDirection);
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
                detectionBoxX.Offset(-((RoomUtilities.NUM_BLOCKS_X - 1) * RoomUtilities.BLOCK_SIDE), 0);
            }

            if (direction.Y == 1)
            {
                detectionBoxY = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, RoomUtilities.BLOCK_SIDE, (RoomUtilities.NUM_BLOCKS_Y * RoomUtilities.BLOCK_SIDE));
            }
            else
            {
                detectionBoxY = new Rectangle(this.trapSprite.Box.X, this.trapSprite.Box.Y, RoomUtilities.BLOCK_SIDE, (RoomUtilities.NUM_BLOCKS_Y * RoomUtilities.BLOCK_SIDE));
                detectionBoxY.Offset(0, -((RoomUtilities.NUM_BLOCKS_Y - 1) * RoomUtilities.BLOCK_SIDE));
            }
        }

        private void TriggerMovement(GameTime gameTime)
        {
            this.Center += direction * MovementConstants.TrapSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            triggerDistance--;

            if (triggerDistance <= 0)
            {
                if (inReverse)
                {
                    this.IsTriggered = false;
                    inReverse = false;
                }
                else
                {
                    this.Direction = -this.Direction;
                    if (this.Direction.X == 0)
                    {
                        this.triggerDistance = MovementConstants.TrapTriggerDistanceY;
                    }
                    else
                    {
                        this.triggerDistance = MovementConstants.TrapTriggerDistanceX;
                    }

                    inReverse = true;
                }
            }
        }
    }
}