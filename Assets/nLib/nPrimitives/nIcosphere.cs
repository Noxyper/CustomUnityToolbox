using UnityEditor;
using UnityEngine;

using nMath;

public class nIcosphere : MonoBehaviour
{
    [MenuItem("GameObject/nPrimitives/Icosphere", false, 10)]
    public static void InstantiatePrimitive() 
    {
        GameObject tempIcosphere = new GameObject("nIcosphere");

        Mesh tempMesh = new Mesh()
        {
            vertices = new Vector3[60]
            {
                new Vector3(0f, 0.5f, 0f),
                new Vector3(0.447212f, 0.223607f, 0f),
                new Vector3(0.138193f, 0.223607f, -0.42532f),

                new Vector3(0f, 0.5f, 0f),
                new Vector3(0.138193f, 0.223607f, -0.42532f),
                new Vector3(-0.3618f, 0.223607f, -0.26286f),

                new Vector3(0f, 0.5f, 0f),
                new Vector3(-0.3618f, 0.223607f, -0.26286f),
                new Vector3(-0.3618f, 0.223607f, 0.26286f),

                new Vector3(0f, 0.5f, 0f),
                new Vector3(-0.3618f, 0.223607f, 0.26286f),
                new Vector3(0.138193f, 0.223607f, 0.42532f),

                new Vector3(0f, 0.5f, 0f),
                new Vector3(0.138193f, 0.223607f, 0.42532f),
                new Vector3(0.447212f, 0.223607f, 0f),



                new Vector3(0.447212f, 0.223607f, 0f),
                new Vector3(0.3618f, -0.223607f, 0.26286f),
                new Vector3(0.3618f, -0.223607f, -0.26286f),

                new Vector3(0.3618f, -0.223607f, -0.26286f),
                new Vector3(0.138193f, 0.223607f, -0.42532f),
                new Vector3(0.447212f, 0.223607f, 0f),

                new Vector3(0.138193f, 0.223607f, -0.42532f),
                new Vector3(0.3618f, -0.223607f, -0.26286f),
                new Vector3(-0.138193f, -0.223607f, -0.42532f),

                new Vector3(-0.138193f, -0.223607f, -0.42532f),
                new Vector3(-0.3618f, 0.223607f, -0.26286f),
                new Vector3(0.138193f, 0.223607f, -0.42532f),

                new Vector3(-0.3618f, 0.223607f, -0.26286f),
                new Vector3(-0.138193f, -0.223607f, -0.42532f),
                new Vector3(-0.447212f, -0.223607f, 0f),

                new Vector3(-0.447212f, -0.223607f, 0f),
                new Vector3(-0.3618f, 0.223607f, 0.26286f),
                new Vector3(-0.3618f, 0.223607f, -0.26286f),

                new Vector3(-0.3618f, 0.223607f, 0.26286f),
                new Vector3(-0.447212f, -0.223607f, 0f),
                new Vector3(-0.138193f, -0.223607f, 0.42532f),

                new Vector3(-0.138193f, -0.223607f, 0.42532f),
                new Vector3(0.138193f, 0.223607f, 0.42532f),
                new Vector3(-0.3618f, 0.223607f, 0.26286f),

                new Vector3(0.138193f, 0.223607f, 0.42532f),
                new Vector3(-0.138193f, -0.223607f, 0.42532f),
                new Vector3(0.3618f, -0.223607f, 0.26286f),

                new Vector3(0.3618f, -0.223607f, 0.26286f),
                new Vector3(0.447212f, 0.223607f, 0f),
                new Vector3(0.138193f, 0.223607f, 0.42532f),



                new Vector3(0f, -0.5f, 0f),
                new Vector3(-0.447212f, -0.223607f, 0f),
                new Vector3(-0.138193f, -0.223607f, -0.42532f),

                new Vector3(0f, -0.5f, 0f),
                new Vector3(-0.138193f, -0.223607f, -0.42532f),
                new Vector3(0.3618f, -0.223607f, -0.26286f),

                new Vector3(0f, -0.5f, 0f),
                new Vector3(0.3618f, -0.223607f, -0.26286f),
                new Vector3(0.3618f, -0.223607f, 0.26286f),

                new Vector3(0f, -0.5f, 0f),
                new Vector3(0.3618f, -0.223607f, 0.26286f),
                new Vector3(-0.138193f, -0.223607f, 0.42532f),

                new Vector3(0f, -0.5f, 0f),
                new Vector3(-0.138193f, -0.223607f, 0.42532f),
                new Vector3(-0.447212f, -0.223607f, 0f),
            },
            triangles = new int[60]
            {
                0, 1, 2,
                3, 4, 5,
                6, 7, 8,
                9, 10, 11,
                12, 13, 14,

                15, 16, 17,
                18, 19, 20,
                21, 22, 23,
                24, 25, 26,
                27, 28, 29,
                30, 31, 32,
                33, 34, 35,
                36, 37, 38,
                39, 40, 41,
                42, 43, 44,

                45, 46, 47,
                48, 49, 50,
                51, 52, 53,
                54, 55, 56,
                57, 58, 59
            },
            normals = new Vector3[60]
            {
                Vector3.up,
                new Vector3(0.8944f, 0.4472f, 0),
                new Vector3(0.2764f, 0.4472f, -0.8506f),

                Vector3.up,
                new Vector3(0.2764f, 0.4472f, -0.8506f),
                new Vector3(-0.7236f, 0.4472f, -0.5257f),

                Vector3.up,
                new Vector3(-0.7236f, 0.4472f, -0.5257f),
                new Vector3(-0.7236f, 0.4472f, 0.5257f),

                Vector3.up,
                new Vector3(-0.7236f, 0.4472f, 0.5257f),
                new Vector3(0.2764f, 0.4472f, 0.8506f),

                Vector3.up,
                new Vector3(0.2764f, 0.4472f, 0.8506f),
                new Vector3(0.8944f, 0.4472f, 0),



                new Vector3(0.8944f, 0.4472f, 0),
                new Vector3(0.7236f, -0.4472f, 0.5257f),
                new Vector3(0.7236f, -0.4472f, -0.5257f),

                new Vector3(0.7236f, -0.4472f, -0.5257f),
                new Vector3(0.2764f, 0.4472f, -0.8506f),
                new Vector3(0.8944f, 0.4472f, 0),

                new Vector3(0.2764f, 0.4472f, -0.8506f),
                new Vector3(0.7236f, -0.4472f, -0.5257f),
                new Vector3(-0.2764f, -0.4472f, -0.8506f),

                new Vector3(-0.2764f, -0.4472f, -0.8506f),
                new Vector3(-0.7236f, 0.4472f, -0.5257f),
                new Vector3(0.2764f, 0.4472f, -0.8506f),

                new Vector3(-0.7236f, 0.4472f, -0.5257f),
                new Vector3(-0.2764f, -0.4472f, -0.8506f),
                new Vector3(-0.8944f, -0.4472f, 0),

                new Vector3(-0.8944f, -0.4472f, 0),
                new Vector3(-0.7236f, 0.4472f, 0.5257f),
                new Vector3(-0.7236f, 0.4472f, -0.5257f),

                new Vector3(-0.7236f, 0.4472f, 0.5257f),
                new Vector3(-0.8944f, -0.4472f, 0),
                new Vector3(-0.2764f, -0.4472f, 0.8506f),

                new Vector3(-0.2764f, -0.4472f, 0.8506f),
                new Vector3(0.2764f, 0.4472f, 0.8506f),
                new Vector3(-0.7236f, 0.4472f, 0.5257f),

                new Vector3(0.2764f, 0.4472f, 0.8506f),
                new Vector3(-0.2764f, -0.4472f, 0.8506f),
                new Vector3(0.7236f, -0.4472f, 0.5257f),

                new Vector3(0.7236f, -0.4472f, 0.5257f),
                new Vector3(0.8944f, 0.4472f, 0),
                new Vector3(0.2764f, 0.4472f, 0.8506f),



                Vector3.down,
                new Vector3(-0.8944f, -0.4472f, 0),
                new Vector3(-0.2764f, -0.4472f, -0.8506f),

                Vector3.down,
                new Vector3(-0.2764f, -0.4472f, -0.8506f),
                new Vector3(0.7236f, -0.4472f, -0.5257f),

                Vector3.down,
                new Vector3(0.7236f, -0.4472f, -0.5257f),
                new Vector3(0.7236f, -0.4472f, 0.5257f),

                Vector3.down,
                new Vector3(0.7236f, -0.4472f, 0.5257f),
                new Vector3(-0.2764f, -0.4472f, 0.8506f),

                Vector3.down,
                new Vector3(-0.2764f, -0.4472f, 0.8506f),
                new Vector3(-0.8944f, -0.4472f, 0)            
            }
        };
        tempMesh.name = "nIcosphere";

        tempIcosphere.AddComponent<nTransform>().mesh = tempMesh;
        tempIcosphere.AddComponent<MeshFilter>().sharedMesh = tempMesh;
        tempIcosphere.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
    }
}
