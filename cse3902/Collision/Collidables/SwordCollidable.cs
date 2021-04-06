using System;
using cse3902.Interfaces;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class SwordCollidable : ICollidable
    {
        private IProjectile sword;
        private int damage;
        private bool collisionOccurrence;

        public bool DamageDisabled { get; set; }
        
	    public SwordCollidable(IProjectile projectile)
        {
            this.sword = projectile;
            DamageDisabled = true;
            collisionOccurrence = false;
        }

        public void OnCollidedWith(ICollidable collidableObject)
        {
            //nothing actually happend TO swords upon collision
            //so do nothing here
        }

        public ref Rectangle RectangleRef
        {
            get => ref sword.Box;
        }
             
        public int DamageValue
        {
            get => this.sword.Damage;
        }

        public Vector2 Direction
        {
            get => this.sword.Direction;
        }

        public void ResetCollisions()
        {
                collisionOccurrence = false;
            
        }
    }

}