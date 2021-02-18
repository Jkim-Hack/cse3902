using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Items;

namespace cse3902.Entities {

    public class LinkStateMachine : IEntityStateMachine {

        
        private LinkMode mode;

        private LinkSprite linkSprite;
        private SpriteBatch spriteBatch;
        private Vector2 centerPosition;
        private Vector2 currDirection;
        

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
            {
                }
                {
                }
                {
                }
                {
                }
            }
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

        }


               
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
            get => this.centerPosition;
            set
            {
                this.linkSprite.Center = value;
                this.centerPosition = value;
            }
        }
    }
}
