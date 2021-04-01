using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class OrangeMapConstants
    {
        public const int RoomSize = 6;
        public const int CurrentRoomSize = 2;

        public static Color CurrentRoomColor = new Color(107, 203, 44);

        /* Calculates room position given coordinates */
        public static Rectangle CalculatePos(Vector3 coords, int size, int mapWidth, int mapHeight)
        {
            int sizeOffset = (RoomSize - size) / 2;
            int x = (int)(mapWidth / 2.2f + (coords.X - 2) * RoomSize) + sizeOffset;
            int y = (int)(mapHeight / 1.3f + (coords.Y - 5) * RoomSize) + sizeOffset;

            return new Rectangle(x, y, (int)size, (int)size);
        }
    }
}
