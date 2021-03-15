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
            // Keese can't change direction
        }

        public void TakeDamage()
        {

        }

        public void Attack()
        {

        }

        public void Die()
        {
            this.keeseSprite.Erase();
        }
    }
}
