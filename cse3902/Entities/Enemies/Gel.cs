using System;
using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using cse3902.Constants;
using cse3902.Sounds;

namespace cse3902.Entities.Enemies
{
    public class Gel : IEntity
    {
        private GelSprite gelSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 center;
        private Vector2 previousCenter;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        private IEntity.EnemyType type;

        public Gel(Game1 game, Vector2 start, IEntity.EnemyType type)
        {
            this.game = game;
            center = start;
            previousCenter = center;
            this.type = type;

            //gel sprite sheet is 1 row, 2 columns
            gelSprite = (GelSprite)EnemySpriteFactory.Instance.CreateGelSprite(game.SpriteBatch, this.center);
            speed = 25.0f;
            travelDistance = 0;
            shoveDistance = -10;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 2;
        }

        public ref Rectangle Bounds
        {
            get => ref gelSprite.Box;
        }

        public void Attack()
        {
            //gels don' attack
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
            SoundFactory.PlaySound(SoundFactory.Instance.enemyDie);
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
	        this.collidable.ResetCollisions();
            if (this.shoveDistance > 0) ShoveMovement();
            else RegularMovement(gameTime);
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
                Random rand = new System.Random();
                int choice = rand.Next(0, 4);
                travelDistance = 100;

                switch (choice)
                {
                    case 0:
                        direction.X = 1;
                        direction.Y = 0;
                        break;
                    case 1:
                        direction.X = -1;
                        direction.Y = 0;
                        break;
                    case 2:
                        direction.X = 0;
                        direction.Y = 1;
                        break;
                    case 3:
                        direction.X = 0;
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

            gelSprite.Update(gameTime);
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            gelSprite.Draw();
        }

        public IEntity Duplicate()
        {
            return new Gel(game, center, type);
        }

        public IEntity.EnemyType Type
        {
            get => type;
        }

        public Vector2 Center
        {
            get => this.center;
            set
            {
                this.PreviousCenter = this.center;
                this.center = value;
                gelSprite.Center = value;
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
            get => 3;
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