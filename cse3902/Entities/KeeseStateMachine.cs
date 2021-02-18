using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities
{
    public class KeeseStateMachine: IEntityStateMachine
    {
        private KeeseSprite keeseSprite;


        public KeeseStateMachine(KeeseSprite keeseSprite)
        {
            this.keeseSprite = keeseSprite;
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
                keeseSprite.StartingFrameIndex = (int)KeeseSprite.FrameIndex.RightFacing;
            }
            else
            {
                keeseSprite.StartingFrameIndex = (int)KeeseSprite.FrameIndex.LeftFacing;
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
