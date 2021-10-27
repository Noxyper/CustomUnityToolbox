using System.Linq;

using UnityEngine;
using UnityEditor;

using nMath;

public enum SceneToolMode
{
    MOVE, ROTATE, SCALE, SHEAR, SQUISH, _MODE_LENGTH
}

[CustomEditor(typeof(nTransform))]
[CanEditMultipleObjects]
public class nTransformEditor : Editor
{
    SceneToolMode _sceneTool = SceneToolMode.MOVE;
    float _labelWidth = 125f;

    nVector3 _radianRotation;

    nQuaternion _baseRotation;
    nVector3 _baseScale;

    nMatrix _baseMatrix;
    nMatrix _baseInverseMatrix;

    nVector3[] _meshVertices;

    void OnEnable()
    {
        nTransform transform = (nTransform)target;
        _baseRotation = transform.rotation;
        _baseScale = transform.scale;

        _baseMatrix = transform.matrix;
        _baseInverseMatrix = transform.inverseMatrix;

        Tools.current = Tool.None;
        _radianRotation = transform.rotation.ToEulerAngles() * (transform.rotationMode == nTransform.RotationMode.DEGREES ? Mathf.Rad2Deg : 1);
        _meshVertices = transform.mesh.vertices.ToNaomiVector3Array();
    }

