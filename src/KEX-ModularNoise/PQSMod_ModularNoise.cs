using LibNoise;
using System;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        /// <summary>
        /// A mod that applies a dynamic noise configuration to the terrain
        /// </summary>
        public class PQSMod_ModularNoise : PQSMod
        {
            public Double deformity;
            public IModule noise;
            public Boolean normalizeHeight = true;

            public override void OnVertexBuildHeight(PQS.VertexBuildData data)
            {
                data.vertHeight += noise.GetValue(data.directionFromCenter * (normalizeHeight ? 1 : sphere.radius)) * deformity;
            }
        }
    }
}