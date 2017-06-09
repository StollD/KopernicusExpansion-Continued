using Kopernicus.Components;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace EmissiveFX
    {
        [KSPAddon(KSPAddon.Startup.Instantly, true)]
        public class ShaderInit : MonoBehaviour
        {
            void Awake()
            {
                ShaderLoader.LoadAssetBundle("KopernicusExpansion/Shaders", "emissivefx");
            }
        }
    }
}