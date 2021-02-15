using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities {

    public class AquamentusStateMachine : IEntityStateMachine
    {
        private AquamentusSprite aquamentusSprite;


        public AquamentusStateMachine(AquamentusSprite aquamentusSprite)
        {
            this.aquamentusSprite = this.aquamentusSprite;
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
                aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.RightFacing;
            } else
            {
                aquamentusSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.LeftFacing;
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
