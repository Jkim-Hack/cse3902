using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Sprites.EnemySprites;
using cse3902.Sprites;

namespace cse3902.Entities
{
    public class StalfosStateMachine
    {
        private StalfosSprite stalfosSprite;



        public StalfosStateMachine(StalfosSprite stalfosSprite)
        {
            this.stalfosSprite = stalfosSprite;

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
                stalfosSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.RightFacing;
            }
            else
            {
                stalfosSprite.StartingFrameIndex = (int)AquamentusSprite.FrameIndex.LeftFacing;
            }


        }

        //TODO: takedamage and attack should probably be IEntity methods

        public void TakeDamage()
        {

        }

        public void Attack()
        {
            stalfosSprite.IsAttacking = true;
        }
    }
}
