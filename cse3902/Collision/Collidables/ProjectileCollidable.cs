using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class ProjectileCollidable : ICollidable
    {
        private IProjectile projectile;
        private int damage;

        public ProjectileCollidable(IProjectile projectile, int damage)
        {
            this.projectile = projectile;
            this.damage = damage;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //only collision cases that matter for projectiles:
            //walls and blocks (destroyed)
            //enemies (destroyed)
            //link (fireballs) (destroyed)


        }

        public ref Rectangle RectangleRef
        {
            //todo: iprojectile needs a ref rectangle bounds member
            get => ref projectile.Bounds;
        }

        public int DamageValue
        {
            get => damage;
        }
    }

}