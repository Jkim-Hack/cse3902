using cse3902.Interfaces;
using System.Collections.Generic;
using cse3902.Constants;

namespace cse3902.Commands
{
    public class UpdateSettingCommand : ICommand
    {
        private List<SettingsManager.Setting> settings;

        public UpdateSettingCommand(List<SettingsManager.Setting> settingsList)
        {
            settings = settingsList;
        }

        public void Execute(int id)
        {
            id = id % CommandConstants.UpdateSettingCommandCount;
            SettingsManager.Instance.UpdateSetting(settings[id]);
        }

        public void Unexecute()
        {
        }
    }
}