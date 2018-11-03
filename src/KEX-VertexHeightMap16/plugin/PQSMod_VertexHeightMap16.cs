using System;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace VertexHeightMap32
    {
        /// <summary>
        /// A heightmap PQSMod that can parse encoded 16bpp textures
        /// </summary>
        public class PQSMod_VertexHeightMap16 : PQSMod_VertexHeightMap
        {
            public override void OnVertexBuildHeight(PQS.VertexBuildData data)
            {
                // Get the Color, not the Float-Value from the Map
                Color32 c = heightMap.GetPixelColor32(data.u, data.v);
                
                // Get the height data from the terrain
                Double height = BitConverter.ToUInt16(new [] {c.g, c.a}, 0) / (Double) UInt16.MaxValue;
                
                // Apply it
                data.vertHeight += heightMapOffset + heightMapDeformity * height;
            }
        }
    }
}