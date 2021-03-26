using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using System.Collections.Generic;

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

        public void Interact(IBlock.PushDirection pushDirection)
        {
            //no interaction
        }
        public void Interact(Vector2 pushDirection)
        {
            //no interaction
        }

        public void Update()
        {
            //nothing to update
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

        public void Reset()
        {
            //doesn't move so no reset needed
        }

        public bool IsMoved()
        {
            return true;
        }
    }
}
