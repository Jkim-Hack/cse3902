﻿using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;

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

        public Aquamentus(Game1 game, Vector2 start)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;
            aquamentusSprite = (AquamentusSprite)EnemySpriteFactory.Instance.CreateAquamentusSprite(game.spriteBatch, center);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite, game.spriteBatch, this.center);
            direction = new Vector2(-1, 0);
            speed = 10.0f;
            travelDistance = 20;
            shoveDistance = -10;
            pauseAnim = false;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 10;
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
            this.aquamentusStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            this.aquamentusSprite.Damaged = true;
        }

        public void Die()
        {
            this.aquamentusStateMachine.Die();
        }

        public void BeShoved()
        {
            this.shoveDistance = 10;
            this.shoveDirection = new Vector2(direction.X * -2, 0);
            this.pauseAnim = true;
        }

        public void Update(GameTime gameTime)
        {

            if (this.shoveDistance > -10) ShoveMovement();
            else RegularMovement(gameTime);

            aquamentusStateMachine.Update(gameTime, this.CenterPosition, this.pauseAnim);
        }

        private void ShoveMovement()
        {
            if (this.shoveDistance >= 0) this.CenterPosition += shoveDirection;
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

            /* Doesn't seem like aquamentus really changes direction in the walkthrough */
            // ChangeDirection(direction);
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