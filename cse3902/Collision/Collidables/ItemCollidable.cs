using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace cse3902.Collision.Collidables
{
    public class ItemCollidable : ICollidable
    {
        private IItem item;
        private List<Boolean> collisionOccurrences = new List<Boolean>(6);

        public ItemCollidable(IItem item)
        {
            this.item = item;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {

            //interaction with player handled in playercollidable
            //doesn't interact with anything else
        }

        public ref Rectangle RectangleRef
        {
            get => ref item.Box;
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

        //necesssary for link to be able to pick item up
        public IItem Item
        {
            get => this.item;
        }
    }
}
