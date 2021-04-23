using System;
using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using cse3902.Constants;
using cse3902.Sounds;
using cse3902.ParticleSystem;

namespace cse3902.Entities.Enemies
{
    public class Stalfos : IEntity
    {
        private StalfosSprite stalfosSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private (Vector2 previous, Vector2 current) center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        private (bool stun, float stunDuration) stun;

        public Stalfos(Game1 game, Vector2 start)
        {
            this.game = game;
            center.current = start;
            center.previous = start;

            //stalfos sprite sheet is 1 row, 2 columns
            stalfosSprite = (StalfosSprite)EnemySpriteFactory.Instance.CreateStalfosSprite(game.SpriteBatch, center.current);
            speed = MovementConstants.StalfosSpeed;
            travelDistance = 0;
            shoveDistance = MovementConstants.DefaultShoveDistance;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = SettingsValues.Instance.GetValue(SettingsValues.Variable.StalfosHealth);

            stun = (false, 0);
        }

        public ref Rectangle Bounds
        {
            get => ref stalfosSprite.Box;
        }

        public void Attack()
        {
            
        }

        public void ChangeDirection(Vector2 direction)
        {
            //direction vector of (0,0) indicates just reverse the current direction
            if (direction == new Vector2(0, 0))
            {
                this.direction = -this.direction;
            } else
            {
                this.direction = direction;
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
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center.current, IEntity.EnemyType.C);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center.current);
        }

        public void BeShoved()
        {
            this.shoveDistance = MovementConstants.StalfosShoveDistance;
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

        private void UpdateStun(GameTime gameTime)
        {
            stun.stunDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            stun.stun = stun.stunDuration > 0;
        }

        public void Update(GameTime gameTime)
        {
            UpdateStun(gameTime);
            UpdateDamage(gameTime);
	        this.collidable.ResetCollisions();
            if (this.shoveDistance > 0) ShoveMovement();
            else RegularMovement(gameTime);
        }

        public void Draw()
        {
            stalfosSprite.Draw();
        }

        private void ShoveMovement()
        {
            this.Center += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            if (!stun.stun)
            {
                this.Center += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (travelDistance <= 0)
                {
                    travelDistance = MovementConstants.StalfosMaxTravel;
                    RandomDirection();
                }
                else
                {
                    travelDistance--;
                }
            }
            stalfosSprite.Update(gameTime);
        }

        private void RandomDirection()
        {
            Random rand = new System.Random();
            int choice = rand.Next(0, 4);

            switch (choice)
            {
                case 0:
                    ChangeDirection(new Vector2(1, 0));
                    break;
                case 1:
                    ChangeDirection(new Vector2(-1, 0));
                    break;
                case 2:
                    ChangeDirection(new Vector2(0, 1));
                    break;
                case 3:
                    ChangeDirection(new Vector2(0, -1));
                    break;
                default:
                    break;
            }
        }

        public IEntity Duplicate()
        {
            return new Stalfos(game, center.current);
        }

        public IEntity.EnemyType Type
        {
            get => IEntity.EnemyType.C;
        }

        public Vector2 Center
        {
            get => this.center.current;
            set
            {
                this.PreviousCenter = this.center.current;
                this.center.current = value;
                stalfosSprite.Center = value;
            }
        }

        public Vector2 PreviousCenter
        {
            get => this.center.previous;
            set
            {
                this.center.previous = value;
            }
        }

        public int Damage
        {
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.StalfosDamage);
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

        public (bool, float) Stunned
        {
            get => stun;
            set => stun = value;
        }
    }
}