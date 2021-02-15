using System;
using Microsoft.Xna.Framework;
using cse3902.Interfaces;

namespace cse3902.Entities {

    public class AquamentusStateMachine : IEntityStateMachine
    {
        private ISprite enemySprite;

        public AquamentusStateMachine(ISprite enemySprite)
        {
            this.enemySprite = enemySprite;
        }

        public void CycleWeapon(int dir)
        {
            //Enemies don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            //enemySprite.
        }
    }
}
