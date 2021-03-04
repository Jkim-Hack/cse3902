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
            enemy.TakeDamage(collidableObject.DamageValue);

            //if collidableObject is enemy, don't take damage, don't move
            //if collidableObject is player, don't take damage don't move
            //ii collidableObject is weapon, take damage and move
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
