using System;

namespace cse3902.Entities {

    public interface IEntityStateMachine {

        void Attack();
        void ChangeDir();
        void SwitchItem();
        void TakeDamage();
        Boolean IsDead();
        void Update();
    }
}
