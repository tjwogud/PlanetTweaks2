using HarmonyLib;
using PlanetTweaks2.UI;

namespace PlanetTweaks2.Patches
{
    [HarmonyPatch(typeof(Persistence), "SetPercentCompletion", typeof(int), typeof(float))]
    public class ProgressUpdatePatch
    {
        public static void Postfix()
        {
            UIAdapter.UpdateProgress();
        }
    }
}
