using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.SpriteFactory
{
    public class NPCSpriteFactory
    {
        private Dictionary<string, Texture2D> npcTextures;
        private Dictionary<SettingsManager.Setting, Texture2D> settingTextures;
        private Dictionary<SettingsManager.Mode, Texture2D> modeTextures;
        private Texture2D flame;

        private SpriteBatch spriteBatch;

        private static NPCSpriteFactory npcSpriteFactoryInstance = new NPCSpriteFactory();

        public static NPCSpriteFactory Instance
        {
            get => npcSpriteFactoryInstance;
        }

        private NPCSpriteFactory()
        {
            npcTextures = new Dictionary<string, Texture2D>();
            settingTextures = new Dictionary<SettingsManager.Setting, Texture2D>();
            modeTextures = new Dictionary<SettingsManager.Mode, Texture2D>();
        }

        public void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            npcTextures.Add("oldman", content.Load<Texture2D>("oldman"));
            npcTextures.Add("medicinewoman", content.Load<Texture2D>("medicinewoman"));
            npcTextures.Add("merchant", content.Load<Texture2D>("merchant"));
            npcTextures.Add("grabbedlink", content.Load<Texture2D>("GrabbedLink"));

            modeTextures.Add(SettingsManager.Mode.Easy, content.Load<Texture2D>("easy"));
            modeTextures.Add(SettingsManager.Mode.Normal, content.Load<Texture2D>("normal"));
            modeTextures.Add(SettingsManager.Mode.Hard, content.Load<Texture2D>("hard"));

            settingTextures.Add(SettingsManager.Setting.Utilities, content.Load<Texture2D>("utilities"));
            settingTextures.Add(SettingsManager.Setting.Vision, content.Load<Texture2D>("vision"));
            settingTextures.Add(SettingsManager.Setting.ItemDropRate, content.Load<Texture2D>("itemdroprate"));
            settingTextures.Add(SettingsManager.Setting.EnemyStrength, content.Load<Texture2D>("enemystrength"));
            settingTextures.Add(SettingsManager.Setting.MinProjectileSwordHealth, content.Load<Texture2D>("minprojhealth"));
            settingTextures.Add(SettingsManager.Setting.HealthChange, content.Load<Texture2D>("health"));

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

        public ISprite CreateSettingSprite(Vector2 startingPos, SettingsManager.Setting setting)
        {
            return new SettingsSprite(spriteBatch, settingTextures[setting], startingPos);
        }

        public ISprite CreateModeSprite(Vector2 startingPos, SettingsManager.Mode mode)
        {
            return new SettingsSprite(spriteBatch, modeTextures[mode], startingPos);
        }

        public ISprite CreateFlameSprite(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            return new FlameSprite(spriteBatch, flame, startingPos);
        }
    }
}
