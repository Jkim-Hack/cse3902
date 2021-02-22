using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class CommandList
    {
        private Dictionary<Keys[], ICommand> keyboardCommandMap;

        private Game1 game;
        
        public CommandList(Game1 game)
        {
            this.game = game;
            BuildCommands();
        }

        private void BuildCommands()
        {
            keyboardCommandMap = new Dictionary<Keys[], ICommand>()
            {
                {new Keys[] {Keys.Q}, new QuitCommand(game)},
                {new Keys[] {Keys.R}, new ResetCommand(game)},
                {new Keys[] {Keys.E}, new DamageLinkCommand(game)},
                {new Keys[] {Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.W, Keys.A, Keys.S, Keys.D}, new LinkMovementCommand(game) },
                {new Keys[] {Keys.D1, Keys.D2, Keys.D3, Keys.D4}, new LinkUseItemCommand(game) },
                {new Keys[] {Keys.D5, Keys.D6, Keys.D7, Keys.D8}, new LinkChangeWeaponCommand(game) },
                {new Keys[] {Keys.Z, Keys.N}, new LinkSwordAttackCommand(game) },
                {new Keys[] {Keys.T, Keys.Y}, new CycleShownBlockCommand(game) },
                {new Keys[] {Keys.U, Keys.I}, new CycleShownItemCommand(game) },
                {new Keys[] {Keys.O, Keys.P}, new CycleShownEnemyNPCCommand(game) }
            };
        }

        public Dictionary<Keys[], ICommand> KeyboardCommands
        {
            get => keyboardCommandMap;
        }
    }
}
