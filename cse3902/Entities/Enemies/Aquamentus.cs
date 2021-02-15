using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
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
        private Vector2 centerPosition;

        public Aquamentus(Game1 game)
        {
            this.game = game;
            Texture2D linkTexture = game.Content.Load<Texture2D>("aquamentus");
            centerPosition = new Vector2(200, 300);
            aquamentusSprite = new AquamentusSprite(game.spriteBatch, linkTexture, 2, 2, centerPosition);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite);
            speed = 0.0f;


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
            //TODO: make the state machine handle this
            this.aquamentusSprite.Erase();
        }
        public void Update(GameTime gameTime)
        {
            aquamentusSprite.Update(gameTime);
            centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //TODO: Ientity should require clases to implement this I think
        public void Draw()
        {
            aquamentusSprite.Draw();
        }
    }
}
