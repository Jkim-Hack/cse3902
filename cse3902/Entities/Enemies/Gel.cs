using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Gel : IEntity
    {
        private GelSprite gelSprite;
        private GelStateMachine gelStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

        public Gel(Game1 game)
        {
            this.game = game;
            Texture2D gelTexture = game.Content.Load<Texture2D>("gel");
            centerPosition = new Vector2(300, 400);

            //gel sprite sheet is 1 row, 2 columns
            gelSprite = new GelSprite(game.spriteBatch, gelTexture, 1, 2, centerPosition);
            gelStateMachine = new GelStateMachine(gelSprite);
            speed = 0.0f;
        }

        public Rectangle Bounds
        {
            get => gelSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.gelStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.gelStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.gelSprite.Erase();
        }
        public void Update(GameTime gameTime)
        {
            gelSprite.Update(gameTime, onSpriteAnimationComplete);
            centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            gelSprite.Draw();
        }


    }
}
