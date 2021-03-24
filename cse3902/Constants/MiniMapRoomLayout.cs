using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapRoomLayout
    {

        public static ReadOnlyCollection<Rectangle> RoomLayout = new ReadOnlyCollection<Rectangle>(new List<Rectangle>
        {
            new Rectangle(0, 0, 10, 10)
        });
    }
}
