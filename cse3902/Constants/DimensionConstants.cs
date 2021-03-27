using Microsoft.Xna.Framework;
using cse3902.Rooms;

namespace cse3902.Constants
{
    public class DimensionConstants
    {
        //changeable constants
        public const int Scale = 1;

        public const int defaultHudHeight = 56;

        //unchangeable constants
        private const int defaultGameplayHeight = RoomUtilities.ROOM_HEIGHT;
        private const int defaultWindowWidth = RoomUtilities.ROOM_WIDTH;
        private const int defaultWindowHeight = defaultHudHeight + defaultGameplayHeight;

        public const int HudHeight = defaultHudHeight * Scale;
        public const int GameplayHeight = defaultGameplayHeight * Scale;
        public const int WindowWidth = defaultWindowWidth * Scale;
        public const int WindowHeight = defaultWindowHeight * Scale;

        private static Vector2 windowDimensions = new Vector2(WindowWidth, WindowHeight);
        public static Vector2 WindowDimensions { get => windowDimensions; }
    }
}
