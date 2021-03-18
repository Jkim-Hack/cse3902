using System;
using cse3902.Rooms;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class WallCollidable : ICollidable
    {
        private Rectangle hitbox;
        private List<Boolean> collisionOccurrences = new List<Boolean>(6);

        public bool DamageDisabled { get; set; }
        
	    public WallCollidable(ref Rectangle hitbox)
        {
            this.hitbox = hitbox;
            DamageDisabled = true;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //
        }

        public ref Rectangle RectangleRef
        {
            get => ref hitbox;
        }

        public int DamageValue
        {
            get => 0;
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
