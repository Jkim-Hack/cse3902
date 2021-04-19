﻿using cse3902.Interfaces;
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
            marioBossStateMachine = new MarioBossStateMachine(marioBossSprite, game.SpriteBatch, this.center);
            direction = new Vector2(1, 0);
            speed = 10.0f;
            travelDistance = 20;
            shoveDistance = -10;
            shoveDirection = new Vector2(1, 0);
            pauseAnim = false;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;
            SoundFactory.PlaySound(SoundFactory.Instance.mariogreeting, 0.2f);

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 100;
            //todo: use this statement when it's available
            //health = SettingsValues.Instance.GetValue(SettingsValues.Variable.MarioBossHealth);
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
                SoundFactory.PlaySound(SoundFactory.Instance.bossHurt, 0.2f);
            }
            this.marioBossSprite.Damaged = true;
            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            SoundFactory.PlaySound(SoundFactory.Instance.bossDefeat, 0.2f);
            this.marioBossStateMachine.Die();
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center, IEntity.EnemyType.D);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center);
        }

        public void BeShoved()
        {
            this.shoveDistance = 20;
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
                travelDistance = 150;
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
            get => IEntity.EnemyType.D;
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
            get => 20;
            //todo: use this value once it's available
            //get => SettingsValues.Instance.GetValue(SettingsValues.Variable.MarioBossDamage);
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
