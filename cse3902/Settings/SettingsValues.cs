using System.Collections.Generic;

namespace cse3902
{
    public class SettingsValues
    {
        private Dictionary<Variable, SettingsManager.Setting> varToSetting;
        private Dictionary<SettingsManager.Mode, int> modeToInt;
        private Dictionary<Variable, List<int>> varToListInt;

        private List<int> variableSeparator;
        int numVariables;
        public enum Variable //TO ADD OR REMOVE A VARIABLE, MAKE A CHANGE HERE AND IN THE PRIVATE CONSTRUCTOR
        {
            //enemy strength (enemy health and damage changes will only apply after a reset, projectiles apply immediately
            AquamentusHealth,
            AquamentusDamage,
            FireballDamage,
            BoggusHealth,
            BoggusDamage,
            MarioHealth,
            MarioDamage,
            RopeHealth,
            RopeDamage,
            ZolHealth,
            ZolDamage,
            DodongoHealth,
            DodongoDamage,
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
            GoriyaHardTexture, //this acts as a boolean
            GoriyaEnemyType, //needs to be casted to enemy type enum
            MinLinkDamage,
            StalfosSpawnDelay,
            GoriyaSpawnDelay,
            KeeseSpawnDelay,
            GelSpawnDelay,
            //health change (over time)
            HealthChange,
            HealthChangeDelay,
            //vision
            VisionRange,
            VisionRangeNoCandle,
            //min sword health
            MinProjectileSwordHealth, //negative means (maxhealth+value+1)
            //item drop rate
            ItemDropA,
            ItemDropB,
            ItemDropC,
            ItemDropD,
            ItemDropX,
            RupeePickup,
            //Utilities
            HealthIncrease,
            HealthDecrease,
            MaxHealthIncrease,
            MaxHealthDecrease,
            SpaceKillEnemies, //this acts as a boolean
        }

        private static SettingsValues settingValuesInstance = new SettingsValues();
        public static SettingsValues Instance
        {
            get => settingValuesInstance;
        }
        private SettingsValues()
        {
            variableSeparator = new List<int>();
            List<int> settingCount = new List<int>() { 32, 2, 2, 1, 6, 5 }; //update this whenever adding/removing a variable {#EnemyStrength, #HealthChange, #Vision, #MinSwordHealth, #ItemDropRate, #Utilities}
            numVariables = 0;
            foreach (int val in settingCount)
            {
                numVariables += val;
                variableSeparator.Add(numVariables);
            }

            LoadVarToSetting();
            LoadModeToInt();
            LoadVarToListInt();
        }

