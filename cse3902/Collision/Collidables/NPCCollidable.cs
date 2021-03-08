using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class NPCCollidable : ICollidable
    {
        private IEntity npc;

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

        public int DamageValue
        {
            get => 0;
        }
    }
}