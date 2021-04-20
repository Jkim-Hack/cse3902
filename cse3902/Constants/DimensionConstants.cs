using Microsoft.Xna.Framework;
using cse3902.Rooms;

namespace cse3902.Constants
{
    public class DimensionConstants
    {
        //changeable constants
        public const int scale = 3;

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

        public static readonly Vector2 OriginalWindowDimensions = new Vector2(OriginalWindowWidth, OriginalWindowHeight);
        public static readonly Vector2 WindowDimensions = new Vector2(WindowWidth, WindowHeight);

        //setting constants
        public static readonly Vector2 OriginalSettingOffset = new Vector2(0, OriginalWindowHeight);
        public static readonly Vector2 SettingOffset = OriginalSettingOffset * scale;

        public static int TextOffsetX = 75;
        public static int TextOffsetY = 30;
    }
}
