using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class NPCSpriteFactory : ISpriteFactory
    {
        private Dictionary<string, Texture2D> npcTextures;
        private Dictionary<SettingsManager>
        private Texture2D flame;

        private static NPCSpriteFactory npcSpriteFactoryInstance = new NPCSpriteFactory();

        public static NPCSpriteFactory Instance
        {
            get => npcSpriteFactoryInstance;
        }

        private NPCSpriteFactory()
        {
            npcTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadAllTextures(ContentManager content)
        {
            npcTextures.Add("oldman", content.Load<Texture2D>("oldman"));
            npcTextures.Add("medicinewoman", content.Load<Texture2D>("medicinewoman"));
            npcTextures.Add("merchant", content.Load<Texture2D>("merchant"));
            npcTextures.Add("grabbedlink", content.Load<Texture2D>("GrabbedLink"));

            npcTextures.Add("easy", content.Load<Texture2D>("easy"));
            npcTextures.Add("normal", content.Load<Texture2D>("normal"));
            npcTextures.Add("hard", content.Load<Texture2D>("hard"));
            npcTextures.Add("utilities", content.Load<Texture2D>("utilities"));
            npcTextures.Add("vision", content.Load<Texture2D>("vision"));
            npcTextures.Add("item", content.Load<Texture2D>("itemdroprate"));
            npcTextures.Add("enemy", content.Load<Texture2D>("enemystrength"));
            npcTextures.Add("sword", content.Load<Texture2D>("minprojhealth"));
            npcTextures.Add("health", content.Load<Texture2D>("health"));

            flame = content.Load<Texture2D>("fire");
        }

        public ISprite CreateOldManSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new NPCSprite(spriteBatch, npcTextures["oldman"], startingPos, false);
        }

        public ISprite CreateMedicineWomanSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new NPCSprite(spriteBatch, npcTextures["medicinewoman"], startingPos, false);
        }
        
        public ISprite CreateMerchantSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new NPCSprite(spriteBatch, npcTextures["merchant"], startingPos, false);
        }

        public ISprite CreateGrabbedLinkSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new NPCSprite(spriteBatch, npcTextures["grabbedlink"], startingPos, true);
        }

        public ISprite CreateEasySprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["easy"], startingPos);
        }

        public ISprite CreateNormalSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["normal"], startingPos);
        }

        public ISprite CreateHardSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["hard"], startingPos);
        }

        public ISprite CreateUtilitiesSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["utilities"], startingPos);
        }

        public ISprite CreateVisionSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["vision"], startingPos);
        }

        public ISprite CreateItemSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["item"], startingPos);
        }

        public ISprite CreateEnemySprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["enemy"], startingPos);
        }

        public ISprite CreateSwordSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["sword"], startingPos);
        }

        public ISprite CreateHealthSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new SettingsSprite(spriteBatch, npcTextures["health"], startingPos);
        }

        public ISprite CreateFlameSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new FlameSprite(spriteBatch, flame, startingPos);
        }
    }
}
