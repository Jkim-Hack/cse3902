using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class LinkSwordAttackCommand : ICommand
    {
        private Game1 game;

        public LinkSwordAttackCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            game.Player.Attack();
        }

        public void Unexecute()
        {

        }
    }
}