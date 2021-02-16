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
            else
            {
                goriyaSprite.StartingFrameIndex = (int)GoriyaSprite.FrameIndex.LeftFacing;
            }
        }

        //TODO: takedamage and attack should probably be IEntity methods

        public void TakeDamage()
        {

        }

        public void Attack()
        {

        }
    }
}
