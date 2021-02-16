using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities.Enemies
{
    public class Stalfos
    {

        //private StalfosStateMachine aquamentusStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

        public Stalfos(Game1 game)
        {
            this.game = game;
            Texture2D stalfosTexture = game.Content.Load<Texture2D>("aquamentus");
            centerPosition = new Vector2(200, 300);
            aquamentusSprite = new AquamentusSprite(game.spriteBatch, stalfosTexture, 2, 2, centerPosition);
            aquamentusStateMachine = new AquamentusStateMachine(aquamentusSprite);
            speed = 0.0f;
        }
    }
}
