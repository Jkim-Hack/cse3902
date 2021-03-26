using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class MiniMapRoomLayout
    {
        public const int offsetX = 100;
        public const int offsetY = 600;

        public const int currentRoomSize = 8;

        /* Real aspect ratio of blue minimap boxes is 7:3, with a gap of 2 */
        private const int width = 16;
        private const int height = 8;
        private const int gap = 3;

        /* Can be thought of as a "coordinate system", with the starting room at (0, 0) */
        public static List<Rectangle> RoomLayout = new List<Rectangle>
        {
            new Rectangle(0 * (width + gap), 0 * (height + gap) * -1, width, height),
            new Rectangle(-1 * (width + gap), 0 * (height + gap) * -1, width, height),
            new Rectangle(1 * (width + gap), 0 * (height + gap) * -1, width, height),
            new Rectangle(0 * (width + gap), 1 * (height + gap) * -1, width, height)
        };
    }
}
