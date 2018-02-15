using System;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace Geometry
    {
        public class CopyMesh
        {
            private Mesh mesh;

            public CopyMesh(Mesh meshToCopy)
            {
                mesh = new Mesh();

                Vector3[] verts = new Vector3[meshToCopy.vertexCount];
                meshToCopy.vertices.CopyTo(verts, 0);
                mesh.vertices = verts;

                Int32[] tris = new Int32[meshToCopy.triangles.Length];
                meshToCopy.triangles.CopyTo(tris, 0);
                mesh.triangles = tris;

                Vector2[] uvs = new Vector2[meshToCopy.uv.Length];
                meshToCopy.uv.CopyTo(uvs, 0);
                mesh.uv = uvs;

                Vector2[] uv2s = new Vector2[meshToCopy.uv2.Length];
                meshToCopy.uv2.CopyTo(uv2s, 0);
                mesh.uv2 = uv2s;

                Vector3[] normals = new Vector3[meshToCopy.normals.Length];
                meshToCopy.normals.CopyTo(normals, 0);
                mesh.normals = normals;

                Vector4[] tangents = new Vector4[meshToCopy.tangents.Length];
                meshToCopy.tangents.CopyTo(tangents, 0);
                mesh.tangents = tangents;

                Color[] colors = new Color[meshToCopy.colors.Length];
                meshToCopy.colors.CopyTo(colors, 0);
                mesh.colors = colors;

                Color32[] colors32 = new Color32[meshToCopy.colors32.Length];
                meshToCopy.colors32.CopyTo(colors32, 0);
                mesh.colors32 = colors32;
            }

            public static implicit operator Mesh(CopyMesh copyMesh)
            {
                return copyMesh.mesh;
            }
        }
	}
}

