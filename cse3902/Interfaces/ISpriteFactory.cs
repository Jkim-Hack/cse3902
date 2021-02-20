using System;
using Microsoft.Xna.Framework.Content;

namespace cse3902.SpriteFactory
{
    public interface ISpriteFactory
    {
        void LoadAllTextures(ContentManager content);
    }
}
