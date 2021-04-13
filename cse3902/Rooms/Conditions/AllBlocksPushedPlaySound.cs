using cse3902.Interfaces;
using cse3902.Sounds;

namespace cse3902.Rooms.Conditions
{
    public class AllBlocksPushedPlaySound : ICondition
    {
        bool alreadyPlayed;

        public AllBlocksPushedPlaySound()
        {
            alreadyPlayed = false;
        }

        public void CheckCondition()
        {
            bool allMoved = true;
            foreach (IBlock block in RoomBlocks.Instance.ListRef)
            {
                if (!block.IsMoved())
                {
                    allMoved = false;
                    break;
                }
            }

            if (allMoved && !alreadyPlayed)
            {
                alreadyPlayed = true;
                SoundFactory.PlaySound(SoundFactory.Instance.secret);
            }
            else if (!allMoved) alreadyPlayed = false;
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
