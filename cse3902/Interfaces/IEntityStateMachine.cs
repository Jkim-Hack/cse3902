using System;
using Microsoft.Xna.Framework;

namespace cse3902.Entities {

    public interface IEntityStateMachine {

        public void ChangeDirection(Vector2 direction);
        public void CycleWeapon(int dir);
    }
}
