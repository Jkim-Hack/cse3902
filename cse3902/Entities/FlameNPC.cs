using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.Entities
{
    class FlameNPC : IEntity
    {
        private ISprite flameSprite;

        private readonly Game1 game;

        private Vector2 centerPosition;

        private string message;

        private ICollidable collidable;

        public FlameNPC(Game1 game, Vector2 start)
        {
            this.game = game;
            centerPosition = start;
            flameSprite = NPCSpriteFactory.Instance.CreateFlameSprite(game.SpriteBatch, centerPosition);

            this.collidable = new NPCCollidable(this);
        }
        public ref Rectangle Bounds
        {
            get => ref flameSprite.Box;
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
            flameSprite.Update(gameTime);
        }

        public void setMessage(string msg)
        {
            message = msg;
        }
        public void Draw()
        {
            flameSprite.Draw();
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
