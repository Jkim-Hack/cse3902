using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.SpriteFactory;

namespace cse3902.Blocks
{
    class WaterBlock : IBlock
    {
        private readonly Game1 game;

        private ISprite waterBlockSprite;

        public WaterBlock(Game1 game, Vector2 center)
        {
            this.game = game;
            waterBlockSprite = BlockSpriteFactory.Instance.CreateWaterBlockSprite(game.spriteBatch,center);
        }

        public void Move(IBlock.PushDirection pushDirection)
        {
            //water blocks don't move
        }
        public void Move(Vector2 pushDirection)
        { 
            //water blocks don't move
        }
        public void Draw()
        {
            waterBlockSprite.Draw();
        }
    }
}
