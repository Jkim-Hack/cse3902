using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class ResetCommand : ICommand
    {
        private Game1 game;
        private bool pressed;
	    
	    public ResetCommand(Game1 game)
        {
            this.game = game;
            pressed = false;
        }

        public void Execute(int id)
        {
            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning() && !pressed)
            {
                pressed = true;
                GameStateManager.Instance.Reset();
                game.RoomHandler.Reset(true);
                GameConditionManager.Instance.Reset();
            }
        }

        public void Unexecute()
        {
            pressed = false;
        }
    }
}
