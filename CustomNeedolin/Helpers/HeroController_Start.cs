using CustomNeedolin.Settings;
using HarmonyLib;

namespace CustomNeedolin.Helpers
{
    [HarmonyPatch(typeof(HeroController), "Start")]
    public static class HeroController_Start
    {
        [HarmonyPostfix]
        public static void Postfix(HeroController __instance)
        {
            //CustomNeedolin.Instance.Log($"HC started. Changing needolin: '{ConfigSettings.fileName.Value}'");
            NeedolinHelper.ChangeNeedolin(ConfigSettings.fileName.Value);
        }
    }
}