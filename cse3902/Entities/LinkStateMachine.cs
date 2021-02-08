﻿using System;
using cse3902.Interfaces;
using cse3902.Sprites;

namespace cse3902.Entities {

    public class LinkStateMachine : IEntityStateMachine {

        private int[] weapons;
        private int currentWeapon;
        private enum LinkDirection { Up, Down, Left, Right };
        private LinkDirection direction;

        private LinkSprite linkSprite;

        public LinkStateMachine(LinkSprite linkSprite) {

            currentWeapon = 0;
            weapons = new int[] {0, 0, 0};
            direction = LinkDirection.Right;
            this.linkSprite = linkSprite;
        }

        public void FaceUp(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Up) return;
            
            direction = LinkDirection.Up;
            UpdateSprite();
        }

        public void FaceDown(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Down) return;
            
            direction = LinkDirection.Down;
            UpdateSprite();
        }

        public void FaceLeft(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Left) return;
            
            direction = LinkDirection.Left;
            UpdateSprite();
        }

        public void FaceRight(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Right) return;
            
            direction = LinkDirection.Right;
            UpdateSprite();
        }

        public void CycleWeapon(int dir){

            currentWeapon = (currentWeapon + dir + 3) % 3;
            UpdateSprite();
        }

        private void UpdateSprite() {
            
	        switch (direction)
            {
                case LinkDirection.Left:
                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.LeftFacing;
                    break;
                case LinkDirection.Right:
                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.RightFacing;
                    break;
                case LinkDirection.Up:
                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.UpFacing;
                    break;
                case LinkDirection.Down:
                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.DownFacing;
                    break;
            }
        }
    }
}
