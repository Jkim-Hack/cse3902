using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;
using cse3902.Constants;
using cse3902.Sounds;
using cse3902.ParticleSystem;
using cse3902.Rooms;

namespace cse3902.Entities.Enemies
{
    public class Keese : IEntity
    {
        private KeeseSprite keeseSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private (Vector2 previous, Vector2 current) center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        public Keese(Game1 game, Vector2 start)
        {
            this.game = game;
            center.current = start;
            center.previous = center.current;

            keeseSprite = (KeeseSprite)EnemySpriteFactory.Instance.CreateKeeseSprite(game.SpriteBatch, center.current);
            travelDistance = 0;
            shoveDistance = MovementConstants.StartingShoveDistance;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = SettingsValues.Instance.GetValue(SettingsValues.Variable.KeeseHealth);
        }

        public ref Rectangle Bounds
        {
            get => ref keeseSprite.Box;
        }

        public void Attack()
        {
            
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
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center.current, IEntity.EnemyType.X);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center.current);
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
            this.Center += direction * MovementConstants.KeeseSpeedNormal * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (travelDistance <= 0)
            {
                Random rand = new System.Random();
                int choice = rand.Next(0, 8);
                travelDistance = MovementConstants.KeeseMaxTravel;

                SetDirection(choice);

                
            }
            else
            {
                travelDistance--;
            }
            keeseSprite.Update(gameTime);
        }

        private void SetDirection(int choice)
        {
            if (choice == 0) direction = new Vector2(1, 0);
            else if (choice == 1) direction = new Vector2(-1, 0);
            else if (choice == 2) direction = new Vector2(0, 1);
            else if (choice == 3) direction = new Vector2(0, -1);
            else if (choice == 4) direction = new Vector2(1, 1);
            else if (choice == 5) direction = new Vector2(1, -1);
            else if (choice == 6) direction = new Vector2(-1, 1);
            else if (choice == 7) direction = new Vector2(-1, -1);

            direction.Normalize();
        }

        public void Draw()
        {
            this.keeseSprite.Draw();
        }

        public IEntity Duplicate()
        {
            return new Keese(game, center.current);
        }

        public IEntity.EnemyType Type
        {
            get => IEntity.EnemyType.X;
        }

        public Vector2 Center
        {
            get => this.center.current;
            set
            {
                this.center.previous = this.center.current;
                this.center.current = value;
                keeseSprite.Center = value;
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
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.KeeseDamage);
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

        public (bool Stun, float StunDuration) Stunned
        {
            get => (false, 0);
            set
            {
                if (value.Stun) health = 0;
            }
        }
    }
}