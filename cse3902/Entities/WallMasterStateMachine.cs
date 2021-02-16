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

        public void CycleWeapon(int dir)
        {
            //Enemies don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            if (newDirection.X > 0)
            {
                if (newDirection.Y > 0)
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.RightUpFacing;
                } else
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.RightDownFacing;
                }
                
            }
            else
            {
                if (newDirection.Y > 0)
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.LeftUpFacing;
                }
                else
                {
                    wallMasterSprite.StartingFrameIndex = (int)WallMasterSprite.FrameIndex.LeftDownFacing;
                }
            }


        }


        public void TakeDamage()
        {

        }

        public void Attack()
        {
            wallMasterSprite.IsAttacking = true;
        }
    }
}
