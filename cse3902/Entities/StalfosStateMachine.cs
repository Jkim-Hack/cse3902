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
           
        }

        public void TakeDamage()
        {

        }

        public void Attack()
        {
            stalfosSprite.IsAttacking = true;
        }

        public void Die()
        {
            this.stalfosSprite.Erase();
        }

        public void BeShoved(Vector2 direction)
        {

        }
    }
}
