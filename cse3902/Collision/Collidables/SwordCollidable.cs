using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class SwordCollidable : ICollidable
    {
        private IProjectile projectile;
        private int damage;

        //todo: change to use some kind of sword class or interface instead of projectile
        public SwordCollidable(IProjectile projectile, int damage)
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