using HarmonyLib;
using PlanetTweaks2.Components;
using System.Collections.Generic;
using System.Reflection;

namespace PlanetTweaks2.Patches
{
    [HarmonyPatch(typeof(PlanetarySystem), "ColorPlanets")]
    public class PlanetUpdatePatch1
    {
        public static void Postfix()
        {
            for (int i = 0; i < 3; i++)
                PlanetController.Get(i).UpdateValue();
        }
    }

    public class PlanetUpdatePatch2
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(scrColorPlanet), "StartEffect");
            yield return AccessTools.Method(typeof(scrSetEmojiMode), "StartEffect");
        }

        public static void Postfix()
        {
            for (int i = 0; i < 3; i++)
                PlanetController.Get(i).UpdateValue();
        }
    }
}
