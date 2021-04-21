using cse3902.Constants;
using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class LinkAttackItemCommand : ICommand
    {
        private Game1 game;

        public LinkAttackItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % CommandConstants.LinkAttackItemCommandCount;

            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning()) 
            {
                switch (id)
                {
                    case 0:
                        game.Player.Attack();
                        break;
                    case 1:
                        game.Player.UseItem();
                        break;
                    default: //this should never happen
                        game.Player.Attack();
                        break;
                }
            }
        }

        public void Unexecute()
        {

        }
    }
}