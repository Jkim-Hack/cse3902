﻿using cse3902.Interfaces;

namespace cse3902.Commands
{
    public class LinkChangeWeaponCommand : ICommand
    {
        private Game1 game;

        public LinkChangeWeaponCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(int id)
        {
            id = id % 4;

            game.player.ChangeWeapon(id);
        }

        public void Unexecute()
        {

        }
    }
}