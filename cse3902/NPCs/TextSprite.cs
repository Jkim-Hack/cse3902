using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cse3902.NPCs
{
    public class TextSprite
    {
        private Game1 game;
        private SpriteFont text;
        private String message;
        private Vector2 location;

        public TextSprite(Game1 gm, String words, Vector2 loc)
        {
            game = gm;
            location = loc;
            text = gm.Content.Load<SpriteFont>("npcroom");
            message = words;
        }

        public void Draw()
        {
            game.SpriteBatch.DrawString(text, message, location, Color.White);
        }
    }
}
