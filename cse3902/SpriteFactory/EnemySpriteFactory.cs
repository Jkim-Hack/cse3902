using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class EnemySpriteFactory : ISpriteFactory
    {
        private Texture2D enemySpritesheet;
	    private Vector2 defaultCenter;
	
	    private static EnemySpriteFactory enemySpriteFactoryInstance = new EnemySpriteFactory();

        public static EnemySpriteFactory Instance
        {
            get => enemySpriteFactoryInstance;
        }

        private EnemySpriteFactory()
        {
            defaultCenter = new Vector2(300, 200);
        }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpritesheet = content.Load<Texture2D>("");
        }

        public ISprite CreateDragonEnemySprite(SpriteBatch spriteBatch)
        {
            // TODO: Add enemy sprite instance
	        return null;
        }
    }
}
