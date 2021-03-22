using Microsoft.Xna.Framework;

namespace cse3902.Sprites
{
    internal class SpriteUtilities
    {
        public static float DoorTopLayer { get => .15f; }
        public static float LinkLayer { get => .2f; }
        public static float ProjectileLayer { get => .3f; }
        public static float EnemyLayer { get => .65f; }
        public static float ItemLayer { get => .7f; }
        public static float BlockLayer { get => .8f; }
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