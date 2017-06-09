using UnityEngine;

namespace KopernicusExpansion
{
    namespace EmissiveFX
    {
        public class PQSMod_EmissiveFX : PQSMod
        {
            // The material the mod will apply to the surface
            public Material EmissiveMaterial;

            public override void OnPostSetup()
            {
                // Force the PQS to share the surface material
                sphere.useSharedMaterial = true;
            }

            public override void OnQuadBuilt(PQ quad)
            {
                if (quad.sphereRoot != sphere)
                    return;

                Material surfaceMaterial = quad.meshRenderer.sharedMaterial;

                // Take a copy of the emissive material
                Material emissiveMaterial = new Material(EmissiveMaterial);
                emissiveMaterial.renderQueue = surfaceMaterial.renderQueue + 10;
                quad.meshRenderer.sharedMaterials = new Material[] { surfaceMaterial, emissiveMaterial };
            }

            // Remove extra material at end of quad's life to prevent issues
            public override void OnQuadDestroy(PQ quad)
            {
                Material[] mats = quad.meshRenderer.sharedMaterials;
                Material[] newMats = new Material[] { mats[0] };
                quad.meshRenderer.sharedMaterials = newMats;
            }
        }
    }
}