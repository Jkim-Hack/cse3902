﻿using System;
using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using cse3902.Projectiles;
using cse3902.Rooms;
using cse3902.Constants;
using cse3902.Sounds;
using cse3902.ParticleSystem;

namespace cse3902.Entities.Enemies
{
    public class Goriya : IEntity
    {
        private GoriyaSprite goriyaSprite;
        private GoriyaStateMachine goriyaStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private (Vector2 previous, Vector2 current) center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;
        private IProjectile boomerang;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        private (bool stun, float stunDuration) stun;

        public Goriya(Game1 game, Vector2 start)
        {
            this.game = game;
            center.current = start;
            center.previous = start;

            goriyaSprite = (GoriyaSprite)EnemySpriteFactory.Instance.CreateGoriyaSprite(game.SpriteBatch, center.current);
            goriyaStateMachine = new GoriyaStateMachine(goriyaSprite);

            travelDistance = 0;
            shoveDistance = MovementConstants.GoriyaShoveDistance;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = SettingsValues.Instance.GetValue(SettingsValues.Variable.GoriyaHealth);

            stun = (false, 0);
        }

        public ref Rectangle Bounds
        {
            get => ref goriyaStateMachine.Bounds;
        }

        public Boolean IsDetectionMode
        {
            get => Bounds != goriyaSprite.Box;
        }

        public void Attack()
        {
            //TODO add boomerang
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
            ChangeSpriteDirection(this.direction);
            goriyaStateMachine.Direction = this.direction;
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            if (this.Health > 0)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.enemyHit);
            }
            //this.goriyaSprite.Damaged = true;
            this.collidable.DamageDisabled = true;
        }

        public void ThrowBoomerang()
        {
            this.goriyaStateMachine.IsTriggered = true;
            boomerang = ProjectileHandler.Instance.CreateEnemyBoomerangItem(game.SpriteBatch, goriyaSprite, Direction);

        }

        public void Die()
        {
            SoundFactory.PlaySound(SoundFactory.Instance.enemyDie);
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center.current, (IEntity.EnemyType)SettingsValues.Instance.GetValue(SettingsValues.Variable.GoriyaEnemyType));
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center.current);
        }

        public void BeShoved()
        {
            this.shoveDistance = MovementConstants.GoriyaShoveDistance;
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
            goriyaStateMachine.Update();
	        this.collidable.ResetCollisions();
            if (this.shoveDistance > 0) ShoveMovement();
            else if (goriyaStateMachine.IsTriggered)
            {
                if (boomerang != null && !RoomProjectiles.Instance.projectiles.Contains(boomerang))
                {
                    goriyaStateMachine.IsTriggered = false;
                }
                return;
            }
            else RegularMovement(gameTime);
        }

        private void ShoveMovement()
        {
            this.Center += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            if (goriyaStateMachine.Bounds == goriyaSprite.Box)
            {
                return;//don't move when detection box is active
            }

            if (boomerang != null && RoomProjectiles.Instance.projectiles.Contains(boomerang))
            {
                return;
            }


            if (!stun.stun)
            {
                this.Center += direction * MovementConstants.GoriyaSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (travelDistance <= 0)
                {
                    travelDistance = MovementConstants.GoriyaMaxTravel;

                    RandomDirection();
                }
                else travelDistance--;
            }
            goriyaSprite.Update(gameTime);
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
            if (direction.X > 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.RightFacing;
            }
            else if (direction.X < 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.LeftFacing;
            }
            else if (direction.Y > 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.DownFacing;
            }
            else
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.UpFacing;
            }
        }

        public void Draw()
        {
            this.goriyaSprite.Draw();
        }

        public IEntity Duplicate()
        {
            return new Goriya(game, center.current);
        }

        public IEntity.EnemyType Type
        {
            get => (IEntity.EnemyType) SettingsValues.Instance.GetValue(SettingsValues.Variable.GoriyaEnemyType);
        }

        public Vector2 Center
        {
            get => this.center.current;
            set
            {
                this.PreviousCenter = this.center.current;
                this.center.current = value;
                goriyaSprite.Center = value;
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
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.GoriyaDamage);
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
