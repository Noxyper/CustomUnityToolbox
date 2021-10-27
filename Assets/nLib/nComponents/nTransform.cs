using UnityEngine;

using nMath;
using System.Linq;

[ExecuteAlways]
public class nTransform : MonoBehaviour
{
    public enum RotationMode
    {
        DEGREES, RADIANS, _MODE_LENGTH
    }

    public RotationMode rotationMode;

    public nVector3 position = nVector3.Zero;
    public nVector3 eulerAngles = nVector3.Zero;
    public nQuaternion rotation = nQuaternion.Identity;
    public nVector3 scale = nVector3.One;
    public nVector3[] shearAxes = new nVector3[3] { nVector3.Zero, nVector3.Zero, nVector3.Zero };

    public Mesh mesh;
    /// <summary>
    /// Matrix to Convert from Local Space to World Space
    /// </summary>
    public nMatrix matrix;
    /// <summary>
    /// Matrix to Convert from World Space to Local Space
    /// </summary>
    public nMatrix inverseMatrix;

    public nVector3 Right
    {
        get
        {
            return (rotation * new nVector4(nVector3.Right).ToNaomiQuaternion() * -rotation).GetAxis();
        }
    }

    public nVector3 Up
    {
        get
        {
            return (rotation * new nVector4(nVector3.Up).ToNaomiQuaternion() * -rotation).GetAxis();
        }
    }

    public nVector3 Forward
    {
        get
        {
            return (rotation * new nVector4(nVector3.Forward).ToNaomiQuaternion() * -rotation).GetAxis();
        }
    }

    private MeshFilter _filter
    {
        get
        {
            if (GetComponent<MeshFilter>())
                return GetComponent<MeshFilter>();
            else
                return null;
        }
    }

    void Start()
    {
        _filter.sharedMesh = Instantiate(mesh);
    }

    void Update()
    {
        UpdateVertices();
    }

    public void UpdateVertices()
    {
        nVector3[] tempVertices = new nVector3[mesh.vertices.Length];
        matrix = nMatrix.Translate(position) * 
                (nMatrix.Rotate(rotation) * 
                (nMatrix.Scale(scale) * 
                 nMatrix.Shear(shearAxes[0], shearAxes[1], shearAxes[2])));
        inverseMatrix = nMatrix.Shear(shearAxes[0], shearAxes[1], shearAxes[2]).ShearInverse() * 
                       (nMatrix.Scale(scale).ScaleInverse() * 
                       (nMatrix.Rotate(-rotation).RotateInverse() * 
                        nMatrix.Translate(position).TranslateInverse()));

        for (int i = 0; i < tempVertices.Length; i++)
        {
            tempVertices[i] = matrix * mesh.vertices[i].ToNaomiVector3();
        }

        if (_filter != null)
        {
            _filter.sharedMesh.vertices = tempVertices.ToUnityVector3Array();

            _filter.sharedMesh.RecalculateNormals();
            _filter.sharedMesh.RecalculateBounds();
        }
    }
}
