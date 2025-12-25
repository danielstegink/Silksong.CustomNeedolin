using BepInEx.Configuration;
using CustomNeedolin.Helpers;
using TeamCherry.Localization;

namespace CustomNeedolin.Settings
{
    public static class ConfigSettings
    {
        /// <summary>
        /// Integrates with UI to set audio clip file name
        /// </summary>
        public static ConfigEntry<string> fileName;

        /// <summary>
        /// Initializes the settings
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(ConfigFile config)
        {
            // Bind set methods to Config
            LocalisedString name = new LocalisedString($"Mods.{CustomNeedolin.Id}", "NAME");
            LocalisedString description = new LocalisedString($"Mods.{CustomNeedolin.Id}", "DESC");
            if (name.Exists &&
                description.Exists)
            {
                fileName = config.Bind("Modifier", name, "", description);
            }
            else
            {
                fileName = config.Bind("Modifier", "Audio File", "", "Name of the audio file to play with the Needolin");
            }

            fileName.SettingChanged += OnSettingChanged;
        }

        /// <summary>
        /// When the designated file changes, reset the needolin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnSettingChanged(object sender, System.EventArgs e)
        {
            //CustomNeedolin.Instance.Log($"Needolin setting changed: '{fileName.Value}'");
            NeedolinHelper.ChangeNeedolin(fileName.Value);
        }
    }
}
