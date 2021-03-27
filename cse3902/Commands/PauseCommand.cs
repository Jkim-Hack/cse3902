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
                    switch (GameStateManager.Instance.PausedState)
                    {
                        case GameStateManager.PauseState.Unpaused:
                            GameStateManager.Instance.PausedState = GameStateManager.PauseState.MenuDisplayed;
                            game.Camera.ToggleHudDisplayed(60);
                            break;
                        case GameStateManager.PauseState.MenuDisplayed:
                            GameStateManager.Instance.PausedState = GameStateManager.PauseState.Unpaused;
                            game.Camera.ToggleHudDisplayed(60);
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (GameStateManager.Instance.PausedState)
                    {
                        case GameStateManager.PauseState.Unpaused:
                            GameStateManager.Instance.PausedState = GameStateManager.PauseState.Paused;
                            break;
                        case GameStateManager.PauseState.Paused:
                            GameStateManager.Instance.PausedState = GameStateManager.PauseState.Unpaused;
                            break;
                        case GameStateManager.PauseState.MenuDisplayed:
                            GameStateManager.Instance.PausedState = GameStateManager.PauseState.MenuPaused;
                            break;
                        case GameStateManager.PauseState.MenuPaused:
                            GameStateManager.Instance.PausedState = GameStateManager.PauseState.MenuDisplayed;
                            break;
                        default: //this should never happen
                            GameStateManager.Instance.PausedState = GameStateManager.PauseState.Paused;
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