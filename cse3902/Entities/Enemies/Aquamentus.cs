﻿using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Entities.Enemies
{
    public class Aquamentus : IEntity
    {
        private AquamentusSprite aquamentusSprite;
        private AquamentusStateMachine aquamentusStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private Vector2 shoveDirection;
        private int shoveDistance;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;
        private Boolean travelUp;

        public Aquamentus(Game1 game)
        {
            this.game = game;

            startingPos = new Vector2(500, 200);
            center = startingPos;
            aquamentusSprite = (AquamentusSprite)EnemySpriteFactory.Instance.CreateAquamentusSprite(game.spriteBatch, center);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite, game.spriteBatch, this.center);
            direction = new Vector2(-1.2f, 0);
            speed = 50.0f;
            travelDistance = 80;
            travelUp = false;
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
        }

        public void Die()
        {
            this.aquamentusStateMachine.Die();
        }

        public void BeShoved()
        {
            this.shoveDistance = 15;
            this.shoveDirection = new Vector2(direction.X * -2, 0);

        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) BeShoved();

            if (this.shoveDistance > 0)
            {

                this.CenterPosition += shoveDirection;
                shoveDistance--;

            }
            else
            {
                this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance)
                {
                    direction.X = 1f;
                    direction.Y = 0.5f * (travelUp ? -1 : 1);
                    travelUp = !travelUp;
                }
                else if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance)
                {
                    direction.X = -1.2f;
                    direction.Y = 0;
                }

                ChangeDirection(direction);
            }

            aquamentusStateMachine.Update(gameTime, this.CenterPosition);
        }

        private void RegularMovement()
        {
            
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
    }
}