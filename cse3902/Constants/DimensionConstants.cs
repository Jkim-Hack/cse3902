using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class DimensionConstants
    {
        private const int scale = 3;

        private const int defaultHudHeight = 56;
        private const int defaultWindowWidth = 256;
        private const int defaultWindowHeight = 232;

        public const int HudHeight = defaultHudHeight * scale;
        public const int WindowWidth = defaultWindowWidth * scale;
        public const int WindowHeight = defaultWindowHeight * scale;

        private static Vector2 windowDimensions = new Vector2(WindowWidth, WindowHeight);
        public static Vector2 WindowDimensions { get => windowDimensions; }
    }
}
