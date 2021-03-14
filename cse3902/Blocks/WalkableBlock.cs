using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Blocks
{
    class WalkableBlock : IBlock
    {
        private readonly Game1 game;

        private ISprite walkableBlockSprite;

        private ICollidable collidable;

        public WalkableBlock(Game1 game, ISprite sprite)
        {
            this.game = game;

            walkableBlockSprite = sprite;

            this.collidable = new BlockCollidable(this);
        }

        public void Move(IBlock.PushDirection pushDirection)
        {
            //unmovable block
        }
        public void Move(Vector2 pushDirection)
        {
            //unmovable block
        }
        public void Draw()
        {
            walkableBlockSprite.Draw();
        }

        public ref Rectangle Bounds
        {
            get => ref walkableBlockSprite.Box;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public Vector2 Center
        {
            get => this.walkableBlockSprite.Center;
        }
    }
}
