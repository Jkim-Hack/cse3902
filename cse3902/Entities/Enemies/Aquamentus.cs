﻿using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Aquamentus: IEntity
    {
        private AquamentusSprite aquamentusSprite;
        private AquamentusStateMachine aquamentusStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
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
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite, game.Content.Load<Texture2D>("fireball"), game.spriteBatch, this.center);
            direction = new Vector2(-1.2f, 0);
            speed = 50.0f;
            travelDistance = 80;
            travelUp = false;
        }

        public Rectangle Bounds
        {
            get => aquamentusSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.aquamentusStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.aquamentusStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            this.aquamentusStateMachine.Die();
        }

        public void Update(GameTime gameTime) {
            
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance) {

                direction.X = 1f;
                direction.Y = 0.5f * (travelUp ? -1 : 1);
                travelUp = !travelUp;

            } else if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance) {

                direction.X = -1.2f;
                direction.Y = 0;
            }

            ChangeDirection(direction);
            aquamentusStateMachine.Update(gameTime, this.CenterPosition);
            
        }

        public void Draw()
        {
            aquamentusStateMachine.Draw();
        }

        public Vector2 CenterPosition {

            get => this.center;
            set {
                this.center = value;
                aquamentusSprite.Center = value;
            }
        }
    }
}
