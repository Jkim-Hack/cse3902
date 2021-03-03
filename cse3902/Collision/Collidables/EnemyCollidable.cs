using System;
using cse3902.Interfaces;
using cse3902.Entities.Enemies;
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
            if (collidableObject is SwordCollidable)
            {
                this.enemy.TakeDamage(collidableObject.DamageValue);
                this.enemy.BeShoved();

            } else if (collidableObject is ProjectileCollidable)
            {
                if (this.enemy is Gel || this.enemy is Keese)
                {
                    //only gels and keese take damage from projectiles it seems
                    this.enemy.TakeDamage(collidableObject.DamageValue);
                } else
                {
                    //other enemies are simply stunned in place for a bit
                    //need some kind of method to be able to 'stun' the enemies
                }
            } else if (collidableObject is BlockCollidable)
            {
                //todo: get the vector from the oncollidewith method and use the opposite direction vector
                Vector2 direction = new Vector2(0, 0);
                this.enemy.ChangeDirection(direction);
            } else
            {
                //no other collision matters for enemies
            }

            
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
