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
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;

        public Aquamentus(Game1 game)
        {
            this.game = game;
            Texture2D aquamentusTexture = game.Content.Load<Texture2D>("aquamentus");
            startingPos = new Vector2(500, 200);
            center = startingPos;
            aquamentusSprite = new AquamentusSprite(game.spriteBatch, aquamentusTexture, 2, 2, center);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite, game.Content.Load<Texture2D>("fireball"), game.spriteBatch);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 50;
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
        public void Update(GameTime gameTime) {
            
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance) {

                direction.X = -1;

            } else if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance) {

                direction.X = 1;
            }

            aquamentusStateMachine.Update(gameTime);
            aquamentusStateMachine.ChangeDirection(direction);
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
