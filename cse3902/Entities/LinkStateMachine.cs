using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites;
using System.Collections.Generic;

namespace cse3902.Entities {

    public class LinkStateMachine : IEntityStateMachine {

        private enum LinkMode { Still, Moving, Attack };
        
	    private IItem currentWeapon;
        private LinkMode mode;

        private LinkSprite linkSprite;
        private Vector2 centerPosition;
        private Vector2 currDirection;
        
        private float speed;
        private int currItemIndex;
        private List<IItem> items;

        private int health;
        
	    public LinkStateMachine(LinkSprite linkSprite, Vector2 centerPosition) 
	    {
            this.centerPosition = centerPosition;
            // TODO: Add default weapon once weapon items are done
	        mode = LinkMode.Still;
            currDirection = new Vector2(1, 0);
            speed = 1.0f;
            this.linkSprite = linkSprite;
            health = 10;
            currItemIndex = 0;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;

            // TODO: Update sprite direction here
            if (newDirection.X == 0 && newDirection.Y == 0)
            {
		        mode = LinkMode.Still;
            }
            else
            {
			    currDirection = newDirection;
                mode = LinkMode.Moving;
            }
        }

        private void onSpriteAnimationComplete() 
	    {
            if (mode == LinkMode.Attack)
            {
                mode = LinkMode.Still;
            }
        }

	    public void Update(GameTime gameTime)
        {
            CenterPosition += currDirection * speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            linkSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        public void Draw()
        {
            if(mode == LinkMode.Attack && currentWeapon != null)
            {
                // TODO: Draw Weapon
               
            }
            if(items.Count > 0)
            {
                // TODO: Draw projectiles
            }
            linkSprite.Draw();
        }

        public void Attack()
        {
            if (mode == LinkMode.Moving || mode == LinkMode.Still)
            {
                mode = LinkMode.Attack;
                Vector2 spriteSize = linkSprite.Bounds.Size.ToVector2();
                Vector2 offset = (spriteSize * currDirection) / 2;
                Vector2 startingPosition = offset + centerPosition; 
			    // TODO: Spawn/use weapon here
            }
        }

        public void UseItem()
        {
            // TODO: Ask Smera how she wants to use items
        }

        public void ChangeItem(int index)
        {
            currItemIndex = index;
        }

        public void TakeDamage(int damage)
        {

        }

        public void CycleWeapon(int dir)
        {
            throw new NotImplementedException();
        }

        public Vector2 Direction
        {
            get => this.currDirection;
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
