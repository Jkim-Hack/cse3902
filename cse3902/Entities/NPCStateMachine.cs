using System;
using Microsoft.Xna.Framework;

namespace cse3902.Entities
{

    public class NPCStateMachine : IEntityStateMachine
    {
        public void CycleWeapon(int dir)
        {
            //Npcs don't change weapons
            throw new NotImplementedException();
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            //TODO: will implement after npc spriting is done
        }
    }
}
