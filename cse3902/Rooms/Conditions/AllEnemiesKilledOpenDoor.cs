using cse3902.Interfaces;
using System.Collections.Generic;

namespace cse3902.Rooms.Conditions
{
    public class AllEnemiesKilledOpenDoor : ICondition
    {
        private List<int> conditionVariables;

        public AllEnemiesKilledOpenDoor(List<int> condVariables)
        {
            /*
             * condVariables[i] = index of desired door in door list of desired room to be opened
             * 
             * There is no limit to the number of doors this can be done for in each room.
             */
            conditionVariables = condVariables;
        }

        public void CheckCondition()
        {
            IDoor.DoorState doorState = IDoor.DoorState.Closed;
            if (RoomEnemies.Instance.ListRef.Count == 0) doorState = IDoor.DoorState.Open;

            List<IDoor> doors = RoomDoors.Instance.ListRef as List<IDoor>;
            foreach (int doornum in conditionVariables)
            {
                doors[doornum].State = doorState;
            }
        }
    }
}
