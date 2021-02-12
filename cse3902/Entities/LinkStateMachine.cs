using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites;

namespace cse3902.Entities {

    public class LinkStateMachine : IEntityStateMachine {

        private enum Weapons { WoodenSword, WhiteSword, MagicalSword, MagicalRod };
        private Weapons currentWeapon;
        private enum LinkDirection { Up, Down, Left, Right };
        private enum LinkMode { Still, Moving, Attack, Item };
        private LinkDirection direction;
        private LinkMode mode;
        private int health;

        private LinkSprite linkSprite;

        public LinkStateMachine(LinkSprite linkSprite) {

            currentWeapon = Weapons.WoodenSword;
            direction = LinkDirection.Right;
            mode = LinkMode.Still;
            this.linkSprite = linkSprite;
            health = 10;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            if (newDirection.X < 0)
            {
                this.FaceLeft();
            }
            else if (newDirection.X > 0)
            {
                this.FaceRight();
            }
            else if (newDirection.Y > 0)
            {
                this.FaceUp();
            }
            else if (newDirection.Y < 0)
            {
                this.FaceDown();
            }
        }

        private void FaceUp(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Up) return;
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;
            /* No need to update sprite if currently grabbing Item */
            if (mode == LinkMode.Item) return;

            direction = LinkDirection.Up;
            UpdateSprite();
        }

        private void FaceDown(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Down) return;
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;
            /* No need to update sprite if currently grabbing Item */
            if (mode == LinkMode.Item) return;

            direction = LinkDirection.Down;
            UpdateSprite();
        }

        private void FaceLeft(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Left) return;
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;
            /* No need to update sprite if currently grabbing Item */
            if (mode == LinkMode.Item) return;

            direction = LinkDirection.Left;
            UpdateSprite();
        }

        private void FaceRight(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Right) return;
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;
            /* No need to update sprite if currently grabbing Item */
            if (mode == LinkMode.Item) return;

            direction = LinkDirection.Right;
            UpdateSprite();
        }

        public void Attack()
        {

        }

        public void CycleWeapon(int dir){

            //TODO 
            //currentWeapon = (currentWeapon + dir + 3) % 3;
            //UpdateSprite();
        }

        public void UseItem()
        {

        }

        public void ChangeItem()
        {

        }

        public void TakeDamage(int damage)
        {

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
