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
        private Texture2D boggusBoss;
        private Texture2D marioBoss;
        private Texture2D dodongo;
        private Texture2D gel;
        private Texture2D zol;
        private Texture2D goriya;
        private Texture2D goriyaBlue;
        private Texture2D keese;
        private Texture2D stalfos;
        private Texture2D rope;
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
            boggusBoss = content.Load<Texture2D>("enemies/boggusboss");
            marioBoss = content.Load<Texture2D>("enemies/marioboss");
            dodongo = content.Load<Texture2D>("enemies/dodongoboss");
            gel = content.Load<Texture2D>("enemies/gel");
            zol = content.Load<Texture2D>("enemies/zol");
            goriya = content.Load<Texture2D>("enemies/goriya_red");
            goriyaBlue = content.Load<Texture2D>("enemies/goriya_blue");
            keese = content.Load<Texture2D>("enemies/keese");
            stalfos = content.Load<Texture2D>("enemies/stalfos");
            rope = content.Load<Texture2D>("enemies/rope");
            wallmaster = content.Load<Texture2D>("enemies/wall_master");
            trap = content.Load<Texture2D>("trap");
            goriyaDamageSequence = content.Load<Texture2D>("enemies/goriya_hurt");
            bossDamageSequence = content.Load<Texture2D>("enemies/bosses_hurt");
	    }

        public ISprite CreateAquamentusSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new AquamentusSprite(spriteBatch, aquamentus, 2, 2, bossDamageSequence, center);
        }

        public ISprite CreateBoggusBossSprite(SpriteBatch spritebatch, Vector2 center)
        {
            return new BoggusBossSprite(spritebatch, boggusBoss, 2, 2, bossDamageSequence, center);
        }

        public ISprite CreateMarioBossSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new MarioBossSprite(spriteBatch, marioBoss, 2, 2, bossDamageSequence, center);
        }

        public ISprite CreateDodongoSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new DodongoSprite(spriteBatch, dodongo, 5, 2, bossDamageSequence, center);
        }

        public ISprite CreateGelSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new GelSprite(spriteBatch, gel, 1, 2, center);
        }

        public ISprite CreateZolSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new ZolSprite(spriteBatch, zol, 1, 2, center);
        }

        public ISprite CreateGoriyaSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            Texture2D goriyaTexture = goriya;
            if (SettingsValues.Instance.GetValue(SettingsValues.Variable.GoriyaHardTexture) != 0) goriyaTexture = goriyaBlue;
            return new GoriyaSprite(spriteBatch, goriyaTexture, 4, 2, goriyaDamageSequence, center);
        }

        public ISprite CreateKeeseSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new KeeseSprite(spriteBatch, keese, 1, 2, center);
        }

        public ISprite CreateStalfosSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new StalfosSprite(spriteBatch, stalfos, 1, 2, center);
        }

        public ISprite CreateRopeSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new RopeSprite(spriteBatch, rope, 2, 2, center);
        }

        public ISprite CreateWallMasterSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new WallMasterSprite(spriteBatch, wallmaster, 4, 2, center);
        }

        public ISprite CreateTrapSprite(SpriteBatch spriteBatch, Vector2 center)
        {
            return new TrapSprite(spriteBatch, trap, 1, 1, center);
        }
    }
}
