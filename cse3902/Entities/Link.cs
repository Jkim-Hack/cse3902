using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities
{
    public class Link : IEntity
    {
        private List<int> items;
        private int currentItemIndex;

	    private LinkSprite linkSprite;
        private LinkStateMachine linkStateMachine;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

	    public Link(Game1 game)
        {
            this.game = game;
            currentItemIndex = 0;
            // TODO Add this into sprite factory
            Texture2D linkTexture = game.Content.Load<Texture2D>("Link");
        }

        public Rectangle Bounds 
	    { 
	        get => linkSprite.Texture.Bounds; 
	    }

        public void Attack()
        {
            // TODO: Add Item damages here
            // TODO: Add collision detection here

        }

        public void ChangeDirection(Vector2 direction)
        {
            this.direction = direction;

            linkStateMachine.ChangeDirection(direction);
	    }        

        public void Die()
        {
            this.linkSprite.Erase();
        }

        public void TakeDamage()
        {
            // TODO: Implement once collision is available
        }
        

        public void Update(GameTime gameTime)
        {
        }

        {
        {
        }

        public Vector2 Direction
        {
            get => linkStateMachine.Direction;
        }

        public float Speed
        {
        }
        
	    public Vector2 CenterPosition
        {
        }
    }
}
