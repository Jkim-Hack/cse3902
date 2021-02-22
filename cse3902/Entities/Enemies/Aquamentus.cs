using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.Sprites;
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
        private Boolean travelUp;

        public Aquamentus(Game1 game)
        {
            this.game = game;
            Texture2D aquamentusTexture = game.Content.Load<Texture2D>("aquamentus");

            startingPos = new Vector2(500, 200);
            center = startingPos;
            aquamentusSprite = new AquamentusSprite(game.spriteBatch, aquamentusTexture, 2, 2, center);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite, game.Content.Load<Texture2D>("fireball"), game.spriteBatch, this.center);
            direction = new Vector2(-1.2f, 0);
            speed = 50.0f;
            travelDistance = 80;
            travelUp = false;
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

        public void Update(GameTime gameTime) {
            
            this.CenterPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (direction.X < 0 && CenterPosition.X < startingPos.X - travelDistance) {

                direction.X = 1f;
                direction.Y = 0.5f * (travelUp ? -1 : 1);
                travelUp = !travelUp;

            } else if (direction.X > 0 && CenterPosition.X > startingPos.X + travelDistance) {

                direction.X = -1.2f;
                direction.Y = 0;
            }

            ChangeDirection(direction);
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
