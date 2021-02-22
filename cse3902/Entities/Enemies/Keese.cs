using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Keese: IEntity
    {
        private KeeseSprite keeseSprite;
        private KeeseStateMachine keeseStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

        public Keese(Game1 game)
        {
            this.game = game;
            Texture2D linkTexture = game.Content.Load<Texture2D>("enemies/keese");
            centerPosition = new Vector2(500, 200);
            keeseSprite = new KeeseSprite(game.spriteBatch, linkTexture, 1, 2, centerPosition);
            keeseStateMachine = new KeeseStateMachine(keeseSprite);
            speed = 0.0f;
        }

        public Rectangle Bounds
        {
            get => keeseSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.keeseStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.keeseStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.keeseSprite.Erase();
        }

        public void Update(GameTime gameTime)
        {
            keeseSprite.Update(gameTime, onSpriteAnimationComplete);
            centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            this.keeseSprite.Draw();
        }
    }
}
