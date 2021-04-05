using cse3902.Sounds;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using System;
using cse3902.Constants;

namespace cse3902.Entities
{
    public class LinkStateMachine : IEntityStateMachine
    {
        private enum LinkMode { Still, Moving, Attack, Item, GameWon, Death };

        private LinkMode mode;

        private LinkSprite linkSprite;
        private LinkInventory linkInventory;
        private Vector2 centerPosition;
        private Vector2 currDirection;

        //todo: for testing only
        public float speed;

        private int totalHealth;
        private int health;

        private double remainingDamageDelay;
        private int lowHealthSoundDelay;

        private Vector2 shoveDirection;
        private int shoveDistance;
        private Boolean pauseMovement;
        private Boolean isGrabbed;


        public LinkStateMachine(Game1 game, LinkSprite linkSprite, Vector2 centerPosition)
        {
            this.centerPosition = centerPosition;
            mode = LinkMode.Still;
            linkInventory = new LinkInventory(game, this);

            currDirection = new Vector2(LinkConstants.defaultXDirection, LinkConstants.defaultYDirection);
            speed = LinkConstants.defaultSpeed;

            this.linkSprite = linkSprite;

            totalHealth = HeartConstants.DefaultHeartCount;
            health = totalHealth;

            remainingDamageDelay = DamageConstants.DamageDisableDelay;
            lowHealthSoundDelay = LinkConstants.defaultSoundDelay;

            shoveDistance = LinkConstants.defaultShoveDistance;
            PauseMovement = false;
            isGrabbed = false;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            /* No need to update sprite if currently attacking or knocked back */
            if (mode == LinkMode.Attack || mode == LinkMode.Item || mode == LinkMode.GameWon || pauseMovement) return;

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
            if (this.isGrabbed)
            {
                if (this.shoveDistance < 0)
                {
                    isGrabbed = false;
                    this.speed = LinkConstants.defaultSpeed;
                } else
                {
                    CenterPosition += currDirection * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.shoveDistance--;
                }
                return;
                
            }

            UpdateDamageDelay(gameTime);
            if (this.shoveDistance > 0) ShoveMovement();
            else RegularMovement(gameTime);

            if (linkSprite.Update(gameTime) != 0)
            {
                if (mode == LinkMode.Attack || mode == LinkMode.Item || mode == LinkMode.GameWon)
                {
                    if (mode == LinkMode.Item || mode == LinkMode.GameWon) Inventory.RemoveItemAnimation();
                    mode = LinkMode.Still;
                    ChangeDirection(new Vector2(0, 0));
                }
                else if (mode == LinkMode.Death)
                {
                    linkSprite.DeathMaskHandler.LoadMask();
                    mode = LinkMode.Still;
                    this.pauseMovement = true;
                    linkSprite.setFrameSet(LinkSprite.AnimationState.DownFacing);
                    linkSprite.DamageMaskHandler.Disabled = false;
                }
            }

            if (health <= 2)
            {
                lowHealthSoundDelay--;
                if (lowHealthSoundDelay == 0)
                {
                    lowHealthSoundDelay = LinkConstants.defaultSoundDelay;
                    SoundFactory.PlaySound(SoundFactory.Instance.lowHealth);
                }
            }
        }

        private void UpdateDamageDelay(GameTime gameTime)
        {
            if (remainingDamageDelay > 0 && linkSprite.Damaged)
            {
                remainingDamageDelay -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingDamageDelay <= 0)
                {
                    remainingDamageDelay = DamageConstants.DamageDisableDelay;
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

        public void Attack()
        {
            if ((mode != LinkMode.Moving && mode != LinkMode.Still) || pauseMovement) return;
            mode = LinkMode.Attack;
            SoundFactory.PlaySound(SoundFactory.Instance.swordSlash);

            Vector2 startingPosition = getItemLocation(currDirection);
            linkInventory.CreateWeapon(startingPosition, currDirection);
            if(HeartConstants.swordProjectileMinHealth <= health)
            {
                linkInventory.CreateSwordProjectile(startingPosition, currDirection);
            }
            SetAttackAnimation();
        }

        public Vector2 CollectItemAnimation()
        {
            //The basic logic to use item. needs to add Pause Game during the duration and such..
            mode = LinkMode.Item;
            linkSprite.setFrameSet(LinkSprite.AnimationState.Item);
            return getItemLocation(new Vector2(0,-1));
        }

        public Vector2 GameWonAnimation()
        {
            //The basic logic to use item. needs to add Pause Game during the duration and such..
            mode = LinkMode.GameWon;
            linkSprite.setFrameSet(LinkSprite.AnimationState.GameWon);
            linkSprite.SetGameWon();
            return getItemLocation(new Vector2(0,-1));
        }

        public void UseItem()
        {
            if ((mode != LinkMode.Moving && mode != LinkMode.Still) || pauseMovement) return;
            Vector2 startingPosition = getItemLocation(currDirection);
            if (linkInventory.CreateItem(startingPosition))
            {
                mode = LinkMode.Attack;
                SetAttackAnimation();
            }
            return;
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
                remainingDamageDelay = DamageConstants.DamageDisableDelay;
            }

            SoundFactory.PlaySound(SoundFactory.Instance.linkHit);
        }

        public void BeGrabbed(IEntity enemy, float speed)
        {
            this.isGrabbed = true;
            this.CenterPosition = enemy.Center;
            this.currDirection = enemy.Direction;
            this.speed = speed;
            //todo: magic number
            this.shoveDistance = 100;
        }

        public void Die()
        {
            linkSprite.setFrameSet(LinkSprite.AnimationState.Death);
            linkSprite.DamageMaskHandler.Disabled = true;
            mode = LinkMode.Death;
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
            set => this.totalHealth = value;
        }
        
	    public int Health
        {
            get => this.health;
            set => this.health = value;
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
