using cse3902.Interfaces;
using System.Collections.Generic;

namespace cse3902.Rooms.Conditions
{
    public class AllBlocksPushedOpenDoor : ICondition
    {
        private List<int> conditionVariables;

        public AllBlocksPushedOpenDoor(List<int> condVariables)
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
            bool isNotMoved = false;
            foreach (IBlock block in RoomBlocks.Instance.ListRef)
            {
                if (!block.IsMoved()) isNotMoved = true;
            }

            if (!isNotMoved)
            {
                List<IDoor> doors = RoomDoors.Instance.ListRef as List<IDoor>;
                foreach (int doornum in conditionVariables)
                {
                    doors[doornum].State = IDoor.DoorState.Open;
                }
            }
        }
    }
}
