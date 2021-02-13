using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities
{
    class MerchantNPC : IEntity
    {
        private NPCSprite merchantSprite;

        private readonly Game1 game;

        private Vector2 centerPosition;

        public MerchantNPC(Game1 game)
        {
            this.game = game;
            centerPosition = new Vector2(100, 200);
            merchantSprite = new NPCSprite(game.spriteBatch, game.Content.Load<Texture2D>("Merchant"), centerPosition);
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
        public void Update(GameTime gameTime)
        {
            //NPCs have nothing to update
        }
    }
}
