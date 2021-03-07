using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class ItemCollidable : ICollidable
    {
        private IItem item;


        public ItemCollidable(IItem item)
        {
            this.item = item;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {

            if (collidableObject is PlayerCollidable)
            {
                //player needs to pick up item
            }
            else
            {
                //do nothing
            }


        }

        public ref Rectangle RectangleRef
        {
            get => ref item.Box;
        }

        public int DamageValue
        {
            get => 0;
        }
    }
}
