using PlanetTweaks2.UI;
using PlanetTweaks2.Utils;
using UnityEngine;

namespace PlanetTweaks2
{
    public partial class Settings
    {
        public PlanetColor GetPlanetColor(int type)
        {
            return type < 2
                ? Persistence.GetPlayerColor(type == 0)
                : thirdPlanetColor;
        }

        public void SetPlanetColor(int type, PlanetColor color)
        {
            if (type < 2)
                Persistence.SetPlayerColor(color, type == 0);
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

        public SystemLanguage GetLanguage()
        {
            if (language == SystemLanguage.Unknown)
            {
                return RDString.language == SystemLanguage.Korean ? SystemLanguage.Korean : SystemLanguage.English;
            }
            return language;
        }
    }
}
