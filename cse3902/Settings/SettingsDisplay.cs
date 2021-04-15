using System.Collections.Generic;
using Microsoft.Xna.Framework;
using cse3902.Constants;
using cse3902.SpriteFactory;
using cse3902.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902
{
    public class SettingsDisplay
    {
        private Game1 game;

        private Dictionary<SettingsManager.Setting, Rectangle> settingsLabelPos;
        private Dictionary<SettingsManager.Setting, Rectangle> settingsPicPos;

        private Dictionary<SettingsManager.Setting, ISprite> settingSprites;
        private Dictionary<SettingsManager.Mode, ISprite> modeSprites;

        private Dictionary<SettingsManager.Mode, Texture2D> modePics;

        private static SettingsDisplay settingsDisplayInstance = new SettingsDisplay();
        public static SettingsDisplay Instance
        {
            get => settingsDisplayInstance;
        }
        private SettingsDisplay()
        {
            LoadSettingsPositions();
            LoadSettingsSprites();
        }
        private void LoadSettingsPositions()
        {
            Vector2 offset = new Vector2(0, DimensionConstants.OriginalWindowHeight);
            Point labelSize = new Point(64, 32);
            Point picSize = new Point(32, 32);

            settingsLabelPos = new Dictionary<SettingsManager.Setting, Rectangle>()
            {
                {SettingsManager.Setting.Utilities, new Rectangle((offset+new Vector2(16,20)).ToPoint(), labelSize)},
                {SettingsManager.Setting.HealthChange, new Rectangle((offset+new Vector2(176,20)).ToPoint(), labelSize)},
                {SettingsManager.Setting.Vision, new Rectangle((offset+new Vector2(16,72)).ToPoint(), labelSize)},
                {SettingsManager.Setting.MinProjectileSwordHealth, new Rectangle((offset+new Vector2(176,72)).ToPoint(), labelSize)},
                {SettingsManager.Setting.ItemDropRate, new Rectangle((offset+new Vector2(16,124)).ToPoint(), labelSize)},
                {SettingsManager.Setting.EnemyStrength, new Rectangle((offset+new Vector2(176,124)).ToPoint(), labelSize)}
            };

            settingsPicPos = new Dictionary<SettingsManager.Setting, Rectangle>()
            {
                {SettingsManager.Setting.Utilities, new Rectangle((offset+new Vector2(88,20)).ToPoint(), picSize)},
                {SettingsManager.Setting.HealthChange, new Rectangle((offset+new Vector2(136,20)).ToPoint(), picSize)},
                {SettingsManager.Setting.Vision, new Rectangle((offset+new Vector2(88,72)).ToPoint(), picSize)},
                {SettingsManager.Setting.MinProjectileSwordHealth, new Rectangle((offset+new Vector2(136,72)).ToPoint(), picSize)},
                {SettingsManager.Setting.ItemDropRate, new Rectangle((offset+new Vector2(88,124)).ToPoint(), picSize)},
                {SettingsManager.Setting.EnemyStrength, new Rectangle((offset+new Vector2(136,124)).ToPoint(), picSize)}
            };
        }
        private void LoadSettingsSprites()
        {

        }

        public void Draw()
        {
        }
        public Game1 Game
        {
            set => game = value;
        }
    }
}