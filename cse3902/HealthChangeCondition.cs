using cse3902.Interfaces;
using cse3902.Entities;

namespace cse3902.Rooms.Conditions
{
    public class HealthChangeCondition : ICondition
    {
        int timeCounter;
        Game1 game;
        public HealthChangeCondition(Game1 game)
        {
            timeCounter = 0;
            this.game = game;
        }

        public void CheckCondition()
        {
            timeCounter++;
            if (timeCounter > SettingsValues.Instance.GetValue(SettingsValues.Variable.HealthChangeDelay))
            {
                timeCounter = 0;
                (game.Player as Link).Health += SettingsValues.Instance.GetValue(SettingsValues.Variable.HealthChange);
            }
        }

        public void SendSignal()
        {

        }

        public void Reset()
        {
            timeCounter = 0;
        }

        public void EnterRoom()
        {
        }

        public void LeaveRoom()
        {
        }
    }
}
