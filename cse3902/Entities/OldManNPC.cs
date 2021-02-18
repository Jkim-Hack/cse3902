using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities
{
    class OldManNPC : IEntity
    {
        private NPCSprite oldManSprite;

        private readonly Game1 game;

        private Vector2 centerPosition;

        private string message;

        public OldManNPC(Game1 game)
        {
            this.game = game;
            centerPosition = new Vector2(100, 200);
            oldManSprite = new NPCSprite(game.spriteBatch, game.Content.Load<Texture2D>("Old Man"), centerPosition);
        }
        public Rectangle Bounds
        {
            get => oldManSprite.Texture.Bounds;
        }

        public void Attack()
        {
            //NPCs don't attack
        }
        public void ChangeDirection(Vector2 direction)
        {
            //NPCs don't change direction
        }
        public void TakeDamage()
        {
            //NPCs don't take damage
        }
        public void Die()
        {
            //NPCs don't die
        }
        private void onSpriteAnimationComplete()
        {
            //nothing to callback
        }
        public void Update(GameTime gameTime)
        {
            oldManSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        public void setMessage(string msg)
        {
            message = msg;
        }
        public void Draw()
        {
            oldManSprite.Draw();
        }
    }
}
