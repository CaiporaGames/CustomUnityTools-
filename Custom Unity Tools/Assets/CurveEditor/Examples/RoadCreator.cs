using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadCreator : MonoBehaviour
{
    [Range(0.05f, 1.5f)]
    public float spacing = 1;
    public float roadWidth = 1;
    public bool autoUpdate;
    public float tiling = 1;

    public void UpdateRoad()
    {
        Path path = GetComponent<PathCreator>().path;
        Vector2[] points = path.CalculateEvenlySpacedPoints(spacing);
        GetComponent<MeshFilter>().mesh = CreateRoadMesh(points, path.IsClosed);

        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * 0.05f);
        GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1,textureRepeat);
    }
    Mesh CreateRoadMesh(Vector2[] points, bool isClosed)
    {
        Vector3[] vertices = new Vector3[points.Length * 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        int numTriangles = 2 * (points.Length - 1) + ((isClosed) ? 2 : 0);
        int[] triangles = new int[numTriangles * (points.Length - 1) * 3];
        int verticesIndex = 0;
        int trianglesIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 forward = Vector2.zero;

            if (i < points.Length - 1 || isClosed)
            {
                forward += points[(i + 1) % points.Length] - points[i];
            }
            if (i > 0 || isClosed)
            {
                forward += points[i] - points[(i - 1 + points.Length) % points.Length];
            }

            forward.Normalize();
            Vector2 left = new Vector2(-forward.y, forward.x);

            vertices[verticesIndex] = points[i] + left * roadWidth * 0.5f;
            vertices[verticesIndex + 1] = points[i] - left * roadWidth * 0.5f;

            float completionPercent = i / (float)(points.Length - 1);
            float v = 1 - Mathf.Abs(2 * completionPercent - 1);
            uvs[verticesIndex] = new Vector2(0, v);
            uvs[verticesIndex + 1] = new Vector2(1, v);
            
            if (i < points.Length - 1 || isClosed)
            {
                triangles[trianglesIndex] = verticesIndex;
                triangles[trianglesIndex + 1] = (verticesIndex + 2) % vertices.Length;
                triangles[trianglesIndex + 2] = verticesIndex + 1;

                triangles[trianglesIndex + 3] = verticesIndex + 1;
                triangles[trianglesIndex + 4] = (verticesIndex + 2)  % vertices.Length;
                triangles[trianglesIndex + 5] = (verticesIndex + 3)  % vertices.Length;
            }

            verticesIndex += 2;
            trianglesIndex += 6;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        return mesh;
    }
}
