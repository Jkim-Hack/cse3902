using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapConstants
    {
        /* Real aspect ratio of blue minimap boxes is 7:3, with a gap of 2 */
        public const int Width = 7;
        public const int Height = 3;
        public const int gap = 1;
        public const int COLOR_DELAY = 10;

        public const int XOffset = 25;
        public const int YOffset = 32;

        public const int LevelDiv = 3;

        public static Color RoomColor = new Color(38, 39, 227);
        public static Color CurrentRoomColor = new Color(107, 203, 44);
        public static Color TriforceRed = new Color(220, 20, 60);
        public static Color TriforceGreen = new Color(0, 100, 0);

        public static Vector2 TriforcePos1 = new Vector2(5, 1);
        public static Vector2 TriforcePos2 = new Vector2(2, 3);
        public static Vector2 TriforcePos3 = new Vector2(4, 0);
        public static Vector2 TriforcePos4 = new Vector2(4, 1);

        public static Vector2[] TriforcePos = { TriforcePos1, TriforcePos2, TriforcePos3, TriforcePos4 };

        /* "Coordinate system", with the starting room at (2, 5) */
        private static List<Vector3> roomCoordinates = new List<Vector3>();

        /* Calculates room sizes and positions */
        public static List<Rectangle> GetRoomLayout(int level)
        {
            List<Rectangle> roomLayout = new List<Rectangle>();
            foreach (Vector3 room in roomCoordinates)
            {
                if ((int)room.Z/2 == level)
                {
                    roomLayout.Add(CalculatePos((int)room.X, (int)room.Y, Width, Height));
                }
            }

            return roomLayout;
        }

        public static Rectangle CalculatePos(int x, int y, int width, int height)
        {
            return new Rectangle(x * (width + gap), y * (height + gap) , width, height);
        } 

        public static List<Vector3> RoomListZ0
        {
            get => roomCoordinates;
        }

    }
}
