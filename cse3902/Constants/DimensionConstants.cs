using Microsoft.Xna.Framework;
using cse3902.Rooms;

namespace cse3902.Constants
{
    public class DimensionConstants
    {
        //changeable constants
        private const int scale = 3;

        public const int OriginalHudHeight = 56;
        private const int originalMouseClickSide = 32;

        //unchangeable constants
        public const int MouseClickSide = originalMouseClickSide * scale;

        public const int OriginalGameplayHeight = RoomUtilities.ROOM_HEIGHT;
        public const int OriginalWindowWidth = RoomUtilities.ROOM_WIDTH;
        public const int OriginalWindowHeight = OriginalHudHeight + OriginalGameplayHeight;

        public const int HudHeight = OriginalHudHeight * scale;
        public const int GameplayHeight = OriginalGameplayHeight * scale;
        public const int WindowWidth = OriginalWindowWidth * scale;
        public const int WindowHeight = OriginalWindowHeight * scale;

        private static Vector2 originalWindowDimensions = new Vector2(OriginalWindowWidth, OriginalWindowHeight);
        private static Vector2 windowDimensions = new Vector2(WindowWidth, WindowHeight);
        public static Vector2 OriginalWindowDimensions { get => originalWindowDimensions; }
        public static Vector2 WindowDimensions { get => windowDimensions; }
    }
}
