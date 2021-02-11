using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
            //game.Link.hurt();
        }

        public void Unexecute()
        {

        }
    }
}