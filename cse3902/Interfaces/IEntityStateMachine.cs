using System;

namespace cse3902.Entities {

    public interface IEntityStateMachine {

        public void Attack();
        public void ChangeDir();
        public void SwitchItem();
        public void TakeDamage();
        public Boolean IsDead();
        public void Update();
    }
}
