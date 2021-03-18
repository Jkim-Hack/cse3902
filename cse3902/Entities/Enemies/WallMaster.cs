using System;
using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using cse3902.Constants;

namespace cse3902.Entities.Enemies
{
    public class WallMaster : IEntity
    {
        private WallMasterSprite wallMasterSprite;
        private WallMasterStateMachine wallMasterStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        public WallMaster(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;

            //wallmaster sprite sheet is 4 rows, 2 columns
            wallMasterSprite = (WallMasterSprite)EnemySpriteFactory.Instance.CreateWallMasterSprite(game.SpriteBatch, center);
            wallMasterStateMachine = new WallMasterStateMachine(wallMasterSprite);
            speed = 20.0f;
            travelDistance = 0;
            shoveDistance = -10;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 10;
        }

        public Vector2 Center
        {
            get => this.center;
        }

        public ref Rectangle Bounds
        {
            get => ref wallMasterSprite.Box;
        }

        public void Attack()
        {
            this.wallMasterStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            //direction vector of (0,0) indicates just reverse the current direction
            if (direction == new Vector2(0, 0))
            {
                this.direction.X = -this.direction.X;
                this.direction.Y = -this.direction.Y;
            }
            else
            {
                this.direction.X = direction.X;
                this.direction.Y = direction.Y;
            }

            wallMasterStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            this.wallMasterStateMachine.Die();
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
	        this.collidable.ResetCollisions();
            if (this.shoveDistance > 0) ShoveMovement();
            else RegularMovement(gameTime);
        }

        private void ShoveMovement()
        {
            this.CenterPosition += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (travelDistance <= 0)
            {
                Random rand = new System.Random();
                int choice = rand.Next(0, 4);
                travelDistance = 80;

                switch (choice)
                {
                    case 0:
                        direction.X = 1;
                        direction.Y = 1;
                        break;
                    case 1:
                        direction.X = 1;
                        direction.Y = -1;
                        break;
                    case 2:
                        direction.X = -1;
                        direction.Y = 1;
                        break;
                    case 3:
                        direction.X = -1;
                        direction.Y = -1;
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
        }

        public Vector2 CenterPosition
        {
            get => this.center;
            set
            {
                this.center = value;
                wallMasterSprite.Center = value;
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

        public Vector2 Direction
        {
            get => this.direction;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}