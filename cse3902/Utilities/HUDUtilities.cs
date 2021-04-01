using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Utilities
{
    public class HUDUtilities
    {
        public const float BackgroundLayer = 1.0f;
        public const float MapLayer = 0.9f;
        public const float TriforceLayer = 0.8f;
        public const float CurrentRoomLayer = 0.7f;

	    public static void DrawRectangle(Game1 game, Rectangle rec, Color color, int offsetX, int offsetY, float layer)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + offsetX, rec.Y + offsetY, rec.Width, rec.Height), null, color, 0, new Vector2(), SpriteEffects.None, layer);
        }
    }
}
