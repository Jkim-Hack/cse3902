using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Sounds;
using Microsoft.Xna.Framework.Audio;

namespace cse3902.Rooms.Conditions
{
    public class AllEnemiesKilledOpenDoor : ICondition
    {
        private List<int> conditionVariables;
        private IDoor.DoorState prevState;

        public AllEnemiesKilledOpenDoor(List<int> condVariables)
        {
            /*
             * condVariables[i] = index of desired door in door list of desired room to be opened
             * conditionVariables[conditionVariables.Count - 1] = initial state of specified doors
             * 
             * There is no limit to the number of doors this can be done for in each room.
             */
            conditionVariables = condVariables;
            switch (conditionVariables[conditionVariables.Count - 1])
            {
                case 0:
                    prevState = IDoor.DoorState.Closed;
                    break;
                case 1:
                    prevState = IDoor.DoorState.Open;
                    break;
                default: //this should never happen
                    break;
            }
            conditionVariables.RemoveAt(conditionVariables.Count - 1);
        }

        public void CheckCondition()
        {
            IDoor.DoorState doorState = IDoor.DoorState.Closed;
            if (RoomEnemies.Instance.ListRef.Count == 0)
            {
                doorState = IDoor.DoorState.Open;
            }

            List<IDoor> doors = RoomDoors.Instance.ListRef as List<IDoor>;
            foreach (int doornum in conditionVariables)
            {
                doors[doornum].State = doorState;
            }

            if (doorState != prevState) SoundFactory.PlaySound(SoundFactory.Instance.doorUnlock);
            prevState = doorState;
        }

        public void SendSignal()
        {

        }

        public void Reset()
        {

        }
    }
}
