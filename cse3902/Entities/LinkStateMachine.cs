using cse3902.Interfaces;
using cse3902.Projectiles;
using cse3902.Sprites;
using cse3902.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace cse3902.Entities
{
    public class LinkStateMachine : IEntityStateMachine
    {
        private enum LinkMode { Still, Moving, Attack, Item };

        private LinkMode mode;

        private LinkSprite linkSprite;
        private SpriteBatch spriteBatch;
        private Vector2 centerPosition;
        private Vector2 currDirection;

        private float speed;
        private Game1 game;

        private int totalHealth;
        private const int healthMax = 10;
        private int health;

        private const double damageDelay = 1.0f;
        private double remainingDamageDelay;

        private Vector2 shoveDirection;
        private int shoveDistance;
        private Boolean pauseMovement;

        public LinkStateMachine(Game1 game, LinkSprite linkSprite, Vector2 centerPosition, SpriteBatch spriteBatch)
        {
            this.centerPosition = centerPosition;
            mode = LinkMode.Still;
            this.game = game;
            linkInventory = new LinkInventory(game, this);

            currDirection = new Vector2(1, 0);
            speed = 50.0f;

            this.spriteBatch = spriteBatch;
            this.linkSprite = linkSprite;

            health = healthMax;
            totalHealth = healthMax + 4;
            currWeaponIndex = 0;
            currItemIndex = 0;

            remainingDamageDelay = damageDelay;

            shoveDistance = -10;
            PauseMovement = false;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            /* No need to update sprite if currently attacking or knocked back */
            if (mode == LinkMode.Attack || mode == LinkMode.Item || pauseMovement) return;

            if (newDirection.Equals(currDirection) && mode == LinkMode.Moving) return;

            if (newDirection.X == 0 && newDirection.Y == 0)
            {
                StillChangeDirection();
            }
            else
            {
                MovingChangeDirection(newDirection);
            }
        }

        private void StillChangeDirection()
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
        }

        private void MovingChangeDirection(Vector2 newDirection)
        {
            currDirection = newDirection;
            mode = LinkMode.Moving;
            if (newDirection.X > 0)
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

        public void BeShoved()
        {
            this.shoveDistance = 20;
            this.shoveDirection = -this.currDirection;
            this.PauseMovement  = true;
        }

        public void Update(GameTime gameTime)
        {
            UpdateDamageDelay(gameTime);
            

            if (this.shoveDistance > 0) ShoveMovement();
            else RegularMovement(gameTime);

            UpdateSprite(gameTime);
             
        }

        private void UpdateDamageDelay(GameTime gameTime)
        {
            if (remainingDamageDelay > 0 && linkSprite.Damaged)
            {
                remainingDamageDelay -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingDamageDelay <= 0)
                {
                    remainingDamageDelay = damageDelay;
                    linkSprite.Damaged = false;
                }
            }
        }

        private void ShoveMovement()
        {
            this.CenterPosition += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            PauseMovement = false;
            if (mode == LinkMode.Moving)
            {
                CenterPosition += currDirection * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void UpdateSprite(GameTime gameTime)
        {
            if(linkSprite.Update(gameTime) != 0)
            {
                if (mode == LinkMode.Attack|| mode == LinkMode.Item)
                {
                    if (mode == LinkMode.Item) Inventory.RemoveItemAnimation();
                    mode = LinkMode.Still;
                    ChangeDirection(new Vector2(0, 0));
                    
                }
            }
        }
        
        //TODO Move this just to sprite.draw in link.cs ?
        public void Draw()
        {
            linkSprite.Draw();
        }

        public void Attack()
        {
            if ((mode != LinkMode.Moving && mode != LinkMode.Still) || pauseMovement) return;
            mode = LinkMode.Attack;

            Vector2 startingPosition = getItemLocation(currDirection);
            linkInventory.CreateWeapon(startingPosition, currDirection);
            SetAttackAnimation();
        }

        public Vector2 CollectItemAnimation()
        {
            //The basic logic to use item. needs to add Pause Game during the duration and such..
            mode = LinkMode.Item;
            linkSprite.setFrameSet(LinkSprite.AnimationState.Item);
            return getItemLocation(new Vector2(0,-1));
        }

        public Vector2 UseItemAnimation()
        {
            if ((mode != LinkMode.Moving && mode != LinkMode.Still) || pauseMovement) return new Vector2(-1, -1);
            Vector2 startingPosition = getItemLocation(currDirection);
            mode = LinkMode.Attack;
            SetAttackAnimation();
            return startingPosition;
        }
        private Vector2 getItemLocation(Vector2 direction)
        {
            Vector2 spriteSize = linkSprite.Size;
            Vector2 offset = (spriteSize * direction) / 1.5f;
            return centerPosition + offset;
        }


        private void SetAttackAnimation()
        {
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

        public void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                if (remainingDamageDelay > 0 && linkSprite.Damaged) return;
                linkSprite.Damaged = true;
                health -= damage;
                remainingDamageDelay = damageDelay;
            }
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
            get => this.linkSprite.Center;
            set
            {
                this.linkSprite.PreviousCenter = this.linkSprite.Center;
                this.linkSprite.Center = value;
                this.centerPosition = value;
            }
        }

        public int TotalHealth
        {
            get => this.totalHealth;
        }
        
	    public int Health
        {
            get => this.health;
        }

        private Boolean PauseMovement
        {
            set
            {
                this.pauseMovement = value;
                linkSprite.PauseMovement = value;
            }
        }

        public LinkSprite Sprite
        {
            get => this.linkSprite;
        }

        public LinkInventory Inventory
        {
            get => linkInventory;
        }
    }
}
