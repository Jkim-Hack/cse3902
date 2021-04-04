using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IPlayer: IEntity
    {
        public int TotalHealthCount { get; }

        public void UseItem();

        public void AddItem(IItem item);

        public void ChangeItem(int index);

        public void ChangeWeapon(int index);

        public void BeGrabbed(IEntity enemy, float speed);

        public void Reset();
    }
}
