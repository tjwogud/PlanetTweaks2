using PlanetTweaks2.UI;
using UnityEngine;

namespace PlanetTweaks2.Utils
{
    public static class PlanetColorUtils
    {
        public static PlanetColor ToAdofai(this SimplePlanetColor color)
        {
            if (color.isSpecial)
            {
                if (color == SimplePlanetColor.DefaultRed)
                    return new PlanetColor(PlanetColorPreset.DefaultRed);
                else if (color == SimplePlanetColor.DefaultBlue)
                    return new PlanetColor(PlanetColorPreset.DefaultBlue);
                else if (color == SimplePlanetColor.Gold)
                    return new PlanetColor(PlanetColorPreset.Gold);
                else if (color == SimplePlanetColor.Rainbow)
                    return new PlanetColor(PlanetColorPreset.Rainbow);
                else if (color == SimplePlanetColor.Overseer)
                    return new PlanetColor(PlanetColorPreset.Overseer);
                else if (color == SimplePlanetColor.Disable)
                    return new PlanetColor(PlanetColorPresetEx.Disable);
            }
            return new PlanetColor(color.color);
        }

        public static SimplePlanetColor ToSimple(this PlanetColor color)
        {
            switch (color.preset)
            {
                case PlanetColorPreset.DefaultRed:
                    return SimplePlanetColor.DefaultRed;
                case PlanetColorPreset.DefaultBlue:
                    return SimplePlanetColor.DefaultBlue;
                case PlanetColorPreset.Gold:
                    return SimplePlanetColor.Gold;
                case PlanetColorPreset.Rainbow:
                    return SimplePlanetColor.Rainbow;
                case PlanetColorPreset.Overseer:
                    return SimplePlanetColor.Overseer;
                default:
                    if (color.preset == PlanetColorPresetEx.Disable)
                        return SimplePlanetColor.Disable;
                    return new SimplePlanetColor(color.ToRealColor());
            }
        }
    }
}
