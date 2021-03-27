using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class LinkChangeWeaponCommand : ICommand
    {
        private Game1 game;

        public LinkChangeWeaponCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 4;

            if (GameStateManager.Instance.PausedState == GameStateManager.PauseState.Unpaused) game.Player.ChangeWeapon(id);
        }

        public void Unexecute()
        {

        }
    }
}