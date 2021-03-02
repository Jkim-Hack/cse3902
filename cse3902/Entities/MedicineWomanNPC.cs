using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.Entities
{
    class MedicineWomanNPC : IEntity
    {
        private ISprite medicineWomanSprite;

        private readonly Game1 game;

        private Vector2 centerPosition;

        private string message;

        public MedicineWomanNPC(Game1 game)
        {
            this.game = game;
            centerPosition = new Vector2(500, 200);
            medicineWomanSprite = NPCSpriteFactory.Instance.CreateMedicineWomanSprite(game.spriteBatch, centerPosition);
        }

        public ref Rectangle Bounds
        {
            get => ref medicineWomanSprite.Box;
        }

        public void Attack()
        {
            //NPCs don't attack
        }
        public void ChangeDirection(Vector2 direction)
        {
            //NPCs don't change direction
        }
        public void TakeDamage(int damage)
        {
            //NPCs don't take damage
        }
        public void Die()
        {
            //NPCs don't die
        }
       
        public void Update(GameTime gameTime)
        {
            medicineWomanSprite.Update(gameTime);
        }

        public void setMessage(string msg)
        {
            message = msg;
        }
        public void Draw()
        {
            medicineWomanSprite.Draw();
        }
    }
}
