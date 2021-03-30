using System;
using Microsoft.Xna.Framework;
using cse3902.Collision;

namespace cse3902.Interfaces
{
    public interface ITrap: ICollidableItemEntity
    {
        public ref Rectangle Bounds { get; }
        public Vector2 Center { get; set; }
        public Vector2 PreviousCenter { get; }
        public int Damage { get; }
        public Boolean IsTriggered { get; set; }
        public Vector2 Direction { get; set; }

        public void Update(GameTime gameTime);
        public void Draw();
    }
}
