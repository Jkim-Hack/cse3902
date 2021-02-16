using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Stalfos: IEntity
    {
        private StalfosSprite stalfosSprite;
        private StalfosStateMachine stalfosStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

        public Stalfos(Game1 game)
        {
            this.game = game;
            Texture2D stalfosTexture = game.Content.Load<Texture2D>("stalfos");
            centerPosition = new Vector2(200, 300);

            //stalfos sprite sheet is 1 row, 2 columns
            stalfosSprite = new StalfosSprite(game.spriteBatch, stalfosTexture, 1, 2, centerPosition);
            stalfosStateMachine = new StalfosStateMachine(stalfosSprite);
            speed = 0.0f;
        }

        public Rectangle Bounds
        {
            get => stalfosSprite.Texture.Bounds;
        }

        public void Attack()
        {
            this.stalfosStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.stalfosStateMachine.ChangeDirection(direction);
        }

        public void TakeDamage()
        {

        }

        public void Die()
        {
            //TODO: make the state machine handle this
            this.stalfosSprite.Erase();
        }
        public void Update(GameTime gameTime)
        {
            stalfosSprite.Update(gameTime);
            centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //TODO: Ientity should require clases to implement this I think
        public void Draw()
        {
            stalfosSprite.Draw();
        }


    }
}
