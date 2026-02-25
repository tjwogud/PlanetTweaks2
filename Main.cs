using HarmonyLib;
using PlanetTweaks2.UI;
using PlanetTweaks2.Utils;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;

namespace PlanetTweaks2
{
    public static class Main
    {
        public static UnityModManager.ModEntry ModEntry;
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony Harmony;
        public static Settings Settings;

        public static void Setup(UnityModManager.ModEntry modEntry)
        {
            ModEntry = modEntry;
            Logger = modEntry.Logger;
            Settings = Settings.Load(modEntry.Path);
            modEntry.OnToggle = OnToggle;
            modEntry.OnUpdate = OnUpdate;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;

            StaticCoroutine.Do(InitAfterLoad(modEntry));
        }

        private static IEnumerator InitAfterLoad(UnityModManager.ModEntry modEntry)
        {
            yield return new WaitUntil(() => PlayerPrefsJson.nonSyncedKeys.Count > 0);
            UIAdapter.Init(Path.Combine(modEntry.Path, "planettweaks2ui"));
        }

        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            if (value)
            {
                Harmony = new Harmony(modEntry.Info.Id);
                Harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                Logger.Warning("이 모드는 게임 중 꺼지는걸 상정하지 않고 만들어졌습니다.");
                Logger.Warning("모드를 완전히 끄기 위해 게임을 종료 후 다시 시작하세요.");
                Logger.Warning("This mod is not intended to be turned off while playing!");
                Logger.Warning("Restart the game in order to completely turn this mod off.");
                Harmony.UnpatchAll(modEntry.Info.Id);
            }
            return true;
        }

        public static void OnUpdate(UnityModManager.ModEntry modEntry, float f)
        {
            if ((!Settings.shortcutCtrl  || Input.GetKey(KeyCode.LeftControl))
             && (!Settings.shortcutAlt   || Input.GetKey(KeyCode.LeftAlt))
             && (!Settings.shortcutShift || Input.GetKey(KeyCode.LeftShift))
             && Input.GetKeyDown(Settings.shortcutKey))
            {
                PlanetTweaks2UI.Instance.Toggle(!PlanetTweaks2UI.Instance.gameObject.activeSelf);
            }
        }

        private static bool waiting;

        public static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.Label("Shortcut");

            GUILayout.BeginHorizontal();
            Settings.shortcutCtrl = GUILayout.Toggle(Settings.shortcutCtrl, "Ctrl");
            GUILayout.Label(" + ");
            Settings.shortcutAlt = GUILayout.Toggle(Settings.shortcutAlt, "Alt");
            GUILayout.Label(" + ");
            Settings.shortcutShift = GUILayout.Toggle(Settings.shortcutShift, "Shift");
            GUILayout.Label(" + ");
            if (waiting)
            {
                var e = Event.current;
                if (e.isKey && e.type == EventType.KeyDown)
                {
                    Settings.shortcutKey = e.keyCode;
                    waiting = false;
                }
            }
            else if (GUILayout.Button($"{Settings.shortcutKey}"))
            {
                waiting = true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry.Path);
            Logger.Log("Saved Settings");
            Persistence.Save(true); // uhh... whynot
        }
    }
}
