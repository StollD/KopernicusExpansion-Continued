using LibNoise;
using System;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        /// <summary>
        /// A mod that applies a dynamic noise configuration to the terrain
        /// </summary>
        public class PQSMod_ModularNoise : SerializedPQSMod
        {
            public Double deformity;
            public Boolean normalizeHeight = true;
            public IModule noise
            {
                get { return GetProperty<IModule>("noise"); }
                set { SetProperty<IModule>("noise", value); }
            }

            public override void OnVertexBuildHeight(PQS.VertexBuildData data)
            {
                data.vertHeight += noise.GetValue(data.directionFromCenter * (normalizeHeight ? 1 : sphere.radius)) * deformity;
            }
        }
    }
}