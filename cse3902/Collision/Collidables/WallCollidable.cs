using System;
using cse3902.Rooms;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class WallCollidable : ICollidable
    {
        private Rectangle hitbox;

        public WallCollidable(ref Rectangle hitbox)
        {
            this.hitbox = hitbox;
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
    }
}
