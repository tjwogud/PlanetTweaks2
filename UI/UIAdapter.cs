using PlanetTweaks2.Components;
using UnityEngine;

namespace PlanetTweaks2.UI
{
    public class UIAdapter
    {
        public static PlanetTweaks2UI UI => PlanetTweaks2UI.Instance;

        public static void Init(string bundlePath)
        {
            var bundle = AssetBundle.LoadFromFile(bundlePath);
            var ui = bundle.LoadAsset<GameObject>("PlanetTweaks2UI");
            var instance = Object.Instantiate(ui);
            instance.name = "PlanetTweaks2UI";
            UI.Init(GetValue, SetValue);
            UI.Toggle(false);

            UpdateProgress();
            UpdateCheatCode();
        }

        public static void UpdateProgress()
        {
            UI.UpdateProgress(Persistence.GetOverallProgressStage() >= 8, Persistence.IsWorldComplete("T5"), Persistence.IsWorldComplete(60));
        }

        public static void UpdateCheatCode()
        {
            UI.UpdateCheatCode(Main.Settings.rainbowCode, Main.Settings.samuraiCode);
        }

        private static object GetValue(Keys key)
        {
            var current = UI.current;
            switch (key)
            {
                case Keys.PlanetColor:
                    return Main.Settings.GetPlanetColor(current);
                case Keys.PlanetAlpha:
                    return Main.Settings.planetAlphaArr[current];
                case Keys.TailColor:
                    return Main.Settings.tailColorArr[current];
                case Keys.TailAlpha:
                    return Main.Settings.tailAlphaArr[current];
                case Keys.RingColor:
                    return Main.Settings.ringColorArr[current];
                case Keys.RingAlpha:
                    return Main.Settings.ringAlphaArr[current];
                case Keys.Samurai:
                    return false;
                case Keys.Emoji:
                    return false;
                case Keys.Image:
                    return Main.Settings.imageArr[current];
                case Keys.ImagePosition:
                    return Main.Settings.imagePositionArr[current];
                case Keys.ImageSize:
                    return Main.Settings.imageSizeArr[current];
                case Keys.ImageFixRotation:
                    return Main.Settings.imageFixRotationArr[current];
            }
            return null;
        }

        private static void SetValue(Keys key, object value)
        {
            Main.Logger.Log($"{key} : {value}");
            var current = UI.current;
            switch (key)
            {
                case Keys.PlanetColor:
                    Main.Settings.SetPlanetColor(current, (Color)value);
                    break;
                case Keys.PlanetAlpha:
                    Main.Settings.planetAlphaArr[current] = (float)value;
                    break;
                case Keys.TailColor:
                    Main.Settings.tailColorArr[current] = (Color)value;
                    break;
                case Keys.TailAlpha:
                    Main.Settings.tailAlphaArr[current] = (float)value;
                    break;
                case Keys.RingColor:
                    Main.Settings.ringColorArr[current] = (Color)value;
                    break;
                case Keys.RingAlpha:
                    Main.Settings.ringAlphaArr[current] = (float)value;
                    break;
                case Keys.Samurai:
                    Main.Settings.SetSamurai(current, (bool)value);
                    break;
                case Keys.Emoji:
                    Main.Settings.SetEmoji(current, (bool)value);
                    break;
                case Keys.Image:
                    Main.Settings.imageArr[current] = (PlanetImage)value;
                    break;
                case Keys.ImagePosition:
                    Main.Settings.imagePositionArr[current] = (Vector2)value;
                    break;
                case Keys.ImageSize:
                    Main.Settings.imageSizeArr[current] = (Vector2)value;
                    break;
                case Keys.ImageFixRotation:
                    Main.Settings.imageFixRotationArr[current] = (bool)value;
                    break;
            }

            var controller = PlanetController.Get(current);
            if (controller)
                controller.UpdateValue();
        }
    }
}
