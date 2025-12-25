using DanielSteginkUtils.Helpers;
using HarmonyLib;
using HutongGames.PlayMaker.Actions;

namespace CustomNeedolin.Helpers
{
    [HarmonyPatch(typeof(StartNeedolinAudioLoop), "OnEnter")]
    public static class StartNeedolinAudioLoop_OnEnter
    {
        [HarmonyPrefix]
        public static void Prefix(StartNeedolinAudioLoop __instance)
        {
            // Verify we have the right state
            // Normally I would check the FSM name, but apparently its just "FSM"
            if (!__instance.State.Name.Equals("Start Needolin Proper"))
            {
                return;
            }

            Needolin.ResetAudioClip(__instance.Fsm, true);
        }
    }
}
