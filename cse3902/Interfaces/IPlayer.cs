using System;
using Microsoft.Xna.Framework;

namespace cse3902.Interfaces
{
    public interface IPlayer: IEntity
    {
        public void UseItem();

        public void ChangeItem(int index);

        public void ChangeWeapon(int index);

        public void Reset();

        public Vector2 CenterPosition { get; set; }
    }
}
