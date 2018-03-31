using System;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace Geometry
    {
        public class Teardrop
        {
            private Mesh mesh;

            public Teardrop(Single radius, Single oblongationFactor, Int32 latitudeLines, Int32 longitudeLines)
            {
                mesh = new UVSphere(radius, latitudeLines, longitudeLines);

                var verts = mesh.vertices;
                for (Int32 i = 0; i < verts.Length; i++)
                {
                    var vert = verts[i];
                    if (vert.z < 0f)

                    {
                        //the extra multiplier makes the shape more triangular
                        Single mult = vert.z * vert.z;
                        vert.z *= (oblongationFactor + (mult * oblongationFactor)) * 0.5f;
                    }
                    verts[i] = vert;
                }
                mesh.vertices = verts;
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();
            }

            public static implicit operator Mesh(Teardrop teardrop)
            {
                return teardrop.mesh;
            }
        }
    }
    }