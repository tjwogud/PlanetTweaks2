using PlanetTweaks2.UI;
using UnityEngine;

namespace PlanetTweaks2
{
    public partial class Settings
    {
        public Color GetPlanetColor(int type)
        {
            if (type < 2)
            {
                var color = Persistence.GetPlayerColor(type == 0);
                if (color == PlanetRenderer.goldColor)
                    return Colors.goldColor;
                else if (color == PlanetRenderer.rainbowColor)
                    return Colors.rainbowColor;
                else if (color == PlanetRenderer.overseerColor)
                    return Colors.overseerColor;
                return scrMisc.PlayerColorToRealColor(color);
            }
            return thirdPlanetColor;
        }

        public void SetPlanetColor(int type, Color color)
        {
            if (type < 2)
            {
                var converted = color;
                if (converted == Colors.goldColor)
                    converted = PlanetRenderer.goldColor;
                else if (converted == Colors.rainbowColor)
                    converted = PlanetRenderer.rainbowColor;
                else if (converted == Colors.overseerColor)
                    converted = PlanetRenderer.overseerColor;
                Persistence.SetPlayerColor(converted, type == 0);
            }
            else
                thirdPlanetColor = color;
        }

        public bool GetSamurai(int type)
        {
            if (type < 2)
                return Persistence.GetSamuraiMode(type == 0);
            return thirdSamurai;
        }

        public void SetSamurai(int type, bool value)
        {
            if (type < 2)
                Persistence.SetSamuraiMode(value, type == 0);
            else
                thirdSamurai = value;
        }

        public bool GetEmoji(int type)
        {
            if (type < 2)
                return Persistence.GetEmojiMode(type == 0);
            return thirdEmoji;
        }

        public void SetEmoji(int type, bool value)
        {
            if (type < 2)
                Persistence.SetEmojiMode(value, type == 0);
            else
                thirdEmoji = value;
        }
    }
}
