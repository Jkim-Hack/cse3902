﻿using System;

namespace cse3902.Entities {

    public class EnemyStateMachine : IEntityStateMachine {

        public EnemyStateMachine() { }

        public void Attack() { }

        public void ChangeDir() { }

        public void SwitchItem() { }

        public void TakeDamage() { }

        public Boolean IsDead() { return false; }

        public void Update() { }
    }
}
