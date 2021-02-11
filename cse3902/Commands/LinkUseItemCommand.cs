using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Commands
{
    public class LinkUseItemCommand : ICommand
    {
        private Game1 game;

        public LinkUseItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 9;
            id++;

            //game.Link.useItem(id);
        }

        public void Unexecute(Keys[] keyset)
        {

        }
    }
}