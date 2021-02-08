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
            linkSprite = new LinkSprite(game.spriteBatch, linkTexture, 3, 3, new Vector2(50, 200));
            linkStateMachine = new LinkStateMachine(linkSprite);
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
            if (direction.X < 0)
            {
                linkStateMachine.FaceLeft();
            }
            else if (direction.X > 0)
            {
                linkStateMachine.FaceRight();
            }
            else if (direction.Y > 0)
            {
                linkStateMachine.FaceUp();
            }
            else if (direction.Y < 0)
            {
                linkStateMachine.FaceDown();
            } 
	    }        

        public void Die()
        {
            this.linkSprite.Erase();
        }

        public void TakeDamage()
        {
            // LinkStateMachine takeDamage aka. lose health
            // TODO: Implement once collision is available
        }

        public void Update(GameTime gameTime)
        {
            linkSprite.Update(gameTime);
	        centerPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public Vector2 Direction
        {
            get => this.direction;
            set
            {
                this.direction = value;
                ChangeDirection();
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
            set => this.centerPosition = value;
        }
    }
}
