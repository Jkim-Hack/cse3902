using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities
{
    class MerchantNPC : IEntity
    {
        private ISprite merchantSprite;

        private readonly Game1 game;

        private Vector2 centerPosition;

        private string message;

        public MerchantNPC(Game1 game)
        {
            this.game = game;
            centerPosition = new Vector2(100, 200);
            merchantSprite = NPCSpriteFactory.Instance.CreateMerchantSprite(game.spriteBatch, centerPosition);
        }
        public Rectangle Bounds
        {
            get => merchantSprite.Texture.Bounds;
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
            merchantSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        public void setMessage(string msg)
        {
            message = msg;
        }
        public void Draw()
        {
            merchantSprite.Draw();
        }
    }
}
