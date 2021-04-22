using cse3902.Interfaces;

namespace cse3902.Commands
{
    class HitboxCommand : ICommand
    {
        private Game1 game;
        private bool pressed;

        public HitboxCommand(Game1 game)
        {
            this.game = game;
            pressed = false;
        }

        public void Execute(int id)
        {
            if (pressed) return;
            pressed = true;
            GameStateManager.Instance.HitboxVisibility = !GameStateManager.Instance.HitboxVisibility;
        }

        public void Unexecute()
        {
            pressed = false;
        }
    }
}