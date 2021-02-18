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
	    private Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

	    public Link(Game1 game)
        {
            this.game = game;
            currentItemIndex = 0;
            // TODO Add this into sprite factory
            Texture2D linkTexture = game.Content.Load<Texture2D>("Link");
            Vector2 centerPosition = new Vector2(50, 200);
            linkSprite = new LinkSprite(game.spriteBatch, linkTexture, 9, 2, centerPosition);
            linkStateMachine = new LinkStateMachine(linkSprite, centerPosition, game.spriteBatch);
        }

        public Rectangle Bounds 
	    { 
	        get => linkSprite.Texture.Bounds; 
	    }

        public void Attack()
        {

            linkStateMachine.Attack();
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
            linkStateMachine.Update(gameTime);
        }

        public void Draw()
        {
            linkStateMachine.Draw();
        }

        public void Draw()
        {
            linkSprite.Draw();
        }

        public Vector2 Direction
        {
            get => linkStateMachine.Direction;
        }

        public float Speed
        {
            get => linkStateMachine.Speed;
            set => linkStateMachine.Speed = value;
        }
        
	    public Vector2 CenterPosition
        {
            get => linkStateMachine.CenterPosition;
            set => linkStateMachine.CenterPosition = value;
        }
    }
}
