using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Entities
{
    public class Link : IEntity
    {
        private List<int> items;
        private int currentItemIndex;
        // TODO: Add LinkStateMachine
        private ISprite linkSprite;
        private Game1 game;

        private Vector2 centerPosition;

	    public Link(Game1 game)
        {
            this.game = game;
            currentItemIndex = 0;
	        // TODO: Add init LinkStateMachine   
        }

        public Rectangle Bounds 
	    { 
	        get => linkSprite.Texture.Bounds; 
	    }

        public void Attack()
        {
            int itemDamage = items[currentItemIndex];

        }

        public void ChangeDirection()
        {
            // LinkStateMachine changeDirection
	    }        

        public void Die()
        {
            // LinkStateMachine die
        }

        public void TakeDamage()
        {
            // LinkStateMachine takeDamage aka. lose health
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
