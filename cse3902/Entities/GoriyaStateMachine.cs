using System;
using Microsoft.Xna.Framework;
using cse3902.Rooms;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities
{
    public class GoriyaStateMachine
    {
        private GoriyaSprite goriyaSprite;

        private Rectangle[] detectionBoxes;
        private Boolean isTriggered;
        private Vector2 direction;

        private Boolean alternate;

        public GoriyaStateMachine(GoriyaSprite goriyaSprite)
        {
            this.goriyaSprite = goriyaSprite;
            detectionBoxes = new Rectangle[4];
            isTriggered = false;
            ConstructDetectionBoxes();
            alternate = true;
        }

        public void ThrowBoomerang()
        {
            if (!IsTriggered && Bounds == goriyaSprite.Box)
            {
                IsTriggered = true;
            }
            
        }
        
        public Boolean IsTriggered
        {
            get => this.isTriggered;
            set => this.isTriggered = value;
        }

        public ref Rectangle Bounds
        {
            get
            {
                if (IsTriggered)
                {
                    return ref goriyaSprite.Box;
                }
                else
                {
                    if(alternate)
                    {
                        alternate = false;
                        return ref goriyaSprite.Box;
                    } else
                    {
                        alternate = true;
                        return ref DetectionBox;
                    }
                    
                }
            }
        }

        public ref Rectangle DetectionBox
        {
            get
            {
                if (direction.X > 0)
                {
                    return ref detectionBoxes[0];
                } else if (direction.X < 0)
                {
                    return ref detectionBoxes[1];
                } else if (direction.Y > 0)
                {
                    return ref detectionBoxes[2];
                } else
                {
                    return ref detectionBoxes[3];
                }
            }
        }

        public Vector2 Direction
        {
            set => direction = value;
        }

        private void ConstructDetectionBoxes()
        {
            //left box 
            detectionBoxes[0] = new Rectangle(goriyaSprite.Box.Center.X, goriyaSprite.Box.Center.Y, goriyaSprite.Box.Width, goriyaSprite.Box.Height);
            detectionBoxes[0].Inflate(RoomUtilities.BLOCK_SIDE * 2, 0);

            //right box
            detectionBoxes[1] = new Rectangle(goriyaSprite.Box.Center.X, goriyaSprite.Box.Center.Y, goriyaSprite.Box.Width, goriyaSprite.Box.Height);
            detectionBoxes[1].Inflate(RoomUtilities.BLOCK_SIDE * 2, 0);

            //top box
            detectionBoxes[2] = new Rectangle(goriyaSprite.Box.Center.X, goriyaSprite.Box.Center.Y, goriyaSprite.Box.Width, goriyaSprite.Box.Height);
            detectionBoxes[2].Inflate(0, RoomUtilities.BLOCK_SIDE * 2);

            //bottom box
            detectionBoxes[3] = new Rectangle(goriyaSprite.Box.Center.X, goriyaSprite.Box.Center.Y, goriyaSprite.Box.Width, goriyaSprite.Box.Height);
            detectionBoxes[3].Inflate(0, RoomUtilities.BLOCK_SIDE * 2);
        }
    }
}