        private void LoadVarToSetting()
        {
            varToSetting = new Dictionary<Variable, SettingsManager.Setting>();
            for (int i=0; i<numVariables; i++)
            {
                if (i < variableSeparator[0]) varToSetting.Add((Variable)i, SettingsManager.Setting.EnemyStrength);
                else if (i < variableSeparator[1]) varToSetting.Add((Variable)i, SettingsManager.Setting.HealthChange);
                else if (i < variableSeparator[2]) varToSetting.Add((Variable)i, SettingsManager.Setting.Vision);
                else if (i < variableSeparator[3]) varToSetting.Add((Variable)i, SettingsManager.Setting.MinProjectileSwordHealth);
                else if (i < variableSeparator[4]) varToSetting.Add((Variable)i, SettingsManager.Setting.ItemDropRate);
                else varToSetting.Add((Variable)i, SettingsManager.Setting.Utilities);
            }
        }
        private void LoadModeToInt()
        {
            modeToInt = new Dictionary<SettingsManager.Mode, int>();
            for (int i = 0; i < 3; i++) modeToInt.Add((SettingsManager.Mode)i, i);
        }
        private void LoadVarToListInt()
        {   // easy, normal, hard
            varToListInt = new Dictionary<Variable, List<int>>();
            varToListInt.Add(Variable.AquamentusHealth, new List<int>() { 4, 6, 10 });
            varToListInt.Add(Variable.AquamentusDamage, new List<int>() { 1, 2, 4 });
            varToListInt.Add(Variable.FireballDamage, new List<int>() { 1, 1, 3 });
            varToListInt.Add(Variable.GoriyaHealth, new List<int>() { 2, 3, 5 });
            varToListInt.Add(Variable.GoriyaDamage, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.GoriyaBoomerang, new List<int>() { 1, 2, 4 });
            varToListInt.Add(Variable.GelHealth, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.GelDamage, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.KeeseHealth, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.KeeseDamage, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.StalfosHealth, new List<int>() { 1, 2, 3 });
            varToListInt.Add(Variable.StalfosDamage, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.TrapDamage, new List<int>() { 1, 2, 3 });
            varToListInt.Add(Variable.WallMasterHealth, new List<int>() { 1, 2, 3 });
            varToListInt.Add(Variable.WallMasterDamage, new List<int>() { 1, 1, 3 });
            varToListInt.Add(Variable.HealthChange, new List<int>() { 1, 0, -1 });
            varToListInt.Add(Variable.HealthChangeDelay, new List<int>() { 900, 0, 3600 });
            varToListInt.Add(Variable.VisionRange, new List<int>() { 512, 72, 48 });
            varToListInt.Add(Variable.VisionRangeNoCandle, new List<int>() { 48, 32, 16 });
            varToListInt.Add(Variable.MinProjectileSwordHealth, new List<int>() { 0, -5, -1 });
            varToListInt.Add(Variable.ItemDropA, new List<int>() { 41, 31, 16 });
            varToListInt.Add(Variable.ItemDropB, new List<int>() { 59, 41, 31 });
            varToListInt.Add(Variable.ItemDropC, new List<int>() { 75, 59, 41 });
            varToListInt.Add(Variable.ItemDropD, new List<int>() { 59, 41, 31 });
            varToListInt.Add(Variable.ItemDropX, new List<int>() { 31, 9, 5 });
            varToListInt.Add(Variable.HealthIncrease, new List<int>() { 1, 0, 0 });
            varToListInt.Add(Variable.HealthDecrease, new List<int>() { 1, 0, 1 });
            varToListInt.Add(Variable.MaxHealthIncrease, new List<int>() { 2, 0, 0 });
            varToListInt.Add(Variable.MaxHealthDecrease, new List<int>() { 2, 0, 2 });
            varToListInt.Add(Variable.SpaceKillEnemies, new List<int>() { 1, 0, 0 });
            varToListInt.Add(Variable.GoriyaHardTexture, new List<int>() { 0, 0, 1 });
            varToListInt.Add(Variable.GoriyaEnemyType, new List<int>() { 1, 1, 3 });
            varToListInt.Add(Variable.MinLinkDamage, new List<int>() { 0, 1, 1 });
            varToListInt.Add(Variable.BoggusHealth, new List<int>() { 4, 6, 10 });
            varToListInt.Add(Variable.BoggusDamage, new List<int>() { 1, 2, 4 });
            varToListInt.Add(Variable.MarioHealth, new List<int>() { 5, 8, 12 });
            varToListInt.Add(Variable.MarioDamage, new List<int>() { 2, 4, 6 });
            varToListInt.Add(Variable.DodongoHealth, new List<int>() { 1, 4, 5 });
            varToListInt.Add(Variable.DodongoDamage, new List<int>() { 1, 2, 4 });
            varToListInt.Add(Variable.RopeHealth, new List<int>() { 1, 1, 4 });
            varToListInt.Add(Variable.RopeDamage, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.ZolHealth, new List<int>() { 1, 1, 2 });
            varToListInt.Add(Variable.ZolDamage, new List<int>() { 1, 2, 3 });
            varToListInt.Add(Variable.StalfosSpawnDelay, new List<int>() { 5, 3, 2 });
            varToListInt.Add(Variable.KeeseSpawnDelay, new List<int>() { 2, 1, 1 });
            varToListInt.Add(Variable.GelSpawnDelay, new List<int>() { 2, 1, 1 });
            varToListInt.Add(Variable.GoriyaSpawnDelay, new List<int>() { 6, 4, 3 });
            varToListInt.Add(Variable.RupeePickup, new List<int>() { 3, 1, 1 });
        }

        public int GetValue(Variable variable)
        {
            return varToListInt[variable][modeToInt[SettingsManager.Instance.Settings[varToSetting[variable]]]];
        }
    }

}