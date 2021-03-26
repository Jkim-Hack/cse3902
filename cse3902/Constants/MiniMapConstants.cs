using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapConstants
    {
        public const int greenSize = 8;

        /* "Coordinate system", with the starting room at (0, 0) */
        private static List<Vector2> RoomCoordinates = new List<Vector2>
        {
            new Vector2(0, 0),
            new Vector2(-1, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 2),
            new Vector2(-1, 2),
            new Vector2(1, 2),
            new Vector2(0, 3),
            new Vector2(-1, 3),
            new Vector2(-2, 3),
            new Vector2(1, 3),
            new Vector2(2, 3),
            new Vector2(0, 4),
            new Vector2(2, 4),
            new Vector2(3, 4),
            new Vector2(0, 5),
            new Vector2(-1, 5),
        };

        private const int gap = 3;

        /* Calculates room sizes and positions */
        public static List<Rectangle> GetRoomLayout()
        {
            /* Real aspect ratio of blue minimap boxes is 7:3, with a gap of 2 */
            int width = 17, height = 8;

            List<Rectangle> RoomLayout = new List<Rectangle>();
            foreach (Vector2 room in RoomCoordinates) RoomLayout.Add(CalculatePos((int)room.X, (int)room.Y, width, height));

            return RoomLayout;
        }

        public static Rectangle CalculatePos(int x, int y, int width, int height)
        {
            return new Rectangle(x * (width + gap), y * (height + gap) * -1, width, height);
        }
    }
}
