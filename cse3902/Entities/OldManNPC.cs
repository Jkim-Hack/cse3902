using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
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

        private ICollidable collidable;

        public OldManNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            centerPosition = start;
            oldManSprite = NPCSpriteFactory.Instance.CreateOldManSprite(game.spriteBatch,centerPosition);

            this.collidable = new NPCCollidable(this);
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

        public void BeShoved()
        {
            //NPCs don't get shoved
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
