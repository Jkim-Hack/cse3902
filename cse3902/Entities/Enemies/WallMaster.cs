using System;
using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Rooms;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using cse3902.Constants;
using cse3902.Sounds;

namespace cse3902.Entities.Enemies
{
    public class WallMaster : IEntity
    {
        private WallMasterSprite wallMasterSprite;
        private WallMasterStateMachine wallMasterStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 abstractStart;
        private Vector2 center;
        private Vector2 previousCenter;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private Boolean isTriggered;
        private Rectangle detectionBox;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        private WallType wallType;
        private IEntity.EnemyType type;

        public WallMaster(Game1 game, Vector2 start, Vector2 abstractStart, IEntity.EnemyType type)
        {
            this.game = game;
            this.abstractStart = abstractStart;
            center = start;
            previousCenter = center;
            this.type = type;

            //wallmaster sprite sheet is 4 rows, 2 columns
            wallMasterSprite = (WallMasterSprite)EnemySpriteFactory.Instance.CreateWallMasterSprite(game.SpriteBatch, center);
            wallMasterStateMachine = new WallMasterStateMachine(wallMasterSprite);
            speed = 30.0f;
            this.direction = new Vector2(0, 0);
            travelDistance = 0;
            shoveDistance = -10;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            isTriggered = false;
            ConstructDetectionBox(abstractStart);
            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 10;
        }

