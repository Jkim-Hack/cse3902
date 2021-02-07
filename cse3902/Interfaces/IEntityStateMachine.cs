using System;

namespace cse3902.Entities {

    public interface IEntityStateMachine {

        public void FaceUp();
        public void FaceDown();
        public void FaceLeft();
        public void FaceRight();
        public void CycleWeapon();
    }
}
