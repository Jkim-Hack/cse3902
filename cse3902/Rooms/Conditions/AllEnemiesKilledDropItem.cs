using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.Rooms.Conditions
{
    public class AllEnemiesKilledDropItem : ICondition
    {
        private List<int> conditionVariables;

        public AllEnemiesKilledDropItem(List<int> condVariables)
        {
            /*
             * condVariables[0] = X-Coord of Key
             * condVariables[1] = Y-Coord of Key
             * condVariables[2] = number corresponding to an item
             */
            conditionVariables = condVariables;
            conditionVariables.Add(0);
        }

        public void CheckCondition()
        {
            if (conditionVariables[3] == 0 && RoomEnemies.Instance.ListRef.Count == 0)
            {
                conditionVariables[3] = 1;
                Vector2 startPos = new Vector2(conditionVariables[0], conditionVariables[1]);
                switch (conditionVariables[2])
                {
                    case 0:
                        ItemSpriteFactory.Instance.CreateKeyItem(startPos, true);
                        break;
                    case 1:
                        ItemSpriteFactory.Instance.CreateBoomerangItem(startPos, true);
                        break;
                    case 2:
                        ItemSpriteFactory.Instance.CreateHeartContainerItem(startPos, true);
                        break;
                    default: //this should never happen
                        break;
                }
            }
        }
    }
}