        public void Attack()
        {
            this.wallMasterStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.direction.X = direction.X;
            this.direction.Y = direction.Y;

            wallMasterStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            if (this.Health > 0)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.enemyHit);
            }
            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            this.wallMasterStateMachine.Die();
            SoundFactory.PlaySound(SoundFactory.Instance.enemyHit);
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center, type);
        }

        public void BeShoved()
        {
            this.shoveDistance = 20;
            this.shoveDirection = -this.direction;
        }

        public void StopShove()
        {
            this.shoveDistance = 0;
        }

        private void UpdateDamage(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (collidable.DamageDisabled)
            {
                remainingDamageDelay -= timer;
                if (remainingDamageDelay < 0)
                {
                    remainingDamageDelay = DamageConstants.DamageDisableDelay;
                    collidable.DamageDisabled = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateDamage(gameTime);

            if (this.IsTriggered)
            {
                if (this.shoveDistance > 0) ShoveMovement();
                else RegularMovement(gameTime);
            }
            
        }

        private void ShoveMovement()
        {
            this.Center += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            this.Center += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (travelDistance <= 0)
            {

                switch (this.wallType)
                {
                    case WallType.LEFTWALL:
                        LeftWallMovement();
                        break;
                    case WallType.RIGHTWALL:
                        RightWallMovement();
                        break;
                    case WallType.BOTTOMWALL:
                        BottomWallMovement();
                        break;
                    case WallType.TOPWALL:
                        TopWallMovement();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                travelDistance--;
            }

            if (direction.X == 0 && direction.Y == 0)
            {
                int x = 0;
            }

            ChangeDirection(direction);

            if (direction.X == -0 && direction.Y == 1)
            {
                int x = 0;
            }
            wallMasterSprite.Update(gameTime);
        }

        public void Draw()
        {
            wallMasterSprite.Draw();
        }

        public IEntity Duplicate()
        {
            return new WallMaster(game, center, abstractStart, type);
        }

        public IEntity.EnemyType Type
        {
            get => type;
        }

        public ref Rectangle Bounds
        {
            get
            {
                if (this.IsTriggered)
                {
                    return ref this.wallMasterSprite.Box;
                } else
                {
                    return ref this.detectionBox;
                }

            }
        }

        public Vector2 Center
        {
            get => this.center;
            set
            {
                this.PreviousCenter = this.center;
                this.center = value;
                wallMasterSprite.Center = value;
            }
        }

        public Vector2 PreviousCenter
        {
            get => this.previousCenter;
            set
            {
                this.previousCenter = value;
            }
        }

        public int Damage
        {
            get => 2;
        }

        public int Health
        {
            get => this.health;
            set
            {
                this.health = value;
            }
        }

        public Boolean IsTriggered
        {
            get => this.isTriggered;
            set => this.isTriggered = value;
        }

        public Vector2 Direction
        {
            get => this.direction;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        private void ConstructDetectionBox(Vector2 startingPosition)
        {
            this.detectionBox = new Rectangle(this.wallMasterSprite.Box.X, this.wallMasterSprite.Box.Y, 2 * this.wallMasterSprite.Box.Width, 2 * this.wallMasterSprite.Box.Height);

            if (startingPosition.X < 0)
            {
                this.wallType = WallType.LEFTWALL;
                this.detectionBox.Inflate(0, RoomUtilities.BLOCK_SIDE);
            } else if (startingPosition.X > 11)
            {
                this.wallType = WallType.RIGHTWALL;
                this.detectionBox.Inflate(0, RoomUtilities.BLOCK_SIDE);
                this.detectionBox.Offset(-RoomUtilities.BLOCK_SIDE, -RoomUtilities.BLOCK_SIDE);
            } else if (startingPosition.Y < 0)
            {
                this.wallType = WallType.TOPWALL;
                this.detectionBox.Inflate(RoomUtilities.BLOCK_SIDE, 0);
                this.detectionBox.Offset(-RoomUtilities.BLOCK_SIDE, 0);
            } else
            {
                this.wallType = WallType.BOTTOMWALL;
                this.detectionBox.Inflate(RoomUtilities.BLOCK_SIDE, 0);
                this.detectionBox.Offset(0, -RoomUtilities.BLOCK_SIDE);
            }
        }

        private void LeftWallMovement()
        {
            switch (this.direction.X)
            {
                case 1:
                    direction.X = 0;
                    direction.Y = -1;
                    travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    break;
                case -1:
                    direction.X = 0;
                    direction.Y = 1;
                    travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    break;
                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    } else if (direction.Y == 1)
                    {
                        direction.X = 0;
                        direction.Y = 0;
                        this.IsTriggered = false;
                    } else
                    {
                        direction.X = 1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    }
                    break;
                default:
                    break;
            }
        }

        private void RightWallMovement()
        {
            switch (this.direction.X)
            {
                case 1:
                    direction.X = 0;
                    direction.Y = 1;
                    travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    break;
                case -1:
                    direction.X = 0;
                    direction.Y = -1;
                    travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    break;
                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = 1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    }
                    else if (direction.Y == 1)
                    {
                        direction.X = 0;
                        direction.Y = 0;
                        this.IsTriggered = false;
                    }
                    else
                    {
                        direction.X = -1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    }
                    break;
                default:
                    break;
            }
        }

        private void BottomWallMovement()
        {
            switch (this.direction.X)
            {
                case 1:
                    direction.X = 0;
                    direction.Y = 0;
                    this.IsTriggered = false;
                    break;
                case -1:
                    direction.X = 0;
                    direction.Y = 1;
                    travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    break;
                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    }
                    else if (direction.Y == 1)
                    {
                        direction.X = 1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    }
                    else
                    {
                        direction.X = 0;
                        direction.Y = -1;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    }
                    break;
                default:
                    break;
            }
        }

        private void TopWallMovement()
        {
            switch (this.direction.X)
            {
                case 1:
                    direction.X = 0;
                    direction.Y = 0;
                    this.IsTriggered = false;
                    break;
                case -1:
                    direction.X = 0;
                    direction.Y = -1;
                    travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    break;
                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = 1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    }
                    else if (direction.Y == 1)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 4;
                    } else
                    {
                        direction.X = 0;
                        direction.Y = 1;
                        travelDistance = RoomUtilities.BLOCK_SIDE * 2;
                    }
                    break;
                default:
                    break;
            }
        }

        private enum WallType
        {
            LEFTWALL = 0,
            RIGHTWALL = 1,
            TOPWALL = 2,
            BOTTOMWALL = 3
        }
    }
}