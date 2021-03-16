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
            //TODO: just a placeholder, will need to reflect how much damage link was dealt
            game.Player.TakeDamage(10);
        }

        public void Unexecute()
        {

        }
    }
}