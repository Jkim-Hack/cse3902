using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapConstants
    {
        /* Real aspect ratio of blue minimap boxes is 7:3, with a gap of 2 */
        public const int Width = 18;
        public const int Height = 8;
        private const int gap = 3;

        public static Color RoomColor = new Color(38, 39, 227);
        public static Color CurrentRoomColor = new Color(107, 203, 44);

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

        /* Calculates room sizes and positions */
        public static List<Rectangle> GetRoomLayout()
        {
            List<Rectangle> RoomLayout = new List<Rectangle>();
            foreach (Vector2 room in RoomCoordinates) RoomLayout.Add(CalculatePos((int)room.X, (int)room.Y, Width, Height));

            return RoomLayout;
        }

        public static Rectangle CalculatePos(int x, int y, int width, int height)
        {
            return new Rectangle(x * (width + gap), y * (height + gap) * -1, width, height);
        }
    }
}
