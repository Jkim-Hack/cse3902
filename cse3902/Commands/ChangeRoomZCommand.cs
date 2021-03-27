using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Commands
{
    public class ChangeRoomZCommand : ICommand
    {
        private Game1 game;

        public ChangeRoomZCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 2;
            Vector3 direction;
            int ans;
            switch (id)
            {
                case 0:
                    direction = new Vector3(0, 0, 1);
                    ans = 4;
                    break;
                case 1:
                    direction = new Vector3(0, 0, -1);
                    ans = 0;
                    break;
                default: //this should never happen
                    direction = new Vector3(0, 0, 1);
                    ans = 4;
                    break;
            }

            if (GameStateManager.Instance.PausedState == GameStateManager.PauseState.Unpaused) game.RoomHandler.LoadNewRoom(direction,ans);
        }

        public void Unexecute()
        {

        }
    }
}