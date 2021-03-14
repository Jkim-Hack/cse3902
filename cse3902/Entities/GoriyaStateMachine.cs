using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities
{
    public class GoriyaStateMachine: IEntityStateMachine
    {
        private GoriyaSprite goriyaSprite;

        public GoriyaStateMachine(GoriyaSprite goriyaSprite)
        {
            this.goriyaSprite = goriyaSprite;
        }

        public void CycleWeapon(int dir)
        {
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            if (newDirection == new Vector2(0, 0))
            {
                //direction vector of (0,0) indicates just reverse the current direction
                if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.RightFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.LeftFacing;
                }
                else if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.LeftFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.RightFacing;
                }
                else if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.UpFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.DownFacing;
                }
                else if (goriyaSprite.StartingFrameIndex == (int)GoriyaSprite.FrameIndex.DownFacing)
                {
                    goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.UpFacing;
                }
            }

            if (newDirection.X > 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.RightFacing;
            }
            else if (newDirection.X < 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.LeftFacing;
            }
            else if (newDirection.Y > 0)
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.DownFacing;
            }
            else
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.UpFacing;
            }
        }

        public void TakeDamage()
        {

        }

        public void Attack()
        {

        }

        public void Die()
        {
            this.goriyaSprite.Erase();
        }
    }
}