    public override void OnInspectorGUI()
    {
        nTransform transform = (nTransform)target;

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Position", GUILayout.Width(_labelWidth))) _sceneTool = SceneToolMode.MOVE;
        nVector3 newPosition = EditorGUILayout.Vector3Field(GUIContent.none, transform.position.ToUnityVector3()).ToNaomiVector3();
        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(transform, "Move nObject");
            transform.position = newPosition;
            transform.UpdateVertices();
        }



        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(string.Format("Rotation ({0})", transform.rotationMode == nTransform.RotationMode.DEGREES ? "DEG" : "RAD"), GUILayout.Width(_labelWidth)))
        {
            _sceneTool = SceneToolMode.ROTATE;
            transform.rotationMode++;
            if (transform.rotationMode >= nTransform.RotationMode._MODE_LENGTH) transform.rotationMode = nTransform.RotationMode.DEGREES;
            _radianRotation *= (transform.rotationMode == nTransform.RotationMode.DEGREES ? Mathf.Rad2Deg : Mathf.Deg2Rad);
        }
        _radianRotation = EditorGUILayout.Vector3Field(GUIContent.none, _radianRotation.ToUnityVector3()).ToNaomiVector3();
        nQuaternion newRotationQ = new nQuaternion(_radianRotation.Magnitude * (transform.rotationMode == nTransform.RotationMode.DEGREES ? Mathf.Deg2Rad : 1), _radianRotation.Normalized);
        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(transform, "Rotate nObject");
            _baseRotation = transform.rotation = newRotationQ;
            transform.UpdateVertices();
        }



        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Scale", GUILayout.Width(_labelWidth))) _sceneTool = SceneToolMode.SCALE;
        nVector3 newScale = EditorGUILayout.Vector3Field(GUIContent.none, transform.scale.ToUnityVector3()).ToNaomiVector3();
        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(transform, "Scale nObject");
            _baseScale = transform.scale = newScale;
            transform.UpdateVertices();
        }

        EditorGUI.BeginDisabledGroup(true);
        transform.mesh = EditorGUILayout.ObjectField(transform.mesh, typeof(Mesh), true) as Mesh;
        EditorGUI.EndDisabledGroup();
    }

    void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseMove)
            Repaint();

        nTransform transform = (nTransform)target;

        if (Event.current.type == EventType.MouseUp)
        {
            _baseRotation = transform.rotation;
            _baseScale = transform.scale;

            _baseMatrix = transform.matrix;
            _baseInverseMatrix = transform.inverseMatrix;
        }

        EditorGUI.BeginChangeCheck();
        switch (_sceneTool)
        {
            case SceneToolMode.MOVE:
                Handles.color = new Color(0.76171875f, 0f, 0f);
                Vector3 newXPos = Handles.Slider((nVector3.Zero * transform.matrix).ToUnityVector3(), 
                                                 transform.Right.ToUnityVector3(), 
                                                 HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                 Handles.ArrowHandleCap, 
                                                 EditorSnapSettings.move.x);

                Handles.color = new Color(0.92578125f, 0.66015625f, 0f);
                Vector3 newYPos = Handles.Slider((nVector3.Zero * transform.matrix).ToUnityVector3(), 
                                                 transform.Up.ToUnityVector3(), 
                                                 HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                 Handles.ArrowHandleCap, 
                                                 EditorSnapSettings.move.y);

                Handles.color = new Color(0f, 0.4296875f, 0.85546875f);
                Vector3 newZPos = Handles.Slider((nVector3.Zero * transform.matrix).ToUnityVector3(), 
                                                 transform.Forward.ToUnityVector3(), 
                                                 HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                 Handles.ArrowHandleCap, 
                                                 EditorSnapSettings.move.z);

                Vector3 newPosition = (new nVector3((newXPos.ToNaomiVector3() * transform.inverseMatrix).x, 
                                                    (newYPos.ToNaomiVector3() * transform.inverseMatrix).y, 
                                                    (newZPos.ToNaomiVector3() * transform.inverseMatrix).z) * transform.matrix).ToUnityVector3();
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Move nObject");
                    transform.position = newPosition.ToNaomiVector3();
                    transform.UpdateVertices();
                }
                break;
            case SceneToolMode.ROTATE:
                Handles.color = new Color(0.76171875f, 0f, 0f);
                nQuaternion newXRot = Handles.Disc(nQuaternion.Identity.ToUnityQuaternion(), 
                                                   transform.position.ToUnityVector3(), 
                                                   transform.Right.ToUnityVector3(), 
                                                   HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                   false, EditorSnapSettings.rotate).ToNaomiQuaternion();

                Handles.color = new Color(0.92578125f, 0.66015625f, 0f);
                nQuaternion newYRot = Handles.Disc(nQuaternion.Identity.ToUnityQuaternion(), 
                                                   transform.position.ToUnityVector3(), 
                                                   transform.Up.ToUnityVector3(), 
                                                   HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                   false, EditorSnapSettings.rotate).ToNaomiQuaternion();

                Handles.color = new Color(0f, 0.4296875f, 0.85546875f);
                nQuaternion newZRot = Handles.Disc(nQuaternion.Identity.ToUnityQuaternion(),
                                                   transform.position.ToUnityVector3(), 
                                                   transform.Forward.ToUnityVector3(), 
                                                   HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                   false, EditorSnapSettings.rotate).ToNaomiQuaternion();

                nQuaternion newRotation =  (newYRot * (newXRot * newZRot)) * _baseRotation;
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Rotate nObject");
                    transform.rotation = newRotation;
                    transform.eulerAngles = newRotation.ToEulerAngles();
                    _radianRotation = transform.rotation.ToEulerAngles() * (transform.rotationMode == nTransform.RotationMode.DEGREES ? Mathf.Rad2Deg : 1);
                    transform.UpdateVertices();
                }
                break;
            case SceneToolMode.SCALE:
                Handles.color = new Color(0.76171875f, 0f, 0f);
                float newXScale = Handles.ScaleSlider(_baseScale.x, transform.position.ToUnityVector3(), 
                                                      transform.Right.ToUnityVector3(), 
                                                      transform.rotation.ToUnityQuaternion(),
                                                      HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                      EditorSnapSettings.scale);

                Handles.color = new Color(0.92578125f, 0.66015625f, 0f);
                float newYScale = Handles.ScaleSlider(_baseScale.y, 
                                                      transform.position.ToUnityVector3(), 
                                                      transform.Up.ToUnityVector3(), 
                                                      transform.rotation.ToUnityQuaternion(), 
                                                      HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                      EditorSnapSettings.scale);

                Handles.color = new Color(0f, 0.4296875f, 0.85546875f);
                float newZScale = Handles.ScaleSlider(_baseScale.z,
                                                      transform.position.ToUnityVector3(), 
                                                      transform.Forward.ToUnityVector3(), 
                                                      transform.rotation.ToUnityQuaternion(), 
                                                      HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                      EditorSnapSettings.scale);

                Handles.color = new Color(0.2578125f, 0.2578125f, 0.2578125f);
                float newOverallScale = Handles.ScaleValueHandle(_baseScale.Magnitude, 
                                                                 transform.position.ToUnityVector3(), 
                                                                 transform.rotation.ToUnityQuaternion(), 
                                                                 HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, 
                                                                 Handles.CubeHandleCap, 
                                                                 EditorSnapSettings.scale);

                nVector3 newScale = newOverallScale != _baseScale.Magnitude ? _baseScale.Normalized * newOverallScale : new nVector3(newXScale, newYScale, newZScale);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Scale nObject");
                    transform.scale = newScale;
                    transform.UpdateVertices();
                }
                break;
            case SceneToolMode.SHEAR:
                nVector3 shearXBy = transform.shearAxes[0], shearYBy = transform.shearAxes[1], shearZBy = transform.shearAxes[2];

                Handles.color = new Color(0.76171875f, 0f, 0f);
                Handles.DrawSolidRectangleWithOutline(new Vector3[] { 
                    (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.min.y, transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.max.y, transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.max.y, transform.mesh.bounds.max.z) * transform.matrix).ToUnityVector3(),
                    (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.min.y, transform.mesh.bounds.max.z) * transform.matrix).ToUnityVector3() 
                }, new Color(0.76171875f, 0f, 0f, 0.5f), new Color(0.76171875f, 0f, 0f));

                nVector3 shearXByY = Handles.Slider( //Shear X by Y
                    (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    ((new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.max.z) * transform.matrix) - (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.z) * transform.matrix)).ToUnityVector3(),
                    HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, Handles.ArrowHandleCap, 0.1f).ToNaomiVector3() - (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.z) * transform.matrix);

                nVector3 shearXByZ = Handles.Slider( //Shear X by Z
                    (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix).ToUnityVector3(),
                    ((new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.max.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix) - (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix)).ToUnityVector3(),
                    HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, Handles.ArrowHandleCap, 0.1f).ToNaomiVector3() - (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix);

                if (nVector3.Zero != shearXByY + shearXByZ)
                    shearXBy += new nVector3(0f, shearXByY.z, 0f) + new nVector3(0f, 0f, shearXByZ.y);


                Handles.color = new Color(0.92578125f, 0.66015625f, 0f);
                Handles.DrawSolidRectangleWithOutline(new Vector3[] { 
                    (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    (new nVector3(transform.mesh.bounds.max.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    (new nVector3(transform.mesh.bounds.max.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.max.z) * transform.matrix).ToUnityVector3(),
                    (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f), transform.mesh.bounds.max.z) * transform.matrix).ToUnityVector3() 
                }, new Color(0.92578125f, 0.66015625f, 0f, 0.5f), new Color(0.92578125f, 0.66015625f, 0f));

                nVector3 shearYByX = Handles.Slider( //Shear Y by X
                    (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.y, transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    ((new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.y, transform.mesh.bounds.max.z) * transform.matrix) - (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.y, transform.mesh.bounds.min.z) * transform.matrix)).ToUnityVector3(),
                    HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, Handles.ArrowHandleCap, 0.1f).ToNaomiVector3() - (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.y, transform.mesh.bounds.min.z) * transform.matrix);

                nVector3 shearYByZ = Handles.Slider( //Shear Y by Z
                    (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix).ToUnityVector3(),
                    ((new nVector3(transform.mesh.bounds.max.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix) - (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix)).ToUnityVector3(),
                    HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, Handles.ArrowHandleCap, 0.1f).ToNaomiVector3() - (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f)) * transform.matrix);

                if (nVector3.Zero != shearYByX + shearYByZ)
                    shearYBy += new nVector3(shearYByX.z, 0f, 0f) + new nVector3(0f, 0f, shearYByZ.x);


                Handles.color = new Color(0f, 0.4296875f, 0.85546875f);
                Handles.DrawSolidRectangleWithOutline(new Vector3[] { 
                    (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f)) * transform.matrix).ToUnityVector3(),
                    (new nVector3(transform.mesh.bounds.max.x, transform.mesh.bounds.min.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f)) * transform.matrix).ToUnityVector3(),
                    (new nVector3(transform.mesh.bounds.max.x, transform.mesh.bounds.max.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f)) * transform.matrix).ToUnityVector3(),
                    (new nVector3(transform.mesh.bounds.min.x, transform.mesh.bounds.max.y, _meshVertices.Max(x => x.z) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .25f)) * transform.matrix).ToUnityVector3() 
                }, new Color(0f, 0.4296875f, 0.85546875f, 0.5f), new Color(0f, 0.4296875f, 0.85546875f));
                
                nVector3 shearZByX = Handles.Slider( //Shear Z by X
                    (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.y, transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    ((new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.max.y, transform.mesh.bounds.min.z) * transform.matrix) - (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.y, transform.mesh.bounds.min.z) * transform.matrix)).ToUnityVector3(),
                    HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, Handles.ArrowHandleCap, 0.1f).ToNaomiVector3() - (new nVector3(_meshVertices.Max(x => x.x) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.y, transform.mesh.bounds.min.z) * transform.matrix);

                nVector3 shearZByY = Handles.Slider( //Shear Z by Y
                    (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.z) * transform.matrix).ToUnityVector3(),
                    ((new nVector3(transform.mesh.bounds.max.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.z) * transform.matrix) - (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.z) * transform.matrix)).ToUnityVector3(),
                    HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * 1f, Handles.ArrowHandleCap, 0.1f).ToNaomiVector3() - (new nVector3(transform.mesh.bounds.min.x, _meshVertices.Max(x => x.y) + (HandleUtility.GetHandleSize(transform.position.ToUnityVector3()) * .5f), transform.mesh.bounds.min.z) * transform.matrix);

                if (nVector3.Zero != shearZByX + shearZByY)
                    shearZBy += new nVector3(shearZByX.y, 0f, 0f) + new nVector3(0f, shearZByY.x, 0f);


                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Shear nObject");
                    transform.shearAxes = new nVector3[] { shearXBy, shearYBy, shearZBy };
                    transform.UpdateVertices();
                }
                break;
            case SceneToolMode.SQUISH:
                Vector3 newSquishedScale = transform.scale.ToUnityVector3();

                Handles.color = new Color(0.76171875f, 0f, 0f);
                float newScaleX = Handles.ScaleSlider(1f, 
                                                      transform.position.ToUnityVector3(), 
                                                      transform.Right.ToUnityVector3(), 
                                                      transform.rotation.ToUnityQuaternion(), 
                                                      HandleUtility.GetHandleSize(transform.position.ToUnityVector3()), 
                                                      1f);
                if (newScaleX <= 0) newScaleX = 0.01f;

                Handles.color = new Color(0.92578125f, 0.66015625f, 0f);
                float newScaleY = Handles.ScaleSlider(1f, 
                                                      transform.position.ToUnityVector3(), 
                                                      transform.Up.ToUnityVector3(), 
                                                      transform.rotation.ToUnityQuaternion(), 
                                                      HandleUtility.GetHandleSize(transform.position.ToUnityVector3()), 
                                                      1f);
                if (newScaleY <= 0) newScaleY = 0.01f;
                
                Handles.color = new Color(0f, 0.4296875f, 0.85546875f);
                float newScaleZ = Handles.ScaleSlider(1f, 
                                                      transform.position.ToUnityVector3(), 
                                                      transform.Forward.ToUnityVector3(), 
                                                      transform.rotation.ToUnityQuaternion(), 
                                                      HandleUtility.GetHandleSize(transform.position.ToUnityVector3()), 
                                                      1f);
                if (newScaleZ <= 0) newScaleZ = 0.01f;
                
                newSquishedScale =  newScaleX * _baseScale.x != _baseScale.x ? new Vector3(_baseScale.x * newScaleX,
                                                                                           _baseScale.y * Mathf.Sqrt(1 / newScaleX),
                                                                                           _baseScale.z * Mathf.Sqrt(1 / newScaleX)) :
                                    newScaleY * _baseScale.y != _baseScale.y ? new Vector3(_baseScale.x * Mathf.Sqrt(1 / newScaleY),
                                                                                           _baseScale.y * newScaleY,
                                                                                           _baseScale.z * Mathf.Sqrt(1 / newScaleY)) :
                                    newScaleZ * _baseScale.z != _baseScale.z ? new Vector3(_baseScale.x * Mathf.Sqrt(1 / newScaleZ),
                                                                                           _baseScale.y * Mathf.Sqrt(1 / newScaleZ),
                                                                                           _baseScale.z * newScaleZ) : 
                                                                                           newSquishedScale;

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(transform, "Squish nObject");
                    transform.scale = newSquishedScale.ToNaomiVector3();
                    transform.UpdateVertices();
                }
                break;
        }

        Handles.BeginGUI();
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        GUIStyleState buttonStyleNormal = buttonStyle.normal;
        GUIStyleState buttonStyleHover = buttonStyle.hover;
        GUIStyleState buttonStyleActive = buttonStyle.active;

        buttonStyleNormal.background = Resources.Load<Texture2D>("ButtonBackdrop");
        buttonStyleHover.background = Resources.Load<Texture2D>("ButtonBackdrop_Hover");
        buttonStyleActive.background = Resources.Load<Texture2D>("ButtonBackdrop_Active");

        buttonStyle.normal = buttonStyleNormal;
        buttonStyle.hover = buttonStyleHover;
        buttonStyle.active = buttonStyleActive;

        GUI.backgroundColor = Color.white;
        for (int x = 0; x < (int)SceneToolMode._MODE_LENGTH; x++)
        {
            GUI.backgroundColor = x == 0 ? new Color(0.464844f, 0f, 0f) : x == 1 ? new Color(0f, 0.300781f, 0.597656f) : x == 2 ? new Color(0.5f, 0.425781f, 0.25f) : x == 3 ? new Color(0.01171875f, 0.55859375f, 0f) : new Color(0.48046875f, 0f, 0.81640625f);
            if (GUI.Button(new Rect((x * 60) + 0, 0, 60, 60), Resources.Load<Texture2D>(x == 0 ? "Translate" : x == 1 ? "Rotate" : x == 2 ? "Scale" : x == 3 ? "Shear" : "Squish"), buttonStyle))
            {
                _sceneTool = (SceneToolMode)x;
            }
        }

        Handles.EndGUI();
    }

    void OnDisable()
    {
        Tools.current = Tool.Move;
    }
}
