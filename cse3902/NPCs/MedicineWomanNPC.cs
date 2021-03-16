using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.Entities
{
    class MedicineWomanNPC : INPC
    {
        private ISprite medicineWomanSprite;

        private readonly Game1 game;

        private string message;

        public MedicineWomanNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            medicineWomanSprite = NPCSpriteFactory.Instance.CreateMedicineWomanSprite(game.SpriteBatch, start);
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
    }
}
