using UnityEngine;

namespace PlanetTweaks2.Components
{
    public class FixRotation : MonoBehaviour
    {
        private void Update()
        {
            var z = ADOBase.controller.camy.transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, z);
        }
    }
}
