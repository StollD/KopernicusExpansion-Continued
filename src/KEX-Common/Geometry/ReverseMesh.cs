using UnityEngine;

namespace KopernicusExpansion
{
    namespace Geometry
    {
        public class ReverseMesh
        {
            private Mesh mesh;

            public ReverseMesh(Mesh meshToReverse)
            {
                mesh = meshToReverse;

                //reverse normals
                var normals = mesh.normals;
                for (int i = 0; i < normals.Length; i++)
                    normals[i] = -normals[i];
                mesh.normals = normals;

                //rewind the triangles
                for (int m = 0; m < mesh.subMeshCount; m++)
                {
                    int[] triangles = mesh.GetTriangles(m);
                    for (int i = 0; i < triangles.Length; i += 3)
                    {
                        int temp = triangles[i + 0];
                        triangles[i + 0] = triangles[i + 1];
                        triangles[i + 1] = temp;
                    }
                    mesh.SetTriangles(triangles, m);
                }
            }

            public static implicit operator Mesh(ReverseMesh reverseMesh)
            {
                return reverseMesh.mesh;
            }
        }
    }
}