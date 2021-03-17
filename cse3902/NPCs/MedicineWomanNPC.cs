using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.NPCs
{
    class MedicineWomanNPC : INPC
    {
        private ISprite medicineWomanSprite;

        private readonly Game1 game;

        private string message;

        private Vector2 center;

        public MedicineWomanNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            center = start;
            medicineWomanSprite = NPCSpriteFactory.Instance.CreateMedicineWomanSprite(game.SpriteBatch, center);
        }

        public void Update(GameTime gameTime)
        {
            medicineWomanSprite.Update(gameTime);
        }

        public void SetMessage(string msg)
        {
            message = msg;
        }

        public void Draw()
        {
            medicineWomanSprite.Draw();
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }
    }
}
