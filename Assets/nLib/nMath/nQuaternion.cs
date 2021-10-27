using System;
using UnityEngine;

namespace nMath
{
    public class nQuaternion
    {
        public float w;
        public float x;
        public float y;
        public float z;

        #region Properties
        public static nQuaternion Identity { get { return new nQuaternion() { x = 0, y = 0, z = 0, w = 1 }; } }
        #endregion

        #region Constructors
        public nQuaternion() { x = 0; y = 0; z = 0; w = 0; }
        /// <summary>
        /// Quaternion Representation
        /// </summary>
        /// <param name="_angle">Angle</param>
        /// <param name="_axis">Axis</param>
        public nQuaternion(float _angle, nVector3 _axis) { x = Mathf.Sin(_angle / 2f) * _axis.x; y = Mathf.Sin(_angle / 2f) * _axis.y; z = Mathf.Sin(_angle / 2f) * _axis.z; w = Mathf.Cos(_angle / 2f); }
        /// <summary>
        /// Quaternion Representation
        /// </summary>
        /// <param name="_vector4">Axis (x,y,z) and Angle (w)</param>
        public nQuaternion(nVector4 _vector4) { x = Mathf.Sin(_vector4.w / 2f) * _vector4.x; y = Mathf.Sin(_vector4.w / 2f) * _vector4.y; z = Mathf.Sin(_vector4.w / 2f) * _vector4.z; w = Mathf.Cos(_vector4.w / 2f); }

        /// <summary>
        /// Axis-Angle Representation
        /// </summary>
        /// <param name="_nW">Angle</param>
        /// <param name="_nX">Axis (x)</param>
        /// <param name="_nY">Axis (y)</param>
        /// <param name="_nZ">Axis (z)</param>
        public nQuaternion(float _nW, float _nX, float _nY, float _nZ) { x = Mathf.Sin(_nW / 2f) * _nX; y = Mathf.Sin(_nW / 2f) * _nY; z = Mathf.Sin(_nW / 2f) * _nZ; w = Mathf.Cos(_nW / 2f); }
        #endregion

        #region Operators
        public static nQuaternion operator *(nQuaternion _r, nQuaternion _s)
        {
            nVector4 tempOutput = new nVector4(_s.w * _r.GetAxis() + _r.w * _s.GetAxis() + nVector3.Cross(_r.GetAxis(), _s.GetAxis()));
            tempOutput.w = _s.w * _r.w - nVector3.Dot(_s.GetAxis(), _r.GetAxis(), false);

            return tempOutput.ToNaomiQuaternion();
        }

        public static nQuaternion operator -(nQuaternion _q)
        {
            nQuaternion tempQuaternion = new nQuaternion();

            tempQuaternion.w = _q.w;
            tempQuaternion.SetAxis(-_q.GetAxis());

            return tempQuaternion;
        }
        #endregion

        public nVector4 GetAxisAngle()
        {
            float tempHalfAngle = Mathf.Acos(w);
            return new nVector4(x / Mathf.Sin(tempHalfAngle), y / Mathf.Sin(tempHalfAngle), z / Mathf.Sin(tempHalfAngle), tempHalfAngle * 2);
        }

        public nVector3 GetAxis()
        {
            return new nVector3(x, y, z);
        }

        public void SetAxis(nVector3 _vector3)
        {
            x = _vector3.x;
            y = _vector3.y;
            z = _vector3.z;
        }

        public static nQuaternion Slerp(nQuaternion _q, nQuaternion _r, float _t)
        {
            nQuaternion d = _r * -_q;
            nVector4 tempAxisAngle = d.GetAxisAngle();
            nQuaternion dT = new nQuaternion(tempAxisAngle.w * _t, new nVector3(tempAxisAngle.x, tempAxisAngle.y, tempAxisAngle.z));

            return dT * _q;
        }

        public override string ToString()
        {
            return string.Format("[{0} {1} {2} {3}]", w.ToString("0.00"), x.ToString("0.00"), y.ToString("0.00"), z.ToString("0.00"));
        }
    }
}