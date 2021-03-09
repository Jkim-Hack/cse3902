using cse3902.Collision;
using Microsoft.Xna.Framework;

namespace cse3902.Rooms
{
    class Wall: ICollidable
    {
        Rectangle rectangle;
        public ref Rectangle RectangleRef => ref rectangle;

        public int DamageValue => throw new System.NotImplementedException();

        public Wall(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        public void OnCollidedWith(ICollidable collidableObject)
        {
            throw new System.NotImplementedException();
        }
    }

}
