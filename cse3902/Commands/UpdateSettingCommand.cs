using cse3902.Interfaces;
using System.Collections.Generic;

namespace cse3902.Commands
{
    public class UpdateSettingCommand : ICommand
    {
        private bool pressed;
        private List<SettingsManager.Setting> settings;

        public UpdateSettingCommand(List<SettingsManager.Setting> settingsList)
        {
            pressed = false;
            settings = settingsList;
        }

        public void Execute(int id)
        {
            if (pressed) return;
            pressed = true;

            id = id % 6;
            SettingsManager.Instance.UpdateSetting(settings[id]);
        }

        public void Unexecute()
        {
            pressed = false;
        }
    }
}