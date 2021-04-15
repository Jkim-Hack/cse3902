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
            id = (id % 4);
            
            if (GameStateManager.Instance.IsUnpaused() && cooldown <= 0)
            {
                if (id <= 1)
                {
                    id = id * 2 - 1;
                    (game.Player as Link).Health += id;
                }
                else
                {
                    id = id * 4 - 10;
                    (game.Player as Link).TotalHealthCount += id;
                }

                cooldown = 15;
            }
        }

        public void Unexecute()
        {
            cooldown = 0;
        }
    }
}