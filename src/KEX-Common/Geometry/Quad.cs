using System;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace Geometry
    {
        public class Quad
        {
            private Mesh mesh;

            //constructor
            public Quad(Single xSize, Single zSize, Boolean centered = false)
            {
                mesh = new Mesh();

                var vertices = new Vector3[4];
                if (centered)
                {
                    var x2 = xSize / 2;
                    var z2 = zSize / 2;
                    vertices[0] = new Vector3(-x2, 0f, -z2);
                    vertices[1] = new Vector3(-x2, 0f, z2);
                    vertices[2] = new Vector3(x2, 0f, z2);
                    vertices[3] = new Vector3(x2, 0f, -z2);
                }
                else
                {
                    vertices[0] = new Vector3(0f, 0f, 0f);
                    vertices[1] = new Vector3(0f, 0f, zSize);
                    vertices[2] = new Vector3(xSize, 0f, zSize);
                    vertices[3] = new Vector3(xSize, 0f, 0f);
                }

                var triangles = new Int32[6];
                triangles[0] = 0;
                triangles[1] = 1;
                triangles[2] = 2;
                triangles[3] = 2;
                triangles[4] = 3;
                triangles[5] = 0;

                var uvs = new Vector2[4];
                uvs[0] = new Vector2(0f, 0f);
                uvs[1] = new Vector2(0f, 1f);
                uvs[2] = new Vector2(1f, 1f);
                uvs[3] = new Vector2(1f, 0f);

                var normals = new Vector3[4];
                normals[0] = Vector3.up;
                normals[1] = Vector3.up;
                normals[2] = Vector3.up;
                normals[3] = Vector3.up;

                mesh.vertices = vertices;
                mesh.triangles = triangles;
                mesh.uv = uvs;
                mesh.normals = normals;
            }

            public static implicit operator Mesh(Quad quad)
            {
                return quad.mesh;
            }
        }
    }
}