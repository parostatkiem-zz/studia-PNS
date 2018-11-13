using UnityEngine;
using System.Collections.Generic;



public static class MapRenderer 
{
   
    public static Mesh RenderTheMap(float cubeSize =1, float cubeHeight =0.1f, float  distance=0.1f){
        return RenderCube(0, 0, cubeSize, cubeHeight);
    }

    private static Mesh RenderCube(float x, float y, float size, float height){
        Mesh mesh = new Mesh();
     
        mesh.vertices = GetCubeVertices(x,y,size, height);
        mesh.triangles = GetCubeTriangles(0,0,1);
     
        mesh.RecalculateNormals();
        return mesh;

    }
    private static Vector3[] GetCubeVertices(float x, float y, float size, float height){
        return new Vector3[]{
            new Vector3(0, 0, 0), //
            new Vector3(1, 0, 0), //
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 1, height),
            new Vector3(1, 1, height),
            new Vector3(1, 0, height),//
            new Vector3(0, 0, height),//
        };
    }

    private static int[] GetCubeTriangles(double x, double y, double size)
    {
        return new int[]{
            0, 2, 1, //face front
            0, 3, 2,
            2, 3, 4, //face top
            2, 4, 5,
            1, 2, 5, //face right
            1, 5, 6,
            0, 7, 4, //face left
            0, 4, 3,
            5, 4, 7, //face back
            5, 7, 6,
            0, 6, 7, //face bottom
            0, 1, 6
        };
    }
}
