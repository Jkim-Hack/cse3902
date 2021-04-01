using System;
using cse3902.Collision;
using cse3902.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Interfaces
{
    public interface IItem : ISprite, ICollidableItemEntity
    {
        public InventoryManager.ItemType ItemType { get; }
        public bool IsKept { get; }
    }
}
