using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;

namespace cse3902.Entities
{
    class MerchantNPC : IEntity
    {
        private ISprite merchantSprite;

        private readonly Game1 game;

        private Vector2 centerPosition;

        private string message;

        public MerchantNPC(Game1 game)
        {
            this.game = game;
            centerPosition = new Vector2(500, 200);
            merchantSprite = NPCSpriteFactory.Instance.CreateMerchantSprite(game.spriteBatch, centerPosition);
        }
        public ref Rectangle Bounds
        {
            get => ref merchantSprite.Box;
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
            merchantSprite.Update(gameTime);
        }

        public void setMessage(string msg)
        {
            message = msg;
        }
        public void Draw()
        {
            merchantSprite.Draw();
        }
    }
}
