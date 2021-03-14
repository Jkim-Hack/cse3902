using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
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

        private ICollidable collidable;

        public MedicineWomanNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            centerPosition = start;
            medicineWomanSprite = NPCSpriteFactory.Instance.CreateMedicineWomanSprite(game.spriteBatch, centerPosition);

            this.collidable = new NPCCollidable(this);
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

        public void BeShoved()
        {
            //NPCs don't get shoved
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

        public int Health
        {
            get => 0;

        }

        public Vector2 Direction
        {
            get => new Vector2(0, 0);
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public Vector2 Center
        {
            get => this.centerPosition;
        }
    }
}
