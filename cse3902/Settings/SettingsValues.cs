using System.Collections.Generic;

namespace cse3902
{
    public class SettingsValues
    {
        private Dictionary<Variable, SettingsManager.Setting> varToSetting;
        private Dictionary<SettingsManager.Mode, int> modeToInt;
        private Dictionary<Variable, List<int>> varToListInt;
        public enum Variable
        {
            //enemy strength (enemy health and damage changes will only apply after a reset, projectiles apply immediately) (0-14)
            AquamentusHealth,
            AquamentusDamage,
            AquamentusFireball,
            BoggusBossHealth,
            BoggusBossDamage,
            GoriyaHealth,
            GoriyaDamage,
            GoriyaBoomerang,
            GelHealth,
            GelDamage,
            KeeseHealth,
            KeeseDamage,
            StalfosHealth,
            StalfosDamage,
            TrapDamage,
            WallMasterHealth,
            WallMasterDamage,
            //health change (over time) (15-16)
            HealthChange,
            HealthChangeDelay,
            //vision (17-18)
            VisionRange,
            VisionRangeNoCandle,
            //min sword health (19-19)
            MinProjectileSwordHealth, //negative means (maxhealth+value+1)
            //item drop rate (20-24)
            ItemDropA,
            ItemDropB,
            ItemDropC,
            ItemDropD,
            ItemDropX,
            //Utilities (25-29)
            HealthIncrease,
            HealthDecrease,
            MaxHealthIncrease,
            MaxHealthDecrease,
            SpaceKillEnemies //this acts as a boolean
        }

        private static SettingsValues settingValuesInstance = new SettingsValues();
        public static SettingsValues Instance
        {
            get => settingValuesInstance;
        }
        private SettingsValues()
        {
            LoadVarToSetting();
            LoadModeToInt();
            LoadVarToListInt();
        }

        private void LoadVarToSetting()
        {
            varToSetting = new Dictionary<Variable, SettingsManager.Setting>();
            for (int i=0; i<30; i++)
            {
                if (i < 15) varToSetting.Add((Variable)i, SettingsManager.Setting.EnemyStrength);
                else if (i < 17) varToSetting.Add((Variable)i, SettingsManager.Setting.HealthChange);
                else if (i < 19) varToSetting.Add((Variable)i, SettingsManager.Setting.Vision);
                else if (i < 20) varToSetting.Add((Variable)i, SettingsManager.Setting.MinProjectileSwordHealth);
                else if (i < 25) varToSetting.Add((Variable)i, SettingsManager.Setting.ItemDropRate);
                else varToSetting.Add((Variable)i, SettingsManager.Setting.Utilities);
            }
        }
        private void LoadModeToInt()
        {
            modeToInt = new Dictionary<SettingsManager.Mode, int>();
            for (int i = 0; i < 3; i++) modeToInt.Add((SettingsManager.Mode)i, i);
        }
        private void LoadVarToListInt()
        {
            varToListInt = new Dictionary<Variable, List<int>>();
            varToListInt.Add((Variable)0, new List<int>() { 4, 6, 10 });
            varToListInt.Add((Variable)1, new List<int>() { 1, 2, 4 });
            varToListInt.Add((Variable)2, new List<int>() { 1, 1, 3 });
            varToListInt.Add((Variable)3, new List<int>() { 2, 3, 5 });
            varToListInt.Add((Variable)4, new List<int>() { 1, 1, 2 });
            varToListInt.Add((Variable)5, new List<int>() { 1, 2, 4 });
            varToListInt.Add((Variable)6, new List<int>() { 1, 1, 2 });
            varToListInt.Add((Variable)7, new List<int>() { 1, 1, 2 });
            varToListInt.Add((Variable)8, new List<int>() { 1, 1, 2 });
            varToListInt.Add((Variable)9, new List<int>() { 1, 1, 2 });
            varToListInt.Add((Variable)10, new List<int>() { 1, 2, 3 });
            varToListInt.Add((Variable)11, new List<int>() { 1, 1, 2 });
            varToListInt.Add((Variable)12, new List<int>() { 1, 2, 3 });
            varToListInt.Add((Variable)13, new List<int>() { 1, 2, 3 });
            varToListInt.Add((Variable)14, new List<int>() { 1, 1, 3 });
            varToListInt.Add((Variable)15, new List<int>() { 1, 0, -1 });
            varToListInt.Add((Variable)16, new List<int>() { 900, 3600, 3600 });
            varToListInt.Add((Variable)17, new List<int>() { 512, 72, 48 });
            varToListInt.Add((Variable)18, new List<int>() { 48, 32, 16 });
            varToListInt.Add((Variable)19, new List<int>() { 0, -5, -1 });
            varToListInt.Add((Variable)20, new List<int>() { 41, 31, 16 });
            varToListInt.Add((Variable)21, new List<int>() { 59, 41, 31 });
            varToListInt.Add((Variable)22, new List<int>() { 75, 59, 41 });
            varToListInt.Add((Variable)23, new List<int>() { 59, 41, 31 });
            varToListInt.Add((Variable)24, new List<int>() { 31, 9, 5 });
            varToListInt.Add((Variable)25, new List<int>() { 1, 0, 0 });
            varToListInt.Add((Variable)26, new List<int>() { 1, 0, 1 });
            varToListInt.Add((Variable)27, new List<int>() { 2, 0, 0 });
            varToListInt.Add((Variable)28, new List<int>() { 2, 0, 2 });
            varToListInt.Add((Variable)29, new List<int>() { 1, 0, 0 });
        }

        public int GetValue(Variable variable)
        {
            return varToListInt[variable][modeToInt[SettingsManager.Instance.Settings[varToSetting[variable]]]];
        }
    }

}