using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Items;

namespace cse3902.Entities {

    public class LinkStateMachine : IEntityStateMachine {

        private enum LinkMode { Still, Moving, Attack };
        
        private LinkMode mode;

        private LinkSprite linkSprite;
        private SpriteBatch spriteBatch;
        private Vector2 centerPosition;
        private Vector2 currDirection;
        
        private float speed;
        
	    private int currWeaponIndex;
        private ISprite weapon;
        
	    private int currItemIndex;
        private List<ISprite> items;

        private const int healthMax = 10;
        private int health;
       
	     private const double damageDelay = 1.0f;
        private double remainingDamageDelay;

        public LinkStateMachine(LinkSprite linkSprite, Vector2 centerPosition, SpriteBatch spriteBatch) 
	    {
            this.centerPosition = centerPosition;
	        mode = LinkMode.Still;

            currDirection = new Vector2(0, 0);
            speed = 50.0f;
            
	        this.spriteBatch = spriteBatch;
            this.linkSprite = linkSprite;
            
	        items = new List<ISprite>();
            
	        health = healthMax;
            
	        currWeaponIndex = 0;
            currItemIndex = 0;
            weapon = null;
		
	        remainingDamageDelay = damageDelay;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;

            if (newDirection.X == 0 && newDirection.Y == 0)
            {
		        mode = LinkMode.Still;
                if (currDirection.X > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.RightFacing);
                }
                if (currDirection.X < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.LeftFacing);
                }
                if (currDirection.Y > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.DownFacing);
                }
                if (currDirection.Y < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.UpFacing);
                }
                speed = 0.0f;
            }
            else
            {
                speed = 50.0f;
			    currDirection = newDirection;
                mode = LinkMode.Moving;
                if(newDirection.X > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.RightRunning);
                }
                if (newDirection.X < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.LeftRunning);
                }
                if (newDirection.Y > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.DownRunning);
                }
                if (newDirection.Y < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.UpRunning);
                }
            }
        }

        private void onSpriteAnimationComplete() 
	    {
            if (mode == LinkMode.Attack)
            {
                mode = LinkMode.Still;
                weapon = null;
                speed = 0.0f;
                ChangeDirection(new Vector2(0, 0));
            }
        }

	    public void Update(GameTime gameTime)
        {
            if (remainingDamageDelay > 0 && linkSprite.Damaged)
            {
                remainingDamageDelay -= gameTime.ElapsedGameTime.TotalSeconds;
                if(remainingDamageDelay <= 0)
                {
                    remainingDamageDelay = damageDelay;
                    linkSprite.Damaged = false;
                }
            }
            if (weapon != null)
            {
                weapon.Update(gameTime, onSpriteAnimationComplete);
            }
            if (mode == LinkMode.Moving)
            {
		        CenterPosition += currDirection * speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            linkSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        public void Draw()
        {
            if(mode == LinkMode.Attack && weapon != null)
            {
                weapon.Draw();
               
            }
            foreach (ISprite sprite in items)
            {
                sprite.Draw();
            }
            linkSprite.Draw();
        }

        public void Attack()
        {
            if (mode == LinkMode.Moving || mode == LinkMode.Still)
            {
                mode = LinkMode.Attack;

                // TODO: Move this to Link.cs not needed in state machine
                Vector2 spriteSize = linkSprite.Size;
                Vector2 offset = (spriteSize * currDirection) / 2;
                Vector2 startingPosition = centerPosition + offset + (spriteSize / 2);
                
		        Console.WriteLine(startingPosition);
                Console.WriteLine(centerPosition);

                weapon = ItemSpriteFactory.Instance.CreateSwordWeapon(spriteBatch, startingPosition, currDirection, currWeaponIndex);
                
		        if (currDirection.X > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.RightAttack);
                }
                if (currDirection.X < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.LeftAttack);
                }
                if (currDirection.Y > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.DownAttack);
                }
                if (currDirection.Y < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.AnimationState.UpAttack);
                }
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
            linkSprite.Damaged = true;
            health -= damage;
            remainingDamageDelay = damageDelay;
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
