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
