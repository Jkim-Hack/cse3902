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
        private List<Boolean> collisionOccurrences = new List<Boolean>(6);

        public bool DamageDisabled { get; set; }
        
	    public SwordCollidable(IProjectile projectile)
        {
            this.sword = projectile;
            DamageDisabled = true;
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
            for (int i = 0; i < collisionOccurrences.Capacity; i++)
            {
                collisionOccurrences[i] = false;
            }
        }
    }

}