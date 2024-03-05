using Phonebook;
using Phonebook.Services;

bool settingsExist = SettingsService.CheckSettings();
if (!settingsExist)
{
    SettingsService.SettingsPrompt();
}
UserInterface.MainMenu();

