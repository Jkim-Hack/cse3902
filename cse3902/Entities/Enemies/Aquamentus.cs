using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;
using cse3902.Constants;

namespace cse3902.Entities.Enemies
{
    public class Aquamentus : IEntity
    {
        private AquamentusSprite aquamentusSprite;
        private AquamentusStateMachine aquamentusStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;
        private Boolean pauseAnim;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        public Aquamentus(Game1 game, Vector2 start)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;
            aquamentusSprite = (AquamentusSprite)EnemySpriteFactory.Instance.CreateAquamentusSprite(game.SpriteBatch, center);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite, game.SpriteBatch, this.center);
            direction = new Vector2(-1, 0);
            speed = 10.0f;
            travelDistance = 20;
            shoveDistance = -10;
            pauseAnim = false;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 20;
        }

        public Vector2 Center
        {
            get => this.center;
        }

        public ref Rectangle Bounds
        {
            get => ref aquamentusSprite.Box;
        }

        public void Attack()
        {
            this.aquamentusStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            //todo: make a more fleshed out implementation for this
            this.direction = -this.direction;
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            this.aquamentusSprite.Damaged = true;
            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            this.aquamentusStateMachine.Die();
        }

        public void BeShoved()
        {
            this.shoveDistance = 20;
            this.shoveDirection = -this.direction;
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
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateDamage(gameTime);
	        if (this.shoveDistance > -10) ShoveMovement();
            else RegularMovement(gameTime);
            this.collidable.ResetCollisions();

            aquamentusStateMachine.Update(gameTime, this.CenterPosition, this.pauseAnim);
        }

        private void ShoveMovement()
        {
            this.CenterPosition += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            pauseAnim = false;

            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            aquamentusStateMachine.Draw();
        }

        public Vector2 CenterPosition
        {
            get => this.center;
            set
            {
                this.center = value;
                aquamentusSprite.Center = value;
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