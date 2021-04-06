using cse3902.Interfaces;
using cse3902.Entities;

namespace cse3902.Commands
{
    public class ChangeHealthCommand : ICommand
    {
        private Game1 game;
        private int cooldown;

        public ChangeHealthCommand(Game1 game)
        {
            this.game = game;
            cooldown = 0;
        }

        public void Execute(int id)
        {
            cooldown--;
            id = (id % 2) * 2 - 1;
            
            if (GameStateManager.Instance.IsUnpaused() && cooldown <= 0)
            {
                (game.Player as Link).Health += id;
                cooldown = 15;
            }
        }

        public void Unexecute()
        {
            cooldown = 0;
        }
    }
}