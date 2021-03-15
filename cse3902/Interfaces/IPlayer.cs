using System;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IPlayer: IEntity
    {
        public void UseItem();

        public void AddItem(IItem item);

        public void ChangeItem(int index);

        public void ChangeWeapon(int index);

        public Vector2 PreviousPosition { get; }

        public void Reset();

        public Vector2 CenterPosition { get; set; }
    }
}
