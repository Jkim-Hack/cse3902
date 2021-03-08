using System;
using cse3902.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Interfaces
{
    public interface IItem : ISprite
    {
        public ICollidable Collidable { get; }
    }
}
