using UnityEditor;
using UnityEngine;


public class nCube : MonoBehaviour
{
    [MenuItem("GameObject/nPrimitives/Cube", false, 10)]
    public static void InstantiatePrimitive() 
    {
        GameObject tempCube = new GameObject("nCube");

        Mesh tempMesh = new Mesh()
        {
            vertices = new Vector3[24]
            {
                new Vector3(0.5f,   0.5f,   0.5f),
                new Vector3(0.5f,   0.5f,  -0.5f),
                new Vector3(-0.5f,  0.5f,  -0.5f),
                new Vector3(-0.5f,  0.5f,   0.5f),

                new Vector3(0.5f,   0.5f,   0.5f),
                new Vector3(0.5f,  -0.5f,   0.5f),
                new Vector3(0.5f,  -0.5f,  -0.5f),
                new Vector3(0.5f,   0.5f,  -0.5f),

                new Vector3(0.5f,   0.5f,   0.5f),
                new Vector3(-0.5f,  0.5f,   0.5f),
                new Vector3(-0.5f, -0.5f,   0.5f),
                new Vector3(0.5f,  -0.5f,   0.5f),

                new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f,  -0.5f,  -0.5f),
                new Vector3(0.5f,  -0.5f,   0.5f),
                new Vector3(-0.5f, -0.5f,   0.5f),

                new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(-0.5f,  0.5f,  -0.5f),
                new Vector3(0.5f,   0.5f,  -0.5f),
                new Vector3(0.5f,  -0.5f,  -0.5f),

                new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(-0.5f, -0.5f,   0.5f),
                new Vector3(-0.5f,  0.5f,   0.5f),
                new Vector3(-0.5f,  0.5f,  -0.5f),
            },
            triangles = new int[36]
            {
                0, 1, 2,
                2, 3, 0,

                4, 5, 6,
                6, 7, 4,

                8, 9, 10,
                10, 11, 8,

                12, 13, 14,
                14, 15, 12,

                16, 17, 18,
                18, 19, 16,

                20, 21, 22,
                22, 23, 20
            },
            normals = new Vector3[24]
            {
                Vector3.up,
                Vector3.up,
                Vector3.up,
                Vector3.up,

                Vector3.right,
                Vector3.right,
                Vector3.right,
                Vector3.right,

                Vector3.forward,
                Vector3.forward,
                Vector3.forward,
                Vector3.forward,

                Vector3.down,
                Vector3.down,
                Vector3.down,
                Vector3.down,

                Vector3.back,
                Vector3.back,
                Vector3.back,
                Vector3.back,

                Vector3.left,
                Vector3.left,
                Vector3.left,
                Vector3.left,
            }
        };
        tempMesh.name = "nCube";

        tempCube.AddComponent<nTransform>().mesh = tempMesh;
        tempCube.AddComponent<MeshFilter>().sharedMesh = tempMesh;
        tempCube.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
    }
}
