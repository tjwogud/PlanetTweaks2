using PlanetTweaks2.UI;
using UnityEngine;
using static CameraFilterPack_NightVisionFX;

namespace PlanetTweaks2.Components
{
    public class PlanetController : MonoBehaviour
    {
        private static readonly PlanetController[] planetControllers = new PlanetController[3];

        public scrPlanet TargetPlanet { get; private set; }
        public int Type { get; private set; }
        public PlanetRenderer Renderer => TargetPlanet.planetRenderer;

        private PlanetImage ptImage;
        private SpriteRenderer image;
        private FixRotation fixRot;

        public static PlanetController Get(int type)
        {
            var controller = planetControllers[type];
            if (controller)
                return controller;

            var planetarySystem = scrController.instance?.planetarySystem;
            if (!planetarySystem)
                return null;

            return Get(planetarySystem.allPlanets[type], type);
        }

        public static PlanetController Get(scrPlanet targetPlanet, int type = -1)
        {
            if (targetPlanet.GetComponent<scrObjectDecoration>())
                return null;

            var controller = targetPlanet.gameObject.GetComponent<PlanetController>();
            if (controller)
                return controller;

            if (type == -1) {
                var planetarySystem = scrController.instance?.planetarySystem;
                if (!planetarySystem) // what???
                    return null;

                type = planetarySystem.allPlanets.IndexOf(targetPlanet);
                if (type < 0 || type >= 3)
                    return null;
            }

            controller = targetPlanet.gameObject.AddComponent<PlanetController>();
            controller.TargetPlanet = targetPlanet;
            controller.Type = type;

            var imageParent = new GameObject("PlanetImageParent");
            imageParent.transform.SetParent(targetPlanet.transform, false);
            controller.fixRot = imageParent.AddComponent<FixRotation>();

            var image = new GameObject("PlanetImage");
            image.transform.SetParent(imageParent.transform, false);
            var renderer = image.AddComponent<SpriteRenderer>();
            renderer.sortingOrder = targetPlanet.planetRenderer.sprite.meshRenderer.sortingOrder + 5;
            renderer.sortingLayerID = targetPlanet.planetRenderer.sprite.meshRenderer.sortingLayerID;
            renderer.sortingLayerName = targetPlanet.planetRenderer.sprite.meshRenderer.sortingLayerName;
            controller.image = renderer;

            controller.UpdateValue();

            planetControllers[type] = controller;

            return controller;
        }

        public void UpdateValue()
        {
            Renderer.SetRainbow(false);
            if (Main.Settings.GetSamurai(Type))
            {
                Renderer.SetEmojiMode(false);
                Renderer.ToggleSamurai(true, true /* this value is actually not important */);
            }
            else
            {
                Renderer.ToggleSamurai(false, true);
                Renderer.SetEmojiMode(Main.Settings.GetEmoji(Type), false);
            }
            PlanetColor planetColor = Main.Settings.GetPlanetColor(Type);
            var planetAlpha = Main.Settings.planetAlphaArr[Type];
            if (planetColor.preset == PlanetColorPreset.Gold || GCS.d_forceGoldPlanets /* what is this? */)
            {
                Renderer.DisableAllSpecialPlanets();
                Renderer.SwitchToGold();
            }
            else if (planetColor.preset == PlanetColorPreset.Overseer)
            {
                Renderer.DisableAllSpecialPlanets();
                Renderer.SwitchToOverseer();
            }
            else if (planetColor.preset == PlanetColorPreset.Rainbow)
            {
                Renderer.EnableCustomColor();
                Renderer.SetRainbow(true);
            }
            else
            {
                var preset = planetColor.preset;
                if (preset == PlanetColorPreset.DefaultRed || preset == PlanetColorPreset.DefaultBlue)
                {
                    int defaultColor = (preset == PlanetColorPreset.DefaultRed) ? 0 : 1;
                    Renderer.EnableDefaultFireAndIceColor(true /* this one is also not important */, defaultColor);
                    Renderer.sprite.color = Color.white.WithAlpha(planetAlpha);
                    Renderer.SetCoreColor(planetColor.ToRealColor().WithAlpha(planetAlpha));
                    Renderer.faceSprite.color = planetColor.ToRealColor().WithAlpha(planetAlpha);
                }
                else
                {
                    Renderer.EnableCustomColor();
                    Renderer.SetPlanetColor(planetColor.ToRealColor().WithAlpha(planetAlpha));
                }

                var tailColor = Main.Settings.tailColorArr[Type];
                if (tailColor.preset == PlanetColorPresetEx.Disable) tailColor = planetColor;
                var tailAlpha = Main.Settings.tailAlphaArr[Type];
                Renderer.SetTailColor(tailColor.ToRealColor().WithAlpha(tailAlpha));

                var ringColor = Main.Settings.ringColorArr[Type];
                if (ringColor.preset == PlanetColorPresetEx.Disable) ringColor = planetColor;
                var ringAlpha = Main.Settings.ringAlphaArr[Type];
                Renderer.ringComp.color = ringColor.ToRealColor().WithAlpha(ringAlpha);
            }
            if (scrLogoText.instance)
                scrLogoText.instance.UpdateColors();

            ptImage = Main.Settings.imageArr[Type];
            if (ptImage == null)
            {
                image.enabled = false;
                return;
            }
            image.enabled = true;
            image.transform.localPosition = Main.Settings.imagePositionArr[Type] / 100;
            image.transform.localScale = Main.Settings.imageSizeArr[Type] / 100;
            var x = image.transform.localScale.x * 512 / ptImage.GetImage().rect.width * .8f;
            var y = image.transform.localScale.y * 512 / ptImage.GetImage().rect.height * .8f;
            image.transform.localScale = new Vector2(x, y);
            fixRot.enabled = Main.Settings.imageFixRotationArr[Type];
            fixRot.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void LateUpdate()
        {
            if (ptImage == null)
                return;
            var current = ptImage.GetImage();
            if (image.sprite != current)
                image.sprite = current;
            var planetEnabled = TargetPlanet.planetRenderer.sprite.meshRenderer.enabled;
            if (image.enabled != planetEnabled)
                image.enabled = planetEnabled;
        }
    }
}
