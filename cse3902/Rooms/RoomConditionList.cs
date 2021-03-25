using System.Collections.Generic;
using cse3902.Interfaces;

namespace cse3902.Rooms
{
    public class RoomConditionList
    {
        public delegate void CheckingCondition();
        private RoomHandler roomHandler;
        private List<CheckingCondition> roomConditions;

        public RoomConditionList(RoomHandler handler)
        {
            roomHandler = handler;
            roomConditions = new List<CheckingCondition>();

            roomConditions.Add(AllEnemiesKilledOpenRightDoor);
            roomConditions.Add(AllBlocksPushedOpenLeftDoor);
        }

        public CheckingCondition GetRoomCondition(int i)
        {
            return roomConditions[i];
        }

        private void AllEnemiesKilledOpenRightDoor()
        {
            if (RoomEnemies.Instance.ListRef.Count == 0)
            {
                roomHandler.rooms.GetValueOrDefault(roomHandler.currentRoom).Doors[3].State = IDoor.DoorState.Open;
            }
            else
            {
                roomHandler.rooms.GetValueOrDefault(roomHandler.currentRoom).Doors[3].State = IDoor.DoorState.Closed;
            }
        }

        private void AllBlocksPushedOpenLeftDoor()
        {
            bool isNotMoved = false;
            foreach (IBlock block in RoomBlocks.Instance.ListRef)
            {
                if (!block.IsMoved()) isNotMoved = true;
            }

            if (isNotMoved)
            {
                roomHandler.rooms.GetValueOrDefault(roomHandler.currentRoom).Doors[1].State = IDoor.DoorState.Closed;
            }
            else
            {
                roomHandler.rooms.GetValueOrDefault(roomHandler.currentRoom).Doors[1].State = IDoor.DoorState.Open;
            }
        }
    }
}
