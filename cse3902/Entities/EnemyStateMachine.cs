using System;
using Microsoft.Xna.Framework;

namespace cse3902.Entities {

    public class EnemyStateMachine : IEntityStateMachine
    {
        public void CycleWeapon(int dir)
        {
            //Enemies and npcs don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            //will implement after enemy spriting is done
        }
    }
}
