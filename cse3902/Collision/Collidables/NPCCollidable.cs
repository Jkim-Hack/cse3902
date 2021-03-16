using System;
using cse3902.Interfaces;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class NPCCollidable : ICollidable
    {
        private IEntity npc;
        private List<Boolean> collisionOccurrences = new List<Boolean>(6);

        public NPCCollidable(IEntity npc)
        {
            this.npc = npc;
        }

        public void OnCollidedWith(ICollidable collidableObject)
        {
            //npc collisions don't do anything but npcs must implement ientity

        }

        public ref Rectangle RectangleRef
        {
            get => ref npc.Bounds;
        }

        public void ResetCollisions()
        {
            for (int i = 0; i < collisionOccurrences.Capacity; i++)
            {
                collisionOccurrences[i] = false;
            }
        }

        public int DamageValue
        {
            get => 0;
        }
    }
}