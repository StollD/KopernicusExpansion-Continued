using System;
using Kopernicus;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;
using UnityEngine;
using UnityEngine.Rendering;

namespace KopernicusExpansion
{
    namespace VertexHeightMap32
    {
        [RequireConfigType(Kopernicus.ConfigParser.Enumerations.ConfigType.Node)]
        public class VertexHeightMap16 : ModLoader<PQSMod_VertexHeightMap16>
        {    
            // The map texture for the planet
            [ParserTarget("map")]
            public MapSOParserRGB<MapSO> HeightMap
            {
                get { return Mod.heightMap; }
                set { Mod.heightMap = value; }
            }

            // Height map offset
            [ParserTarget("offset")]
            public NumericParser<Double> HeightMapOffset 
            {
                get { return Mod.heightMapOffset; }
                set { Mod.heightMapOffset = value; }
            }

            // Height map deformity
            [ParserTarget("deformity")]
            public NumericParser<Double> HeightMapDeformity
            {
                get { return Mod.heightMapDeformity; }
                set { Mod.heightMapDeformity = value; }
            }

            [ParserTarget("scaleDeformityByRadius")]
            public NumericParser<Boolean> ScaleDeformityByRadius
            {
                get { return Mod.scaleDeformityByRadius; }
                set { Mod.scaleDeformityByRadius = value; }
            }

            private static float SingleSample(Int32 x, Int32 y, MapSO heightMap, bool bits24)
            {
                // Get the Color, not the Float-Value from the Map
                Color32 c = heightMap.GetPixelColor32(x, y);

                // Get the height data from the terrain
                float height = 0;
                if (bits24)
                {
                    height = (float)((int)c.b | ((int)c.g << 8) | ((int)c.r << 8)) / (float)0x00FFFFFF;
                }
                else
                {
                    height = (float)((int)c.b | ((int)c.g << 8)) / (float)0xFFFF;
                }

                return height;
            }

            public static float SampleHeightmap16(Double u, Double v, MapSO heightMap, bool bits24)
            {
                if(heightMap == null || !heightMap.IsCompiled) return 0;
                BilinearCoords coords = VertexHeightMap16.ConstructBilinearCoords(u, v, heightMap);
                return Mathf.Lerp(
                    Mathf.Lerp(
                        SingleSample(coords.xFloor, coords.yFloor, heightMap, bits24),
                        SingleSample(coords.xCeiling, coords.yFloor, heightMap, bits24),
                        coords.u),
                    Mathf.Lerp(
                        SingleSample(coords.xFloor, coords.yCeiling, heightMap, bits24),
                        SingleSample(coords.xCeiling, coords.yCeiling, heightMap, bits24),
                        coords.u),
                    coords.v);
            }

            // Function taken from https://github.com/Kopernicus/pqsmods-standalone/blob/master/KSP/MapSO.cs L340
            public static BilinearCoords ConstructBilinearCoords(Double x, Double y, MapSO heightMap)
            {
                // Create the struct
                BilinearCoords coords = new BilinearCoords();

                // Floor
                x = x - Math.Truncate(x);
                y = y - Math.Truncate(y);
                if(x < 0) x = 1.0 + x;
                if (y < 0) y = -y;

                // X to U
                coords.x = x * heightMap.Width;
                if (coords.x >= heightMap.Width) coords.x -= heightMap.Width;
                coords.xFloor = (Int32)Math.Floor(coords.x);
                coords.xCeiling = (Int32)Math.Ceiling(coords.x);
                coords.u = (Single)(coords.x - Math.Truncate(coords.x));
                if (coords.xCeiling >= heightMap.Width) coords.xCeiling -= heightMap.Width;

                // Y to V
                coords.y = y * heightMap.Height;
                if (coords.y >= heightMap.Height) coords.y = heightMap.Height - 1 - (coords.y - heightMap.Height);
                coords.yFloor = (Int32)Math.Floor(coords.y);
                coords.yCeiling = (Int32)Math.Ceiling(coords.y);
                coords.v = (Single)(coords.y - Math.Truncate(coords.y));
                if (coords.yCeiling >= heightMap.Height) coords.yCeiling = heightMap.Height - 1 - (coords.yCeiling - heightMap.Height);

                // We're done
                return coords;
            }

            public struct BilinearCoords
            {
                public Double x, y;
                public Int32 xCeiling, xFloor, yCeiling, yFloor;
                public Single u, v;
            }
        }
    }
}
