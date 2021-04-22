using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.ParticleSystem;
using cse3902.Rooms;
using cse3902.Sounds;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;

namespace cse3902.Entities.Enemies
{
    public class WallMaster : IEntity
    {
        private WallMasterSprite wallMasterSprite;
        private ISprite grabbedLink;
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
        private Boolean grabbed;
        private Rectangle detectionBox;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        private WallType wallType;

        public WallMaster(Game1 game, Vector2 start, Vector2 abstractStart)
        {
            this.game = game;
            this.abstractStart = abstractStart;
            center = start;
            previousCenter = center;

            wallMasterSprite = (WallMasterSprite)EnemySpriteFactory.Instance.CreateWallMasterSprite(game.SpriteBatch, center);
            grabbedLink = NPCSpriteFactory.Instance.CreateGrabbedLinkSprite(game.SpriteBatch, center);
            wallMasterStateMachine = new WallMasterStateMachine(wallMasterSprite);
            speed = MovementConstants.WallMasterSpeed;
            this.direction = new Vector2(0, 0);
            travelDistance = 0;
            shoveDistance = MovementConstants.DefaultShoveDistance;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            isTriggered = false;
            grabbed = false;
            ConstructDetectionBox(abstractStart);
            this.collidable = new EnemyCollidable(this, this.Damage);
            health = SettingsValues.Instance.GetValue(SettingsValues.Variable.WallMasterHealth);
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
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center, IEntity.EnemyType.C);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center);
        }

        public void BeShoved()
        {
            this.shoveDistance = MovementConstants.WallMasterShoveDistance;
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
                this.grabbedLink.Center = this.Center;
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


            ChangeDirection(direction);


            wallMasterSprite.Update(gameTime);
        }

        public void Draw()
        {
            wallMasterSprite.Draw();
            if (this.grabbed)
            {
                this.grabbedLink.Draw();
            }
        }

        public IEntity Duplicate()
        {
            return new WallMaster(game, center, abstractStart);
        }

        public void GrabLink()
        {
            grabbed = true;
        }

        public IEntity.EnemyType Type
        {
            get => IEntity.EnemyType.C;
        }

        public ref Rectangle Bounds
        {
            get
            {
                if (this.IsTriggered)
                {
                    return ref this.wallMasterSprite.Box;
                }
                else
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
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.WallMasterDamage);
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
                this.detectionBox.Offset(RoomUtilities.BLOCK_SIDE, -RoomUtilities.BLOCK_SIDE);
            }
            else if (startingPosition.X > RoomUtilities.NUM_BLOCKS_X)
            {
                this.wallType = WallType.RIGHTWALL;
                this.detectionBox.Inflate(0, RoomUtilities.BLOCK_SIDE);
                this.detectionBox.Offset(-2*RoomUtilities.BLOCK_SIDE, -RoomUtilities.BLOCK_SIDE);
            }
            else if (startingPosition.Y < 0)
            {
                this.wallType = WallType.TOPWALL;
                this.detectionBox.Inflate(RoomUtilities.BLOCK_SIDE, 0);
                this.detectionBox.Offset(-RoomUtilities.BLOCK_SIDE,  RoomUtilities.BLOCK_SIDE);
            }
            else
            {
                this.wallType = WallType.BOTTOMWALL;
                this.detectionBox.Inflate(RoomUtilities.BLOCK_SIDE, 0);
                this.detectionBox.Offset(0, -2*RoomUtilities.BLOCK_SIDE);
            }
        }

        private void LeftWallMovement()
        {
            travelDistance = MovementConstants.WallMasterMaxTravel;
            switch (this.direction.X)
            {
                case 1:
                    direction.X = 0;
                    direction.Y = -1;
                    break;

                case -1:
                    direction.X = 0;
                    direction.Y = 1;
                    break;

                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                    }
                    else if (direction.Y == 1)
                    {
                        direction.X = 0;
                        direction.Y = 0;
                        this.IsTriggered = false;
                    }
                    else
                    {
                        direction.X = 1;
                        direction.Y = 0;
                    }
                    break;

                default:
                    break;
            }
        }

        private void RightWallMovement()
        {
            travelDistance = MovementConstants.WallMasterMaxTravel;
            switch (this.direction.X)
            {
                case 1:
                    direction.X = 0;
                    direction.Y = 1;
                    break;

                case -1:
                    direction.X = 0;
                    direction.Y = -1;
                    break;

                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = 1;
                        direction.Y = 0;
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
                    }
                    break;

                default:
                    break;
            }
        }

        private void BottomWallMovement()
        {
            travelDistance = MovementConstants.WallMasterMaxTravel;
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
                    break;

                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                    }
                    else if (direction.Y == 1)
                    {
                        direction.X = 1;
                        direction.Y = 0;
                    }
                    else
                    {
                        direction.X = 0;
                        direction.Y = -1;
                    }
                    break;

                default:
                    break;
            }
        }

        private void TopWallMovement()
        {
            travelDistance = MovementConstants.WallMasterMaxTravel;
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
                    break;

                case 0:
                    if (direction.Y == -1)
                    {
                        direction.X = 1;
                        direction.Y = 0;
                    }
                    else if (direction.Y == 1)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                    }
                    else
                    {
                        direction.X = 0;
                        direction.Y = 1;
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