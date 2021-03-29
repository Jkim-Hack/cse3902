using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites.EnemySprites;

namespace cse3902.SpriteFactory
{
    public class EnemySpriteFactory : ISpriteFactory
    {
        private Texture2D aquamentus;
        private Texture2D gel;
        private Texture2D goriya;
        private Texture2D keese;
        private Texture2D stalfos;
        private Texture2D wallmaster;
        private Texture2D trap;

        private Texture2D goriyaDamageSequence;
        private Texture2D bossDamageSequence;
	
	    private static EnemySpriteFactory enemySpriteFactoryInstance = new EnemySpriteFactory();

        public static EnemySpriteFactory Instance
        {
            get => enemySpriteFactoryInstance;
        }

        private EnemySpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            aquamentus = content.Load<Texture2D>("aquamentus");
            gel = content.Load<Texture2D>("enemies/gel");
            goriya = content.Load<Texture2D>("enemies/goriya_blue");
            keese = content.Load<Texture2D>("enemies/keese");
            stalfos = content.Load<Texture2D>("enemies/stalfos");
            wallmaster = content.Load<Texture2D>("enemies/wall_master");
            trap = content.Load<Texture2D>("trap");
            goriyaDamageSequence = content.Load<Texture2D>("enemies/goriya_hurt");
            bossDamageSequence = content.Load<Texture2D>("enemies/bosses_hurt");
	    }

        public ISprite CreateAquamentusSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new AquamentusSprite(spriteBatch, aquamentus, 2, 2, bossDamageSequence, center);
        }

        public ISprite CreateGelSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new GelSprite(spriteBatch, gel, 1, 2, center);
        }

        public ISprite CreateGoriyaSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new GoriyaSprite(spriteBatch, goriya, 4, 2, goriyaDamageSequence, center);
        }

        public ISprite CreateKeeseSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new KeeseSprite(spriteBatch, keese, 1, 2, center);
        }

        public ISprite CreateStalfosSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new StalfosSprite(spriteBatch, stalfos, 1, 2, center);
        }

        public ISprite CreateWallMasterSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new WallMasterSprite(spriteBatch, wallmaster, 4, 2, center);
        }

        public ISprite CreateTrapSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new TrapSprite(spriteBatch, wallmaster, 1, 1, center);
        }
    }
}
