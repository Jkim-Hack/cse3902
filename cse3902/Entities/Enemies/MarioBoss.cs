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

namespace cse3902.Entities.Enemies
{
    public class MarioBoss : IEntity
    {
        private MarioBossSprite marioBossSprite;
        private MarioBossStateMachine marioBossStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 center;
        private Vector2 previousCenter;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;
        private Boolean pauseAnim;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        public MarioBoss(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;
            previousCenter = center;
            marioBossSprite = (MarioBossSprite)EnemySpriteFactory.Instance.CreateMarioBossSprite(game.SpriteBatch, center);
            marioBossStateMachine = new MarioBossStateMachine(marioBossSprite, game.SpriteBatch, this.center, game.Player);
            direction = new Vector2(1, 0);
            speed = MovementConstants.MarioSpeed;
            travelDistance = MovementConstants.MarioMaxTravel;
            shoveDistance = 0;
            shoveDirection = new Vector2(1, 0);
            pauseAnim = false;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;
            

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = SettingsValues.Instance.GetValue(SettingsValues.Variable.MarioHealth);
        }

        public ref Rectangle Bounds
        {
            get => ref marioBossSprite.Box;
        }

        public void Attack()
        {
            this.marioBossStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            //todo: make a more fleshed out implementation for this
            this.direction = -this.direction;
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            if (this.Health > 0)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.bossHurt, MovementConstants.MarioDelay);
            }
            this.marioBossSprite.Damaged = true;
            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            SoundFactory.PlaySound(SoundFactory.Instance.bossDefeat, MovementConstants.MarioDelay);
            this.marioBossStateMachine.Die();
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center, IEntity.EnemyType.A);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center);
        }

        public void BeShoved()
        {
            shoveDistance = MovementConstants.MarioShoveDistance;
            this.pauseAnim = true;
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
                    marioBossSprite.Damaged = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateDamage(gameTime);
            if (this.shoveDistance > 0) ShoveMovement();
            else RegularMovement(gameTime);
            this.collidable.ResetCollisions();

            marioBossStateMachine.Update(gameTime, this.Center, this.pauseAnim);
        }

        private void ShoveMovement()
        {
            this.Center += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            pauseAnim = false;

            this.Center += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (travelDistance <= 0)
            {
                direction.X *= -1;
                travelDistance = MovementConstants.MarioMaxTravel;
            }
            else
            {
                travelDistance--;
            }

        }

        public void Draw()
        {
            marioBossStateMachine.Draw();
        }

        public IEntity Duplicate()
        {
            return new MarioBoss(game, center);
        }

        public IEntity.EnemyType Type
        {
            get => IEntity.EnemyType.A;
        }

        public Vector2 Center
        {
            get => this.center;
            set
            {
                this.PreviousCenter = this.center;
                this.center = value;
                marioBossSprite.Center = value;
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
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.MarioDamage);
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
