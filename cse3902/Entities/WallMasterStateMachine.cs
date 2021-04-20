using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities
{
    public class WallMasterStateMachine
    {
        private WallMasterSprite wallMasterSprite;

        public WallMasterStateMachine(WallMasterSprite wallMasterSprite)
        {
            this.wallMasterSprite = wallMasterSprite;
        }


        public void ChangeDirection(Vector2 newDirection)
        {
            if (newDirection.X > 0)
            {
                if (newDirection.Y > 0)
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.RightDownFacing;
                } else
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.RightUpFacing;
                }
                
            }
            else
            {
                if (newDirection.Y > 0)
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.LeftDownFacing;
                }
                else
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.LeftUpFacing;
                }
            }
        }
        public void Attack()
        {
            wallMasterSprite.IsAttacking = true;
        }

        public void Die()
        {
        }
    }
}
