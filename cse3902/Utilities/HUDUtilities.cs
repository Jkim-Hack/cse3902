using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Utilities
{
    public class HUDUtilities
    {        
	    public static void DrawRectangle(Game1 game, Rectangle rec, Color color, int offsetX, int offsetY)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + offsetX, rec.Y + offsetY, rec.Width, rec.Height), color);
        }
    }
}
