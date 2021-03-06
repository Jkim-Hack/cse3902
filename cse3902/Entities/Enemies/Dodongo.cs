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
        private (Vector2 previous, Vector2 current) center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        public Dodongo(Game1 game, Vector2 start)
        {
            this.game = game;
            center.current = start;
            center.previous = center.current;

            dodongoSprite = (DodongoSprite)EnemySpriteFactory.Instance.CreateDodongoSprite(game.SpriteBatch, center.current);
            travelDistance = MovementConstants.DodongoMaxTravel;
            shoveDistance = 0;
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
            if (this.dodongoSprite.StartingFrameIndex == (int)DodongoSprite.FrameIndex.LeftFacing || this.dodongoSprite.StartingFrameIndex == (int)DodongoSprite.FrameIndex.RightFacing)
            {
                this.dodongoSprite.StartingFrameIndex++;
            }
            this.dodongoSprite.StartingFrameIndex++;

            this.shoveDirection = new Vector2(0, 0);
            this.shoveDistance = MovementConstants.DodongoShoveDistance;
            this.travelDistance = 0;

            this.Health -= damage;
            SoundFactory.PlaySound(SoundFactory.Instance.bossHurt);
            

            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            SoundFactory.PlaySound(SoundFactory.Instance.bossDefeat);
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center.current, IEntity.EnemyType.D);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center.current);
        }

        public void BeShoved()
        {
            
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
            this.Center += direction * MovementConstants.DodongoSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (travelDistance <= 0)
            {
                travelDistance = MovementConstants.DodongoMaxTravel;

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
            return new Dodongo(game, center.current);
        }

        public IEntity.EnemyType Type
        {
            get => IEntity.EnemyType.D;
        }

        public Vector2 Center
        {
            get => this.center.current;
            set
            {
                this.PreviousCenter = this.center.current;
                this.center.current = value;
                dodongoSprite.Center = value;
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

        public (bool, float) Stunned
        {
            get => (false, 0);
            set {}
        }
    }
}
