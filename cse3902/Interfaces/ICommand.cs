﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace cse3902.Interfaces
{
    // Command
    public interface ICommand
    {
        public void Execute(int id);
        public void Unexecute();
    }
}
