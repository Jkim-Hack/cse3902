using cse3902.Interfaces;

namespace cse3902.Commands
{
    class PauseCommand : ICommand
    {
        private Game1 game;
        private int cooldown;

        public PauseCommand(Game1 game)
        {
            this.game = game;
            cooldown = 10;
        }

        public void Execute(int id)
        {
            if (cooldown < 10 || game.Camera.IsCameraMoving())
            {
                cooldown++;
                return;
            }
            cooldown = 0;

            switch (id) {
                case 0:
                    switch (game.PausedState)
                    {
                        case Game1.PauseState.Unpaused:
                            game.PausedState = Game1.PauseState.HudDisplayed;
                            game.Camera.ToggleHudDisplayed(60);
                            break;
                        case Game1.PauseState.HudDisplayed:
                            game.PausedState = Game1.PauseState.Unpaused;
                            game.Camera.ToggleHudDisplayed(60);
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (game.PausedState)
                    {
                        case Game1.PauseState.Unpaused:
                            game.PausedState = Game1.PauseState.Paused;
                            break;
                        case Game1.PauseState.Paused:
                            game.PausedState = Game1.PauseState.Unpaused;
                            break;
                        case Game1.PauseState.HudDisplayed:
                            game.PausedState = Game1.PauseState.HudPaused;
                            break;
                        case Game1.PauseState.HudPaused:
                            game.PausedState = Game1.PauseState.HudDisplayed;
                            break;
                        default: //this should never happen
                            game.PausedState = Game1.PauseState.Paused;
                            break;
                    }
                    break;
            }
        }

        public void Unexecute()
        {
            cooldown = 10;
        }
    }
}