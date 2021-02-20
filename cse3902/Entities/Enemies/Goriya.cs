using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Goriya: IEntity
    {
        private GoriyaSprite goriyaSprite;
        private GoriyaStateMachine goriyaStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

        public Goriya(Game1 game)
        {
            this.game = game;
            Texture2D linkTexture = game.Content.Load<Texture2D>("enemies/goriya_blue");
            centerPosition = new Vector2(200, 300);
            goriyaSprite = new GoriyaSprite(game.spriteBatch, linkTexture, 2, 2, centerPosition);
            goriyaStateMachine = new GoriyaStateMachine(goriyaSprite);
            speed = 0.0f;


        }

        public Rectangle Bounds
        {
            get => goriyaSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.goriyaStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.goriyaStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.goriyaSprite.Erase();
        }

        public void Update(GameTime gameTime)
        {
            goriyaSprite.Update(gameTime, onSpriteAnimationComplete);
            centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            this.goriyaSprite.Draw();
        }
    }
}
