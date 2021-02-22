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
        private Vector2 center;

        public Aquamentus(Game1 game)
        {
            this.game = game;
            Texture2D aquamentusTexture = game.Content.Load<Texture2D>("aquamentus");
            center = new Vector2(200, 300);
            aquamentusSprite = new AquamentusSprite(game.spriteBatch, aquamentusTexture, 2, 2, center);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite, game.Content.Load<Texture2D>("fireball"), game.spriteBatch);
            direction = new Vector2(-1, 0);
            speed = 50.0f;


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
            direction.X = -1 * direction.X;
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
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            aquamentusStateMachine.ChangeDirection(direction);
            aquamentusStateMachine.Update(gameTime);
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
