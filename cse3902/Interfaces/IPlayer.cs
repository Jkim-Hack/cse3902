using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IPlayer: IEntity
    {
        public void UseItem();

        public void AddItem(IItem item);

        public void ChangeItem(int index);

        public void ChangeWeapon(int index);

        public void Reset();

        public List<Vector2> Directions { get; set; }
    }
}
