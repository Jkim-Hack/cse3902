using cse3902.Interfaces;

namespace cse3902.Commands
{
    class PauseCommand : ICommand
    {
        private Game1 game;

        public PauseCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 2;
            switch (id) {
                case 0:
                    GameStateManager.Instance.ToggleMenuDisplayed();
                    break;
                case 1:
                    GameStateManager.Instance.TogglePaused();
                    break;
                default: //this should never happen;
                    break;
            }
        }

        public void Unexecute()
        {
        }
    }
}