using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class EnemyCollidable : ICollidable
    {
        private IEntity enemy;
        private int damage;

        public EnemyCollidable(IEntity enemy, int damage)
        {
            this.enemy = enemy;
            this.damage = damage;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //only collision cases that matter for enemies:
            //walls and blocks
            //damaging weapons (sword, arrow, etc.)
            //in all other cases, do nothing

            
        }

        public ref Rectangle RectangleRef
        {
            get => ref enemy.Bounds;
        }

        public int DamageValue
        {
            get => damage;
        }
    }
}
