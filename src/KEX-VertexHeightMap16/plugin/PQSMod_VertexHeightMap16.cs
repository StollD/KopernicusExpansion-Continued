using Kopernicus.Components;
using Kopernicus.Configuration;
using Kopernicus.Constants;
using System;
using System.Collections.Generic;
using Kopernicus.Configuration.Parsing;
using UnityEngine;
using UnityEngine.Rendering;

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

                //Debug.Log("[KopEx VertexHeightMap16] " + data.u + ", " + data.v + ", " + c);
                
                // Get the height data from the terrain
                Double height = (Double) BitConverter.ToUInt16(new byte[] {c.b, c.g}, 0) / (Double) UInt16.MaxValue;

                //Debug.Log("[KopEx VertexHeightMap16] " + height + ", " + c.g + ", " + c.a + ", " + BitConverter.ToUInt16(new byte[] { c.g, c.a }, 0));
                //Debug.Log("[KopEx VertexHeightMap16] " + heightMapOffset + ", " + heightMapDeformity);

                // Apply it
                data.vertHeight += heightMapOffset + heightMapDeformity * height;
            }
        }
    }
}