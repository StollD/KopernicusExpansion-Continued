using UnityEngine;

namespace KopernicusExpansion
{
    namespace Geometry
    {
        public class Teardrop
        {
            private Mesh mesh;

            public Teardrop(float radius, float oblongationFactor, int latitudeLines, int longitudeLines)
            {
                mesh = new UVSphere(radius, latitudeLines, longitudeLines);

                var verts = mesh.vertices;
                for (int i = 0; i < verts.Length; i++)
                {
                    var vert = verts[i];
                    if (vert.z < 0f)

                    {
                        //the extra multiplier makes the shape more triangular
                        float mult = vert.z * vert.z;
                        vert.z *= (oblongationFactor + (mult * oblongationFactor)) * 0.5f;
                    }
                    verts[i] = vert;
                }
                mesh.vertices = verts;
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();
                mesh.Optimize();
            }

            public static implicit operator Mesh(Teardrop teardrop)
            {
                return teardrop.mesh;
            }
        }
    }
    }