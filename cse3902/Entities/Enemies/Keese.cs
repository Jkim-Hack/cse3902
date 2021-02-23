using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.SpriteFactory;

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
        private int radius = 80;
        private float degrees;

        public Keese(Game1 game)
        {
            this.game = game;
            startingPos = new Vector2(500, 200);
            center = startingPos;

            keeseSprite = (KeeseSprite)EnemySpriteFactory.Instance.CreateKeeseSprite(game.spriteBatch, center);
            keeseStateMachine = new KeeseStateMachine(keeseSprite);
            degrees = 0;
            speed = 0.02f;
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
            this.keeseStateMachine.Die();
        }

        public void Update(GameTime gameTime)
        {
            var radians = degrees + (Math.PI / 180);
            var unitCirclePos = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
            CenterPosition = startingPos + (unitCirclePos * radius);

            degrees += speed;

            keeseSprite.Update(gameTime);
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
