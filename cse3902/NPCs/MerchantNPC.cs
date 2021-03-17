using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.Entities
{
    class MerchantNPC : INPC
    {
        private ISprite merchantSprite;

        private readonly Game1 game;

        private string message;

        public MerchantNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            merchantSprite = NPCSpriteFactory.Instance.CreateMerchantSprite(game.SpriteBatch, start);
        }

        public void Update(GameTime gameTime)
        {
            merchantSprite.Update(gameTime);
        }

        public void SetMessage(string msg)
        {
            message = msg;
        }
        public void Draw()
        {
            merchantSprite.Draw();
        }

    }
}
