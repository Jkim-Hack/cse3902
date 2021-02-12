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
	    private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 centerPosition;

	    public Link(Game1 game)
        {
            this.game = game;
            currentItemIndex = 0;
            Texture2D linkTexture = game.Content.Load<Texture2D>("Link");
            centerPosition = new Vector2(50, 200);
            linkSprite = new LinkSprite(game.spriteBatch, linkTexture, 3, 3, centerPosition);
            linkStateMachine = new LinkStateMachine(linkSprite);
            direction = new Vector2(0,0);
            speed = 0.0f;
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
            /*TODO: this cannot stay.  You are assuming that all commands will
             * change direction, while sometimes they wont, at least not immediately (like currently attacking) */
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
            //TODO: what should update animation stage for items and attacking
            linkSprite.Update(gameTime);
	        centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public Vector2 Direction
        {
            get => this.direction;
            set
            {
                this.direction = value;
                ChangeDirection(value);
            }
        }

        public float Speed
        {
            get => this.speed;
            set => this.speed = value;
        }
        
	    public Vector2 CenterPosition
        {
            get => this.centerPosition;
            set
	        {
                this.linkSprite.Center = value;
                this.centerPosition = value;
            }
        }
    }
}
