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
            linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.DownFacing;
        }

        private void FaceLeft(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Left) return;
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;
            /* No need to update sprite if currently grabbing Item */
            if (mode == LinkMode.Item) return;
            linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.LeftFacing;
        }

        private void FaceRight(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Right) return;
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;
            /* No need to update sprite if currently grabbing Item */
            if (mode == LinkMode.Item) return;
            linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.RightFacing;
        }

        public void Attack()
        {

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

        public void UseItem()
        {

        }

        public void ChangeItem()
        {

        }

        public void TakeDamage(int damage)
        {

        }

        
    }
}
