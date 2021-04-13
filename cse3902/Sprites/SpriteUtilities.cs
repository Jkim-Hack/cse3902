using Microsoft.Xna.Framework;

namespace cse3902.Sprites
{
    internal class SpriteUtilities
    {
        public static float TriforceLayer { get => .05f; }
        public static float GameWonLayer { get => .1f; }
        public static float DeathEffectLayer { get => .1f; }
        public static float EffectsLayer { get => .2f; }
        public static float TopBackgroundLayer { get => .3f; }
        public static float LinkLayer { get => .4f; }
        public static float ProjectileLayer { get => .5f; }
        public static float EnemyLayer { get => .65f; }
        public static float ItemLayer { get => .7f; }
        public static float BlockLayer { get => .8f; }
        public static float BombedDoorBackgroundLayer { get => .9f; }
        public static float BackgroundLayer { get => .95f; }

        public static Rectangle[] distributeFrames(int columns, int rows, int frameWidth, int frameHeight)
        {
            Rectangle[] frames = new Rectangle[columns * rows];
            for (int i = 0; i < columns * rows; i++)
            {
                int row = i / columns;
                int column = i % columns;
                frames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            }
            return frames;
        }
    }
}