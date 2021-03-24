using Microsoft.Xna.Framework;
using cse3902.Rooms;

namespace cse3902.Constants
{
    public class DimensionConstants
    {
        //changeable constants
        private const int scale = 3;

        public const int defaultHudHeight = 56;

        //unchangeable constants
        private const int defaultGameplayHeight = RoomUtilities.ROOM_HEIGHT;
        private const int defaultWindowWidth = RoomUtilities.ROOM_WIDTH;
        private const int defaultWindowHeight = defaultHudHeight + defaultGameplayHeight;

        public const int HudHeight = defaultHudHeight * scale;
        public const int GameplayHeight = defaultGameplayHeight * scale;
        public const int WindowWidth = defaultWindowWidth * scale;
        public const int WindowHeight = defaultWindowHeight * scale;

        private static Vector2 windowDimensions = new Vector2(WindowWidth, WindowHeight);
        public static Vector2 WindowDimensions { get => windowDimensions; }
    }
}
