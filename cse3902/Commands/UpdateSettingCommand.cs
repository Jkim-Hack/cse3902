using cse3902.Interfaces;
using System.Collections.Generic;

namespace cse3902.Commands
{
    public class UpdateSettingCommand : ICommand
    {
        private int pressed;
        private List<SettingsManager.Setting> settings;

        public UpdateSettingCommand(List<SettingsManager.Setting> settingsList)
        {
            pressed = 0;
            settings = settingsList;
        }

        public void Execute(int id)
        {
            if (pressed > 0)
            {
                pressed--;
                return;
            }
            pressed = 7;

            id = id % 6;
            SettingsManager.Instance.UpdateSetting(settings[id]);
        }

        public void Unexecute()
        {
        }
    }
}