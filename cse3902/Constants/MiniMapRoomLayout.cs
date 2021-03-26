using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapRoomLayout
    {
        public const int offsetX = 100;
        public const int offsetY = 600;

        public const int currentRoomSize = 8;

        /* "Coordinate system", with the starting room at (0, 0) */
        private static List<Vector2> RoomCoordinates = new List<Vector2>
        {
            new Vector2(0, 0),
            new Vector2(-1, 0),
            new Vector2(1, 0)
        };

        /* Calculates room sizes and positions */
        public static List<Rectangle> GetRoomLayout()
        {
            /* Real aspect ratio of blue minimap boxes is 7:3, with a gap of 2 */
            int width = 16, height = 8, gap = 3;

            List<Rectangle> RoomLayout = new List<Rectangle>();
            foreach (Vector2 room in RoomCoordinates) RoomLayout.Add(new Rectangle((int)room.X * (width + gap), (int)room.Y * (height + gap) * -1, width, height));

            return RoomLayout;
        }
    }
}
