using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapRoomLayout
    {
        public const int offsetX = 100;
        public const int offsetY = 500;

        public static List<Rectangle> RoomLayout = new List<Rectangle>
        {
            new Rectangle(0, 0, 50, 50)
        };
    }
}
