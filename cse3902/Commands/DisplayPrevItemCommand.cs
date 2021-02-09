using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Items;

namespace cse3902.Commands
{
    public class DisplayPrevItemCommand : ICommand
    {
        private Game1 game;

        private ItemHandler itemHandler;

        private int currentPos;

        public DisplayPrevItemCommand(Game1 game, int position)
        {
            this.game = game;
            itemHandler = new ItemHandler();
            currentPos = position;
        }

        public void Execute()
        {
            currentPos--;
            itemHandler.itemIndex = currentPos;
        }
    }
}
