using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.Entities
{
    class OldManNPC : INPC
    {
        private ISprite oldManSprite;

        private readonly Game1 game;

        private string message;

        public OldManNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            oldManSprite = NPCSpriteFactory.Instance.CreateOldManSprite(game.SpriteBatch,start);
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
    }
}
