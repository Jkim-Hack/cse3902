using System.Collections.Generic;

namespace cse3902
{
    public class SettingsManager
    {
        public enum Setting
        {
            Utilities,
            Vision,
            ItemDropRate,
            EnemyStrength,
            MinProjectileSwordHealth,
            HealthChange
        }
        public enum Mode
        {
            Easy,
            Normal,
            Hard
        }

        private Dictionary<Setting, Mode> settings;

        private static SettingsManager settingsManagerInstance = new SettingsManager();
        public static SettingsManager Instance
        {
            get => settingsManagerInstance;
        }
        private SettingsManager()
        {
            settings = new Dictionary<Setting, Mode>();
            settings.Add(Setting.Utilities, Mode.Normal);
            settings.Add(Setting.Vision, Mode.Easy);
            settings.Add(Setting.ItemDropRate, Mode.Normal);
            settings.Add(Setting.EnemyStrength, Mode.Normal);
            settings.Add(Setting.MinProjectileSwordHealth, Mode.Hard);
            settings.Add(Setting.HealthChange, Mode.Normal);
        }
        public void LoadManager()
        {
            SettingsDisplay.Instance.LoadSettingsPositions();
            SettingsDisplay.Instance.LoadSettingsSprites();
        }

        public void Draw()
        {
            if (GameStateManager.Instance.IsPaused()) SettingsDisplay.Instance.Draw();
        }
        public void UpdateMode(Setting setting)
        {
            Mode mode = Settings[setting];
            Mode newMode;
            switch (mode)
            {
                case Mode.Easy:
                    newMode = Mode.Normal;
                    break;
                case Mode.Normal:
                    newMode = Mode.Hard;
                    break;
                case Mode.Hard:
                    newMode = Mode.Easy;
                    break;
                default: //this should never happen
                    newMode = Mode.Normal;
                    break;
            }
            Settings[setting] = newMode;
        }

        public Dictionary<Setting,Mode> Settings
        {
            get => settings;
        }
    }
}