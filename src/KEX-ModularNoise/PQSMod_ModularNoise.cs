using LibNoise;
using System;
using Kopernicus.Components;
using Kopernicus.Components.Serialization;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        /// <summary>
        /// A mod that applies a dynamic noise configuration to the terrain
        /// </summary>
        public class PQSMod_ModularNoise : SerializablePQSMod
        {
            public Double deformity;
            public Boolean normalizeHeight = true;
            
            [SerializeField] 
            public IModule noise;

            public override void OnVertexBuildHeight(PQS.VertexBuildData data)
            {
                data.vertHeight += noise.GetValue(data.directionFromCenter * (normalizeHeight ? 1 : sphere.radius)) * deformity;
            }
        }
    }
}