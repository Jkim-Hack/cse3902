﻿using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class BlockCollidable : ICollidable
    {
        private IBlock block;


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
                //figure out the direction of the player and pass the vector accordingly
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