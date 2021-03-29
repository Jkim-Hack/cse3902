using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class LinkUseItemCommand : ICommand
    {
        private Game1 game;

        public LinkUseItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 4;
            id++;

            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning()) 
            { 
                game.Player.ChangeItem(id);
                game.Player.UseItem();
            }
        }

        public void Unexecute()
        {

        }
    }
}