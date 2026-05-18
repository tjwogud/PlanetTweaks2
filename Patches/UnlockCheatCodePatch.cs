using HarmonyLib;
using PlanetTweaks2.UI;

namespace PlanetTweaks2.Patches
{
    [HarmonyPatch(typeof(PlanetarySystem), "RainbowMode")]
    public class UnlockCheatCodePatch1
    {
        public static void Postfix()
        {
            Main.Settings.rainbowCode = true;
            UIAdapter.UpdateCheatCode();
        }
    }

    [HarmonyPatch(typeof(PlanetarySystem), "SamuraiMode")]
    public class UnlockCheatCodePatch2
    {
        public static void Postfix()
        {
            Main.Settings.samuraiCode = true;
            UIAdapter.UpdateCheatCode();
        }
    }
}
