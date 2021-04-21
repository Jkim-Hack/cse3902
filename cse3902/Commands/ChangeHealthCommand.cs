using cse3902.Interfaces;
using cse3902.Entities;
using cse3902.Constants;

namespace cse3902.Commands
{
    public class ChangeHealthCommand : ICommand
    {
        private Game1 game;
        private int cooldown;

        public ChangeHealthCommand(Game1 game)
        {
            this.game = game;
            cooldown = 0;
        }

        public void Execute(int id)
        {
            cooldown--;
            id = id % CommandConstants.ChangeHealthCommandCount;
            
            if (GameStateManager.Instance.IsUnpaused() && cooldown <= 0)
            {
                switch (id)
                {
                    case 0:
                        (game.Player as Link).Health -= SettingsValues.Instance.GetValue(SettingsValues.Variable.HealthDecrease);
                        break;
                    case 1:
                        (game.Player as Link).Health += SettingsValues.Instance.GetValue(SettingsValues.Variable.HealthIncrease);
                        break;
                    case 2:
                        (game.Player as Link).TotalHealthCount -= SettingsValues.Instance.GetValue(SettingsValues.Variable.MaxHealthDecrease);
                        break;
                    case 3:
                        (game.Player as Link).TotalHealthCount += SettingsValues.Instance.GetValue(SettingsValues.Variable.MaxHealthIncrease);
                        break;
                    default: //this should never happen
                        (game.Player as Link).Health -= SettingsValues.Instance.GetValue(SettingsValues.Variable.HealthDecrease);
                        break;
                }

                cooldown = CommandConstants.Cooldown;
            }
        }

        public void Unexecute()
        {
            cooldown = 0;
        }
    }
}