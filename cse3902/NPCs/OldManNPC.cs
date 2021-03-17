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

        public OldManNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;
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
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }
    }
}
