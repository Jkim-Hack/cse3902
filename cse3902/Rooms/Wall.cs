using cse3902.Collision;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;

namespace cse3902.Rooms
{
    class Wall : ICollidableItemEntity
    {
        private ICollidable wallCollidable;

        public ICollidable Collidable { get => wallCollidable; }

        public Wall(Rectangle hitbox)
        {
            wallCollidable = new WallCollidable(ref hitbox);
        }
    }

}
