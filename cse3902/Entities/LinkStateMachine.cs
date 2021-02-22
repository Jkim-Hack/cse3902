using cse3902.Interfaces;
using cse3902.Items;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Entities
{
    public class LinkStateMachine : IEntityStateMachine
    {
        private enum LinkMode { Still, Moving, Attack };

        private LinkMode mode;

        private LinkSprite linkSprite;
        private SpriteBatch spriteBatch;
        private Vector2 centerPosition;
        private Vector2 currDirection;

        private float speed;

        private int currItemIndex;
        private int currWeaponIndex;
        private Game1 game;

        private const int healthMax = 10;
        private int health;

        private const double damageDelay = 3.0f;
        private double remainingDamageDelay;

        public LinkStateMachine(Game1 game, LinkSprite linkSprite, Vector2 centerPosition, SpriteBatch spriteBatch)
        {
            this.centerPosition = centerPosition;
            mode = LinkMode.Still;
            this.game = game;

            currDirection = new Vector2(1, 0);
            speed = 50.0f;

            this.spriteBatch = spriteBatch;
            this.linkSprite = linkSprite;

            health = healthMax;

            currWeaponIndex = 0;
            currItemIndex = 0;

            remainingDamageDelay = damageDelay;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;

            if (newDirection.Equals(currDirection) && mode == LinkMode.Moving) return;

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
            }
            else
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
        }

        private void onSpriteAnimationComplete()
        {
            if (mode == LinkMode.Attack)
            {
                mode = LinkMode.Still;
                ChangeDirection(new Vector2(0, 0));
            }
        }

        public void Update(GameTime gameTime)
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
            if (mode == LinkMode.Moving)
            {
                CenterPosition += currDirection * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            linkSprite.Update(gameTime, onSpriteAnimationComplete);
        }

        public void Draw()
        {
            linkSprite.Draw();
        }

        public void Attack()
        {
            if (mode != LinkMode.Moving && mode != LinkMode.Still) return;
            mode = LinkMode.Attack;

            // TODO: Move this to Link.cs not needed in state machine
            Vector2 spriteSize = linkSprite.Size;
            Vector2 offset = (spriteSize * currDirection) / 1.5f;
            Vector2 startingPosition = centerPosition + offset + (spriteSize / 2);

            IProjectile weapon = (IProjectile)ItemSpriteFactory.Instance.CreateSwordWeapon(spriteBatch, startingPosition, currDirection, currWeaponIndex);
            game.linkProjectiles.Add(weapon);
            SetAttackAnimation();

        }

        public void UseItem()
        {
            if (mode != LinkMode.Moving && mode != LinkMode.Still) return;

            mode = LinkMode.Attack;
            IProjectile item;
            Vector2 spriteSize = linkSprite.Size;
            Vector2 offset = (spriteSize * currDirection) / 1.5f;
            Vector2 startingPosition = centerPosition + offset + (spriteSize / 2);
            switch (currItemIndex)
            {
                case 1:
                    item = (IProjectile)ItemSpriteFactory.Instance.CreateSwordItem(spriteBatch, startingPosition, currDirection);
                    break;

                case 2:
                    item = (IProjectile)ItemSpriteFactory.Instance.CreateArrowItem(spriteBatch, startingPosition, currDirection);
                    break;

                case 3:
                    item = (IProjectile)ItemSpriteFactory.Instance.CreateBoomerangItem(spriteBatch, startingPosition, currDirection);
                    break;

                case 4:
                    item = (IProjectile)ItemSpriteFactory.Instance.CreateBombItem(spriteBatch, startingPosition);
                    break;

                default:
                    item = null;
                    break;
            }
            if (item != null)
            {
                game.linkProjectiles.Add(item);
            }
            SetAttackAnimation();
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

        public void ChangeItem(int index)
        {
            currItemIndex = index;
        }

        public void TakeDamage(int damage)
        {
            if (remainingDamageDelay > 0 && linkSprite.Damaged) return;
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