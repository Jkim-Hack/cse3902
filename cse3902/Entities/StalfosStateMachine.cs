using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;


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
            //stalfos dont seem to change directions


        }


        public void TakeDamage()
        {

        }

        public void Attack()
        {
            stalfosSprite.IsAttacking = true;
        }
    }
}
