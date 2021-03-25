using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.NPCs
{
    class OldManNPC : INPC
    {
        private ISprite oldManSprite;

        private readonly Game1 game;

        private string message;

        private Vector2 center;

        private TextSprite textSprite;

        public OldManNPC(Game1 game, Vector2 start, string text)
        {
            this.game = game;
            center = start;
            message = text;
            if (message.Length > 0)
            {
                textSprite = new TextSprite(game, text, new Vector2(center.X - 75, center.Y - 30));
            }
            oldManSprite = NPCSpriteFactory.Instance.CreateOldManSprite(game.SpriteBatch, center);
        }
       
        public void Update(GameTime gameTime)
        {
            oldManSprite.Update(gameTime);
        }

        public void SetMessage(string msg)
        {
            message = msg;
        }

        public void Draw()
        {
            oldManSprite.Draw();
            if (message.Length != 0)
            {
                textSprite.Draw();
            }
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }
    }
}
