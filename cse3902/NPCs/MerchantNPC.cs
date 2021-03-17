using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.NPCs
{
    class MerchantNPC : INPC
    {
        private ISprite merchantSprite;

        private readonly Game1 game;

        private string message;

        private Vector2 center;

        public MerchantNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;
            merchantSprite = NPCSpriteFactory.Instance.CreateMerchantSprite(game.SpriteBatch, center);
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

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }
    }
}
