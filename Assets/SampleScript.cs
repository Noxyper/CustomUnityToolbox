using nMath;

using UnityEngine;

[System.Serializable]
public enum SampleMode
{
    TRANSLATE, ROTATE, SCALE, SHEAR
}

public class SampleScript : MonoBehaviour
{
    public SampleMode mode;
    public float speed = 5f;

    nTransform nTransform;

    void Start()
    {
        nTransform = GetComponent<nTransform>();
    }

    void Update()
    {
        switch(mode)
        {
            case SampleMode.TRANSLATE:
                nTransform.position = new nVector3(Mathf.PingPong(Time.time * speed, 2) - 1f, 0f, 0f);
                break;
            case SampleMode.ROTATE:
                nTransform.rotation = new nQuaternion(new nVector4(0f, 1f, 0f, Time.time * speed));
                break;
            case SampleMode.SCALE:
                nTransform.scale = new nVector3(Mathf.PingPong(Time.time * speed, 1) + 1f, Mathf.PingPong(Time.time * speed, 1) + 1f, Mathf.PingPong(Time.time * speed, 1) + 1f);
                break;
            case SampleMode.SHEAR:
                nTransform.shearAxes[0] = new nVector3(0f, Mathf.PingPong(Time.time * speed, Mathf.PI / 4), Mathf.PingPong(Time.time * speed, Mathf.PI / 4));
                break;
        }
    }
}
