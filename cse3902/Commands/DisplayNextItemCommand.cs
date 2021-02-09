using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Items;

namespace cse3902.Commands
{
    public class DisplayNextItemCommand: ICommand
    {
        private Game1 game;

        private ItemHandler itemHandler;

        private int currentPos;

        public DisplayNextItemCommand(Game1 game, int position)
        {
            this.game = game;
            itemHandler = new ItemHandler();
            currentPos = position;
        }

        public void Execute()
        {
            currentPos++;
            itemHandler.itemIndex = currentPos;
        }
    }
}
