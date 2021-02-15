using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;
using cse3902.Sprites.EnemySprites;
using cse3902.Sprites;

namespace cse3902.Entities {

    public class AquamentusStateMachine : IEntityStateMachine
    {
        private AquamentusSprite aquamentusSprite;
        private FireballSprite fireball1;
        private FireballSprite fireball2;
        private FireballSprite fireball3;


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
