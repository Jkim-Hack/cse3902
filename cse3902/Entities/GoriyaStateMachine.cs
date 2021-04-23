using System;
using Microsoft.Xna.Framework;
using cse3902.Rooms;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities
{
    public class GoriyaStateMachine
    {
        private GoriyaSprite goriyaSprite;

        private Rectangle detectionBox;
        private Boolean isTriggered;
        private Vector2 direction;

        private int alternate;

        public GoriyaStateMachine(GoriyaSprite goriyaSprite)
        {
            this.goriyaSprite = goriyaSprite;
            isTriggered = false;
            alternate = 0;
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
                    if(alternate == 0)
                    {
                        return ref goriyaSprite.Box;
                    } else
                    {
                        return ref DetectionBox;
                    }
                    
                }
            }
        }

        public void Update()
        {
            if (alternate == 0) alternate = 1;
            else alternate = 0;
        }

        public ref Rectangle DetectionBox
        {
            get
            {
                if (direction.X > 0)
                {
                    detectionBox = goriyaSprite.Box;
                    detectionBox.Inflate(RoomUtilities.BLOCK_SIDE * 2, 0);
                    detectionBox.Offset(RoomUtilities.BLOCK_SIDE * 2, 0);
                    return ref detectionBox;
                } else if (direction.X < 0)
                {
                    detectionBox = goriyaSprite.Box;
                    detectionBox.Inflate(RoomUtilities.BLOCK_SIDE * 2, 0);
                    detectionBox.Offset(-RoomUtilities.BLOCK_SIDE * 2, 0);
                    return ref detectionBox;
                } else if (direction.Y > 0)
                {
                    detectionBox = goriyaSprite.Box;
                    detectionBox.Inflate(0, RoomUtilities.BLOCK_SIDE * 2);
                    detectionBox.Offset(0, RoomUtilities.BLOCK_SIDE * 2);
                    return ref detectionBox;
                } else
                {
                    detectionBox = goriyaSprite.Box;
                    detectionBox.Inflate(0, RoomUtilities.BLOCK_SIDE * 2);
                    detectionBox.Offset(0, -RoomUtilities.BLOCK_SIDE * 2);
                    return ref detectionBox;
                }
            }
        }

        public Vector2 Direction
        {
            set => direction = value;
        }

    }
}
