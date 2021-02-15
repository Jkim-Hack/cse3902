using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Aquamentus: IEntity
    {
        private AquamentusSprite aquamentusSprite;

        
       
        
        private EnemyStateMachine enemyStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private Vector2 centerPosition;

        public Aquamentus(Game1 game)
        {
            this.game = game;
            Texture2D linkTexture = game.Content.Load<Texture2D>("aquamentus");
            centerPosition = new Vector2(200, 300);
            linkSprite = new LinkSprite(game.spriteBatch, linkTexture, 3, 3, centerPosition);
            linkStateMachine = new LinkStateMachine(linkSprite);
        }

        public Rectangle Bounds
        {
            get => aquamentusSprite.Texture.Bounds;
        }
        public void Attack()
        {

        }
        public void ChangeDirection(Vector2 direction)
        {

        }
        public void TakeDamage()
        {

        }
        public void Die()
        {

        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
