﻿using System;
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
    public class Dodongo : IEntity
    {
        private DodongoSprite dodongoSprite;
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

        public Dodongo(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;
            previousCenter = center;

            dodongoSprite = (DodongoSprite)EnemySpriteFactory.Instance.CreateDodongoSprite(game.SpriteBatch, center);
            speed = 25.0f;
            travelDistance = 0;
            shoveDistance = -10;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = SettingsValues.Instance.GetValue(SettingsValues.Variable.DodongoHealth);
        }

        public ref Rectangle Bounds
        {
            get => ref dodongoSprite.Box;
        }

        public void Attack()
        {

        }

        public void ChangeDirection(Vector2 direction)
        {
            //direction vector of (0,0) gives opposite direction
            if (direction == new Vector2(0, 0))
            {
                this.direction = -this.direction;
            }
            else
            {
                this.direction = direction;
            }
            ChangeSpriteDirection(direction);

        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            if (this.Health > 0)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.enemyHit);
            }
            //this.dodongoSprite.Damaged = true;
            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            SoundFactory.PlaySound(SoundFactory.Instance.enemyDie);
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center, IEntity.EnemyType.D);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center);
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

        public void SwallowBomb()
        {

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
            if (this.shoveDistance > -10) ShoveMovement();
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
                travelDistance = 125;

                RandomDirection();
            }
            else travelDistance--;

            dodongoSprite.Update(gameTime);
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

        private void ChangeSpriteDirection(Vector2 direction)
        {
            if (direction == new Vector2(0, 0))
            {
                //direction vector of (0,0) indicates just reverse the current direction
                if (dodongoSprite.StartingFrameIndex == (int)DodongoSprite.FrameIndex.RightFacing)
                {
                    dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.LeftFacing;
                }
                else if (dodongoSprite.StartingFrameIndex == (int)DodongoSprite.FrameIndex.LeftFacing)
                {
                    dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.RightFacing;
                }
                else if (dodongoSprite.StartingFrameIndex == (int)DodongoSprite.FrameIndex.UpFacing)
                {
                    dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.DownFacing;
                }
                else if (dodongoSprite.StartingFrameIndex == (int)DodongoSprite.FrameIndex.DownFacing)
                {
                    dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.UpFacing;
                }

                return;
            }

            if (direction.X > 0)
            {
                dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.RightFacing;
            }
            else if (direction.X < 0)
            {
                dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.LeftFacing;
            }
            else if (direction.Y > 0)
            {
                dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.DownFacing;
            }
            else
            {
                dodongoSprite.StartingFrameIndex = (int)DodongoSprite.FrameIndex.UpFacing;
            }
        }

        public void Draw()
        {
            this.dodongoSprite.Draw();
        }

        public IEntity Duplicate()
        {
            return new Dodongo(game, center);
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
                dodongoSprite.Center = value;
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
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.DodongoDamage);
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
