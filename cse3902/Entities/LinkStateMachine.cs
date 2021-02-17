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
        private const double damageDelay = 5.0f;
        private double remainingDamageDelay;

        public LinkStateMachine(LinkSprite linkSprite, Vector2 centerPosition, SpriteBatch spriteBatch) 
	    {
            this.centerPosition = centerPosition;
	        mode = LinkMode.Still;
            currDirection = new Vector2(1, 0);
            speed = 1.0f;
            this.spriteBatch = spriteBatch;
            this.linkSprite = linkSprite;
            linkSprite.Callback = onSpriteAnimationComplete;
            health = healthMax;
            currWeaponIndex = 0;
            currItemIndex = 0;
            weapon = null;
            remainingDamageDelay = 0;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;

            // TODO: Update sprite direction here
            if (newDirection.X == 0 && newDirection.Y == 0)
            {
		        mode = LinkMode.Still;
                if (currDirection.X > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.LeftFacing);
                }
                if (currDirection.X < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.RightFacing);
                }
                if (currDirection.Y > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.UpFacing);
                }
                if (currDirection.Y < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.DownFacing);
                }
            }
            else
            {
			    currDirection = newDirection;
                mode = LinkMode.Moving;
                if(newDirection.X > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.LeftRunning);
                }
                if (newDirection.X < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.RightRunning);
                }
                if (newDirection.Y > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.UpRunning);
                }
                if (newDirection.Y < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.DownRunning);
                }
            }
        }

        private void onSpriteAnimationComplete() 
	    {
            if (mode == LinkMode.Attack)
            {
                mode = LinkMode.Still;
                weapon = null;
                ChangeDirection(new Vector2(0, 0));
            }
        }

	    public void Update(GameTime gameTime)
        {
            if (remainingDamageDelay > 0)
            {
                remainingDamageDelay -= gameTime.ElapsedGameTime.TotalSeconds;
                if(remainingDamageDelay > 0)
                {
                    linkSprite.Damaged = false;
                }
            }
            CenterPosition += currDirection * speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            linkSprite.Update(gameTime);
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
                Vector2 spriteSize = linkSprite.Bounds.Size.ToVector2();
                Vector2 offset = (spriteSize * currDirection) / 2;
                Vector2 startingPosition = offset + centerPosition;
                weapon = ItemSpriteFactory.Instance.CreateSwordWeapon(spriteBatch, CenterPosition, currDirection, currWeaponIndex);
                if (currDirection.X > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.LeftAttack);
                }
                if (currDirection.X < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.RightAttack);
                }
                if (currDirection.Y > 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.UpAttack);
                }
                if (currDirection.Y < 0)
                {
                    linkSprite.setFrameSet(LinkSprite.FrameIndex.DownAttack);
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
