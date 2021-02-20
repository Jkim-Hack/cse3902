using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class DamageLinkCommand : ICommand
    {
        private Game1 game;

        public DamageLinkCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            game.player.TakeDamage();
        }

        public void Unexecute()
        {

        }
    }
}