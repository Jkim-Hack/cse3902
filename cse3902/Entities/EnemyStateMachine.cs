﻿using System;
using Microsoft.Xna.Framework;

namespace cse3902.Entities {

    public class EnemyStateMachine : IEntityStateMachine
    {
        public void CycleWeapon(int dir)
        {
            //Enemies don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            //TODO: will implement after enemy spriting is done
        }

        public void BeShoved(Vector2 direction)
        {

        }
    }
}
