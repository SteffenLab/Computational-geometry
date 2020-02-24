using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Habrador_Computational_Geometry;



//Display meshes, points, etc so we dont have to do it in each file
public static class DisplayResultsHelper
{
    //Display some points
    public static void DisplayPoints(HashSet<Vector3> points, float radius, Color color)
    {
        if (points == null)
        {
            return;
        }
    
        Gizmos.color = color;

        foreach (Vector3 p in points)
        {
            Gizmos.DrawSphere(p, radius);
        }
    }



    //Display an arrow at the end of vector from a to b
    public static void DrawArrow(Vector3 a, Vector3 b, float size, Color color)
    {
        //We also need to know the direction of the vector, so we need to draw a small arrow
        Vector3 vecDir = (b - a).normalized;

        Vector3 vecDirPerpendicular = new Vector3(vecDir.z, 0f, -vecDir.x);

        Vector3 arrowStart = b - vecDir * size;

        //Draw the arrows 4 lines
        Gizmos.color = color;

        Gizmos.DrawLine(arrowStart, arrowStart + vecDirPerpendicular * size);
        Gizmos.DrawLine(arrowStart + vecDirPerpendicular * size, b);
        Gizmos.DrawLine(b, arrowStart - vecDirPerpendicular * size);
        Gizmos.DrawLine(arrowStart - vecDirPerpendicular * size, arrowStart);
    }



    //Display some mesh where each triangle could have a random color
    private static void DisplayMesh(Mesh mesh, bool useRandomColor, int seed, Color meshColor)
    {
        if (mesh == null)
        {
            Debug.Log("Cant display the mesh because there's no mesh!");
        
            return;
        }
    
        //Display the triangles with a random color
        int[] meshTriangles = mesh.triangles;

        Vector3[] meshVertices = mesh.vertices;

        Random.InitState(seed);

        for (int i = 0; i < meshTriangles.Length; i += 3)
        {
            Vector3 p1 = meshVertices[meshTriangles[i + 0]];
            Vector3 p2 = meshVertices[meshTriangles[i + 1]];
            Vector3 p3 = meshVertices[meshTriangles[i + 2]];

            Mesh triangleMesh = new Mesh();

            triangleMesh.vertices = new Vector3[] { p1, p2, p3 };

            triangleMesh.triangles = new int[] { 0, 1, 2 };

            triangleMesh.RecalculateNormals();

            if (useRandomColor)
            {
                Gizmos.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            }
            else
            {
                Gizmos.color = meshColor;
            }
            

            Gizmos.DrawMesh(triangleMesh);
        }
    }

    //Just one color
    public static void DisplayMesh(Mesh mesh, int seed, Color meshColor)
    {
        DisplayMesh(mesh, false, seed, meshColor);
    }

    //Random color
    public static void DisplayMeshWithRandomColors(Mesh mesh, int seed)
    {
        DisplayMesh(mesh, true, seed, Color.black);
    }



    //Display the edges of a mesh's triangles with some color
    public static void DisplayMeshEdges(Mesh mesh, Color sideColor)
    {
        if (mesh == null)
        {
            return;
        }

        //Display the triangles with a random color
        int[] meshTriangles = mesh.triangles;

        Vector3[] meshVertices = mesh.vertices;

        for (int i = 0; i < meshTriangles.Length; i += 3)
        {
            Vector3 p1 = meshVertices[meshTriangles[i + 0]];
            Vector3 p2 = meshVertices[meshTriangles[i + 1]];
            Vector3 p3 = meshVertices[meshTriangles[i + 2]];

            Gizmos.color = sideColor;

            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p1);
        }
    }



    //Display a connected set of points, like a convex hull
    public static void DisplayConnectedPoints(List<Vector3> points, Color color)
    {
        if (points == null)
        {
            return;
        }
    
        Gizmos.color = color;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 pos = points[i];

            Vector3 posNext = points[MathUtility.ClampListIndex(i + 1, points.Count)];

            Gizmos.color = Color.black;

            Gizmos.DrawLine(pos, posNext);

            Gizmos.DrawSphere(pos, 0.2f);
        }
    }
}