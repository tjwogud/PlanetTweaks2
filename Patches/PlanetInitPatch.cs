using HarmonyLib;
using PlanetTweaks2.Components;

namespace PlanetTweaks2.Patches
{
    [HarmonyPatch(typeof(PlanetarySystem), "Init")]
    public class PlanetInitPatch
    {
        public static void Postfix()
        {
            for (int i = 0; i < 3; i++)
                PlanetController.Get(i);
        }
    }
}
