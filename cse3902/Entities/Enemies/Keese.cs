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

        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int radius = 50;
        private int degrees;

        public Keese(Game1 game)
        {
            this.game = game;
            Texture2D linkTexture = game.Content.Load<Texture2D>("enemies/keese");
            startingPos = new Vector2(500, 200);
            center = startingPos;

            keeseSprite = new KeeseSprite(game.spriteBatch, linkTexture, 1, 2, center);
            keeseStateMachine = new KeeseStateMachine(keeseSprite);
            degrees = 0;
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

            var radians = degrees + (Math.PI / 180);



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
        }

        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }

        public void Draw()
        {
            this.keeseSprite.Draw();
        }

        public Vector2 CenterPosition {

            get => this.center;
            set {
                this.center = value;
                keeseSprite.Center = value;
            }
        }
    }
}
