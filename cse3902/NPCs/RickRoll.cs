using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.NPCs
{
    public class RickRollNPC : INPC
    {
        private ISprite rickRollSprite;

        private readonly Game1 game;

        private string message;

        private Vector2 center;

        public RickRollNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;
            rickRollSprite = NPCSpriteFactory.Instance.CreateFlameSprite(game.SpriteBatch, center);
        }

        public void Update(GameTime gameTime)
        {
            rickRollSprite.Update(gameTime);
        }

        public void SetMessage(string msg)
        {
            message = msg;
        }

        public void Draw()
        {
            rickRollSprite.Draw();
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

    }
}