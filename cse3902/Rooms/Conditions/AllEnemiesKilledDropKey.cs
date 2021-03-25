using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.Rooms.Conditions
{
    public class AllEnemiesKilledDropKey : ICondition
    {
        private List<int> conditionVariables;

        public AllEnemiesKilledDropKey(List<int> condVariables)
        {
            /*
             * condVariables[0] = X-Coord of Key
             * condVariables[1] = Y-Coord of Key
             */
            conditionVariables = condVariables;
            conditionVariables.Add(0);
        }

        public void CheckCondition()
        {
            if (conditionVariables[2] == 0 && RoomEnemies.Instance.ListRef.Count == 0)
            {
                conditionVariables[2] = 1;
                Vector2 startPos = new Vector2(conditionVariables[0], conditionVariables[1]);
                ItemSpriteFactory.Instance.CreateKeyItem(startPos);
            }
        }
    }
}
