using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.NPCs
{
    class FlameNPC : INPC
    {
        private ISprite flameSprite;

        private readonly Game1 game;

        private string message;

        private Vector2 center;

        public FlameNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;
            flameSprite = NPCSpriteFactory.Instance.CreateFlameSprite(game.SpriteBatch, center);
        }

        public void Update(GameTime gameTime)
        {
            flameSprite.Update(gameTime);
        }

        public void SetMessage(string msg)
        {
            message = msg;
        }

        public void Draw()
        {
            flameSprite.Draw();
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

    }
}
