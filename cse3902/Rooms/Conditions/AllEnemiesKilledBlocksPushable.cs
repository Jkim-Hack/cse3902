using cse3902.Interfaces;

namespace cse3902.Rooms.Conditions
{
    public class AllEnemiesKilledBlocksPushable : ICondition
    {
        public AllEnemiesKilledBlocksPushable()
        {
        }

        public void CheckCondition()
        {
            if (RoomEnemies.Instance.ListRef.Count != 0) RoomBlocks.Instance.Reset();
        }

        public void SendSignal()
        {

        }

        public void Reset()
        {

        }

        public void EnterRoom()
        {

        }

        public void LeaveRoom()
        {

        }
    }
}
