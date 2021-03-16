using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.Entities
{
    class FlameNPC : INPC
    {
        private ISprite flameSprite;

        private readonly Game1 game;


        private string message;

        public FlameNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            flameSprite = NPCSpriteFactory.Instance.CreateFlameSprite(game.SpriteBatch, start);

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

    }
}
