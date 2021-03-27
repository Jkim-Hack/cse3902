using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapConstants
    {
        /* Real aspect ratio of blue minimap boxes is 7:3, with a gap of 2 */
        public const int Width = 7 * DimensionConstants.Scale;
        public const int Height = 3 * DimensionConstants.Scale;
        private const int gap = 2 * DimensionConstants.Scale;

        public static Color RoomColor = new Color(38, 39, 227);
        public static Color CurrentRoomColor = new Color(107, 203, 44);

        /* "Coordinate system", with the starting room at (2, 5) */
        private static List<Vector2> roomCoordinates = new List<Vector2>();

        /* Calculates room sizes and positions */
        public static List<Rectangle> GetRoomLayout()
        {
            List<Rectangle> roomLayout = new List<Rectangle>();
            foreach (Vector2 room in roomCoordinates) roomLayout.Add(CalculatePos((int)room.X, (int)room.Y, Width, Height));

            return roomLayout;
        }

        public static Rectangle CalculatePos(int x, int y, int width, int height)
        {
            return new Rectangle(x * (width + gap), y * (height + gap) , width, height);
        }

        public static List<Vector2> RoomListZ0
        {
            get => roomCoordinates;
        }

    }
}
