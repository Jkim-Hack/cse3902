using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.Entities
{
    class OldManNPC : IEntity
    {
        private ISprite oldManSprite;

        private readonly Game1 game;

        private Vector2 centerPosition;

        private string message;

        public OldManNPC(Game1 game)
        {
            this.game = game;
            centerPosition = new Vector2(500, 200);
            oldManSprite = NPCSpriteFactory.Instance.CreateOldManSprite(game.spriteBatch,centerPosition);
        }
        public ref Rectangle Bounds
        {
            get => ref oldManSprite.Box;
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
            oldManSprite.Update(gameTime);
        }

        public void setMessage(string msg)
        {
            message = msg;
        }
        public void Draw()
        {
            oldManSprite.Draw();
        }
    }
}
