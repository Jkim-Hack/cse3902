using System;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;

namespace cse3902.Entities
{
    public class GelStateMachine
    {
        private GelSprite gelSprite;

        public GelStateMachine(GelSprite gelSprite)
        {
            this.gelSprite = gelSprite;
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
            gelSprite.IsAttacking = true;
        }

        public void Die()
        {
            this.gelSprite.Erase();
        }

        public void BeShoved(Vector2 direction)
        {

        }
    }
}
