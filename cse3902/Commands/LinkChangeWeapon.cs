using cse3902.Interfaces;
using cse3902.Constants;

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
            id = id % CommandConstants.LinkChangeWeaponCount;

            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning()) game.Player.ChangeWeapon(id);
        }

        public void Unexecute()
        {

        }
    }
}