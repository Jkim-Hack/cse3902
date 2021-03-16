using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using System.Collections.Generic;
using cse3902.Rooms;

namespace cse3902.Blocks
{
    class MovementBlock : IBlock
    {
        private readonly Game1 game;
        private Vector2 center;
        private Rectangle dest;

        private ICollidable collidable;

        public MovementBlock(Game1 game, Vector2 center)
        {
            this.game = game;

            this.collidable = new BlockCollidable(this);
            this.center = center;
        }

        public void Interact(IBlock.PushDirection pushDirection)
        {
            List<Vector2> directions = new List<Vector2>();
            directions.Add(new Vector2(0, 0));
            directions.Add(new Vector2(1, 0));
            directions.Add(new Vector2(-1, 0));

            game.Player.Directions = directions;
        }
        public void Interact(Vector2 pushDirection)
        {
            List<Vector2> directions = new List<Vector2>();
            directions.Add(new Vector2(0, 0));
            directions.Add(new Vector2(1, 0));
            directions.Add(new Vector2(-1, 0));

            game.Player.Directions = directions;
        }
        public void Draw()
        {
            //nothing to draw
        }

        public ref Rectangle Bounds
        {
            get
            {
                Rectangle destination = new Rectangle((int)center.X, (int)center.Y, RoomUtilities.BLOCK_SIDE, RoomUtilities.BLOCK_SIDE);
                destination.Offset(-destination.Width / 2, -destination.Height / 2);
                this.dest = destination;
                return ref dest;
            }
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public Vector2 Center
        {
            get => center;
        }
    }
}
