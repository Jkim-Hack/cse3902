using System;

namespace cse3902.Entities {

    public class LinkStateMachine : IEntityStateMachine {

        private int[] weapons;
        private int currentWeapon;
        private enum LinkDirection { Up, Down, Left, Right };
        private LinkDirection direction;

        public LinkStateMachine() {

            currentWeapon = 0;
            weapons = new int[] {0, 0, 0};
            direction = LinkDirection.Right;
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

            //call sprite factory and give it direction, weapon
            throw new NotImplementedException();
        }
    }
}
