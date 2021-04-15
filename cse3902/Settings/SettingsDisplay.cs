using System.Collections.Generic;
using Microsoft.Xna.Framework;
using cse3902.Constants;
using cse3902.SpriteFactory;
using cse3902.Interfaces;

namespace cse3902
{
    public class SettingsDisplay
    {
        private Dictionary<SettingsManager.Setting, Rectangle> settingsPos;
        private Dictionary<SettingsManager.Setting, Rectangle> modePos;

        private Dictionary<SettingsManager.Setting, ISprite> settingsSprites;
        private Dictionary<SettingsManager.Setting, ISprite> modeSprites;

        private static SettingsDisplay settingsDisplayInstance = new SettingsDisplay();
        public static SettingsDisplay Instance
        {
            get => settingsDisplayInstance;
        }
        private SettingsDisplay()
        {
        }
        public void LoadSettingsPositions()
        {
            Vector2 offset = DimensionConstants.OriginalSettingOffset;
            Point labelSize = new Point(64, 32);
            Point picSize = new Point(32, 32);

            settingsPos = new Dictionary<SettingsManager.Setting, Rectangle>()
            {
                {SettingsManager.Setting.Utilities, new Rectangle((offset+new Vector2(16,20)).ToPoint(), labelSize)},
                {SettingsManager.Setting.HealthChange, new Rectangle((offset+new Vector2(176,20)).ToPoint(), labelSize)},
                {SettingsManager.Setting.Vision, new Rectangle((offset+new Vector2(16,72)).ToPoint(), labelSize)},
                {SettingsManager.Setting.MinProjectileSwordHealth, new Rectangle((offset+new Vector2(176,72)).ToPoint(), labelSize)},
                {SettingsManager.Setting.ItemDropRate, new Rectangle((offset+new Vector2(16,124)).ToPoint(), labelSize)},
                {SettingsManager.Setting.EnemyStrength, new Rectangle((offset+new Vector2(176,124)).ToPoint(), labelSize)}
            };

            modePos = new Dictionary<SettingsManager.Setting, Rectangle>()
            {
                {SettingsManager.Setting.Utilities, new Rectangle((offset+new Vector2(88,20)).ToPoint(), picSize)},
                {SettingsManager.Setting.HealthChange, new Rectangle((offset+new Vector2(136,20)).ToPoint(), picSize)},
                {SettingsManager.Setting.Vision, new Rectangle((offset+new Vector2(88,72)).ToPoint(), picSize)},
                {SettingsManager.Setting.MinProjectileSwordHealth, new Rectangle((offset+new Vector2(136,72)).ToPoint(), picSize)},
                {SettingsManager.Setting.ItemDropRate, new Rectangle((offset+new Vector2(88,124)).ToPoint(), picSize)},
                {SettingsManager.Setting.EnemyStrength, new Rectangle((offset+new Vector2(136,124)).ToPoint(), picSize)}
            };
        }
        public void LoadSettingsSprites()
        {
            settingsSprites = new Dictionary<SettingsManager.Setting, ISprite>();
            foreach (SettingsManager.Setting setting in settingsPos.Keys)
            {
                Rectangle r = settingsPos[setting];
                settingsSprites.Add(setting, NPCSpriteFactory.Instance.CreateSettingSprite(new Vector2(r.X, r.Y), setting));
            }

            modeSprites = new Dictionary<SettingsManager.Setting, ISprite>();
            foreach (SettingsManager.Setting setting in modePos.Keys)
            {
                Rectangle r = modePos[setting];
                modeSprites.Add(setting, NPCSpriteFactory.Instance.CreateModeSprite(new Vector2(r.X, r.Y), SettingsManager.Instance.Settings[setting]));
            }
        }

        public void Draw()
        {
            foreach(SettingsManager.Setting setting in settingsSprites.Keys)
            {
                settingsSprites[setting].Draw();
                modeSprites[setting].Draw();
            }
        }

        public Dictionary<SettingsManager.Setting, Rectangle> ModeRectangles
        {
            get => modePos;
        }
    }
}