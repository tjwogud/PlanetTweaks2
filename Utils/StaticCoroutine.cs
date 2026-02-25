using System.Collections;
using UnityEngine;

namespace PlanetTweaks2.Utils
{
    public class StaticCoroutine
    {
        private static Runner runner;

        public static void Do(IEnumerator coroutine)
        {
            if (!runner)
            {
                runner = new GameObject("StaticCoroutine").AddComponent<Runner>();
                Object.DontDestroyOnLoad(runner.gameObject);
            }
            runner.StartCoroutine(coroutine);
        }

        private class Runner : MonoBehaviour
        {
        }
    }
}
