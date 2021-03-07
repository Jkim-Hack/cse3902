using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class BlockCollidable : ICollidable
    {
        private IBlock block;

        //TODO: make some kind of block interface (uses Ientity interface rn)
        public BlockCollidable(IBlock block)
        {
            this.block = block;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //only collision cases that matter for blocks:
            //link
            //only need to move block if it is a movable block

            if (collidableObject is PlayerCollidable)
            {
                //if it is a movable block, move it
                //if we are considering stairs a block, collision with stairs will need to move camera
            } else
            {
                //do nothing
            }


        }

        public ref Rectangle RectangleRef
        {
            get => ref block.Bounds;
        }

        public int DamageValue
        {
            get => 0;
        }
    }
}