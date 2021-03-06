﻿using System;
using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Blocks;
using Microsoft.Xna.Framework;

namespace cse3902.Collision.Collidables
{
    public class BlockCollidable : ICollidable
    {
        private IBlock block;
        private List<Boolean> collisionOccurrences = new List<Boolean>(6);

        public bool DamageDisabled { get; set; }

        public BlockCollidable(IBlock block)
        {
            this.block = block;
            DamageDisabled = true;
        }


        public void OnCollidedWith(ICollidable collidableObject)
        {
            //only collision cases that matter for blocks:
            //link
            //only need to move block if it is a movable block

            if (collidableObject is PlayerCollidable)
            {
                block.Interact(((PlayerCollidable)collidableObject).Direction);
                
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

        public void ResetCollisions()
        {
            for (int i = 0; i < collisionOccurrences.Capacity; i++)
            {
                collisionOccurrences[i] = false;
            }
        }

        public Boolean IsWalkable
        {
            get => this.block is WalkableBlock;
        }


    }
}