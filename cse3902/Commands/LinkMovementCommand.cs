using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Commands
{
    public class LinkMovementCommand : ICommand
    {
        private Game1 game;
        private int cooldown;

        public LinkMovementCommand(Game1 game)
        {
            this.game = game;
            cooldown = 0;
        }

        public void Execute(int id)
        {
            cooldown--;
            id = id % 4;
            Vector2 direction;
            switch (id)
            {
                case 0:
                    direction = new Vector2(0, -1);
                    break;
                case 1:
                    direction = new Vector2(-1, 0);
                    break;
                case 2:
                    direction = new Vector2(0, 1);
                    break;
                case 3:
                    direction = new Vector2(1, 0);
                    break;
                default: //this should never happen
                    direction = new Vector2(0, 1);
                    break;
            }

            if (GameStateManager.Instance.IsUnpaused() && !game.RoomHandler.roomTransitionManager.IsTransitioning()) game.Player.ChangeDirection(direction);
            else if (GameStateManager.Instance.InMenu(false) && cooldown <= 0)
            {
                game.HudManager.HudInventory.MoveCursor(direction);
                cooldown = 15;
            }
        }

        public void Unexecute()
        {
            if (!GameStateManager.Instance.IsDying())
            {
                game.Player.ChangeDirection(new Vector2(0, 0));
            }
            cooldown = 0;
        }
    }
}