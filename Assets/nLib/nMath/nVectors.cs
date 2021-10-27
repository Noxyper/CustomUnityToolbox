using System;
using UnityEngine;

namespace nMath
{
    [Serializable]
    public class nVector2
    {
        public float x;
        public float y;

        #region Properties
        public static nVector2 One      { get { return new nVector2(1,  1); } }
        public static nVector2 Zero     { get { return new nVector2(0,  0); } }
        public static nVector2 Right    { get { return new nVector2(1,  0); } }
        public static nVector2 Left     { get { return new nVector2(-1, 0); } }
        public static nVector2 Up       { get { return new nVector2(0,  1); } }
        public static nVector2 Down     { get { return new nVector2(0, -1); } }

        public float Magnitude          { get { return Mathf.Sqrt((x * x) + (y * y)); } }
        public float SqrMagnitude       { get { return (x * x) + (y * y); } }
        public nVector2 Normalized      { get { return Magnitude > 0 ? this / Magnitude : this; } }
        #endregion

        #region Constructors
        public nVector2() { x = 0; y = 0; }
        public nVector2(float _nX, float _nY) { x = _nX; y = _nY; }
        public nVector2(nVector2 _vector2) { x = _vector2.x; y = _vector2.y; }
        public nVector2(nVector3 _vector3) { x = _vector3.x; y = _vector3.y; }
        public nVector2(nVector4 _vector4) { x = _vector4.x; y = _vector4.y; }
        #endregion

        #region Operators
        public static nVector2 operator -(nVector2 _v) => new nVector2(-_v.x, -_v.y);
        public static bool operator ==(nVector2 _a, nVector2 _b) => _a.x == _b.x && _a.y == _b.y;
        public static bool operator !=(nVector2 _a, nVector2 _b) => _a.x != _b.x || _a.y != _b.y;

        public static nVector2 operator +(nVector2 _a, nVector2 _b) => new nVector2(_a.x + _b.x, _a.y + _b.y);
        public static nVector2 operator -(nVector2 _a, nVector2 _b) => new nVector2(_a.x - _b.x, _a.y - _b.y);
        public static nVector2 operator *(nVector2 _v, float _s) => new nVector2(_v.x * _s, _v.y * _s);
        public static nVector2 operator *(float _s, nVector2 _v) => new nVector2(_v.x * _s, _v.y * _s);
        public static nVector2 operator /(nVector2 _v, float _d) => new nVector2(_v.x / _d, _v.y / _d);
        public static nVector2 operator /(float _d, nVector2 _v) => new nVector2(_v.x / _d, _v.y / _d);
        #endregion

        public nVector2 Normalize() => Magnitude > 0 ? this / Magnitude : this;

        public static float Dot(nVector2 _a, nVector2 _b, bool _shouldNormalize = true)
        {
            nVector2 a = _a;
            nVector2 b = _b;

            if(_shouldNormalize)
            {
                a = _a.Normalized;
                b = _b.Normalized;
            }

            return (a.x * b.x) + (a.y * b.y);
        }

        public static nVector2 Lerp(nVector2 _a, nVector2 _b, float _t) => (_a * (1 - Mathf.Clamp01(_t))) + (_b * Mathf.Clamp01(_t));

        #region System Overrides
        public override bool Equals(object _obj)
        {
            return _obj is nVector2 vector2 &&
                x == vector2.x &&
                y == vector2.y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}]", x.ToString("0.00"), y.ToString("0.00"));
        }
        #endregion
    }

    [Serializable]
    public class nVector3
    {
        public float x;
        public float y;
        public float z;

        #region Properties
        public static nVector3 One      { get { return new nVector3(1,  1,  1); } }
        public static nVector3 Zero     { get { return new nVector3(0,  0,  0); } }
        public static nVector3 Right    { get { return new nVector3(1,  0,  0); } }
        public static nVector3 Left     { get { return new nVector3(-1, 0,  0); } }
        public static nVector3 Up       { get { return new nVector3(0,  1,  0); } }
        public static nVector3 Down     { get { return new nVector3(0, -1,  0); } }
        public static nVector3 Forward  { get { return new nVector3(0,  0,  1); } }
        public static nVector3 Back     { get { return new nVector3(0,  0, -1); } }

        public float Magnitude { get { return Mathf.Sqrt((x * x) + (y * y) + (z * z)); } }
        public float SqrMagnitude { get { return (x * x) + (y * y) + (z * z); } }
        public nVector3 Normalized { get { return Magnitude > 0 ? this / Magnitude : this; } }
        #endregion

        #region Constructors
        public nVector3() { x = 0; y = 0; z = 0; }
        public nVector3(float _nX, float _nY) { x = _nX; y = _nY; z = 0; }
        public nVector3(float _nX, float _nY, float _nZ) { x = _nX; y = _nY; z = _nZ; }
        public nVector3(nVector2 _vector2) { x = _vector2.x; y = _vector2.y; z = 0; }
        public nVector3(nVector3 _vector3) { x = _vector3.x; y = _vector3.y; z = _vector3.z; }
        public nVector3(nVector4 _vector4) { x = _vector4.x; y = _vector4.y; z = _vector4.z; }
        #endregion

        #region Operators
        public static explicit operator nVector2(nVector3 _vector3) => new nVector2(_vector3.x, _vector3.y);
        public static explicit operator nVector3(nVector2 _vector2) => new nVector3(_vector2.x, _vector2.y, 0);

        public static nVector3 operator -(nVector3 _v) => new nVector3(-_v.x, -_v.y, -_v.z);
        public static bool operator ==(nVector3 _a, nVector3 _b) => _a.x == _b.x && _a.y == _b.y && _a.z == _b.z;
        public static bool operator !=(nVector3 _a, nVector3 _b) => _a.x != _b.x || _a.y != _b.y || _a.z != _b.z;

        public static nVector3 operator +(nVector3 _a, nVector3 _b) => new nVector3(_a.x + _b.x, _a.y + _b.y, _a.z + _b.z);
        public static nVector3 operator -(nVector3 _a, nVector3 _b) => new nVector3(_a.x - _b.x, _a.y - _b.y, _a.z - _b.z);
        public static nVector3 operator *(nVector3 _v, float _s) => new nVector3(_v.x * _s, _v.y * _s, _v.z * _s);
        public static nVector3 operator *(float _s, nVector3 _v) => new nVector3(_v.x * _s, _v.y * _s, _v.z * _s);
        public static nVector3 operator /(nVector3 _v, float _d) => new nVector3(_v.x / _d, _v.y / _d, _v.z / _d);
        public static nVector3 operator /(float _d, nVector3 _v) => new nVector3(_v.x / _d, _v.y / _d, _v.z / _d);
        #endregion

        public nVector3 Normalize() => Magnitude > 0 ? this / Magnitude : this;

        public static nVector3 Cross(nVector3 _a, nVector3 _b) => new nVector3(_a.y * _b.z - _a.z * _b.y, _a.z * _b.x - _a.x * _b.z, _a.x * _b.y - _a.y * _b.x);
        public static float Dot(nVector3 _a, nVector3 _b, bool _shouldNormalize = true)
        {
            nVector3 a = _a;
            nVector3 b = _b;

            if (_shouldNormalize)
            {
                a = _a.Normalized;
                b = _b.Normalized;
            }

            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }

        public static nVector3 Project(nVector3 _a, nVector3 _b) => Dot(_a, _b, false) * _a;
        public static nVector3 RotateAround(nVector3 _point, float _angle, nVector3 _axis) => (_point * Mathf.Cos(_angle)) + (Project(_axis, _point) * (1 - Mathf.Cos(_angle))) + (Cross(_axis, _point) * Mathf.Sin(_angle));
        public static nVector3 Lerp(nVector3 _a, nVector3 _b, float _t) => (_a * (1 - Mathf.Clamp01(_t))) + (_b * Mathf.Clamp01(_t));

        #region System Overrides
        public override bool Equals(object _obj)
        {
            return _obj is nVector3 vector3 &&
                x == vector3.x &&
                y == vector3.y &&
                z == vector3.z;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0} {1} {2}]", x.ToString("0.00"), y.ToString("0.00"), z.ToString("0.00"));
        }
        #endregion
    }

    [Serializable]
    public class nVector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        #region Properties
        public static nVector4 One { get { return new nVector4(1, 1, 1, 1); } }
        public static nVector4 Zero { get { return new nVector4(0, 0, 0, 0); } }

        public float Magnitude { get { return Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w)); } }
        public float SqrMagnitude { get { return (x * x) + (y * y) + (z * z) + (w * w); } }
        public nVector4 Normalized { get { return Magnitude > 0 ? this / Magnitude : this; } }
        #endregion

        #region Constructors
        public nVector4() { x = 0; y = 0; z = 0; w = 0; }
        public nVector4(float _nX, float _nY) { x = _nX; y = _nY; z = 0; w = 0; }
        public nVector4(float _nX, float _nY, float _nZ) { x = _nX; y = _nY; z = _nZ; w = 0; }
        public nVector4(float _nX, float _nY, float _nZ, float _nW) { x = _nX; y = _nY; z = _nZ; w = _nW; }
        public nVector4(nVector2 _vector2) { x = _vector2.x; y = _vector2.y; z = 0; w = 0; }
        public nVector4(nVector3 _vector3) { x = _vector3.x; y = _vector3.y; z = _vector3.z; w = 0; }
        public nVector4(nVector4 _vector4) { x = _vector4.x; y = _vector4.y; z = _vector4.z; w = _vector4.w; }
        #endregion

        #region Operators
        public static explicit operator nVector2(nVector4 _vector4) => new nVector2(_vector4.x, _vector4.y);
        public static explicit operator nVector3(nVector4 _vector4) => new nVector3(_vector4.x, _vector4.y, 0);
        public static explicit operator nVector4(nVector2 _vector2) => new nVector4(_vector2.x, _vector2.y, 0, 0);
        public static explicit operator nVector4(nVector3 _vector3) => new nVector4(_vector3.x, _vector3.y, _vector3.z, 0);

        public static nVector4 operator -(nVector4 _v) => new nVector4(-_v.x, -_v.y, -_v.z, -_v.w);
        public static bool operator ==(nVector4 _a, nVector4 _b) => _a.x == _b.x && _a.y == _b.y && _a.z == _b.z && _a.w == _b.w;
        public static bool operator !=(nVector4 _a, nVector4 _b) => _a.x != _b.x || _a.y != _b.y || _a.z != _b.z || _a.w != _b.w;

        public static nVector4 operator +(nVector4 _a, nVector4 _b) => new nVector4(_a.x + _b.x, _a.y + _b.y, _a.z + _b.z, _a.w + _b.w);
        public static nVector4 operator -(nVector4 _a, nVector4 _b) => new nVector4(_a.x - _b.x, _a.y - _b.y, _a.z - _b.z, _a.w - _b.w);
        public static nVector4 operator *(nVector4 _v, float _s) => new nVector4(_v.x * _s, _v.y * _s, _v.z * _s, _v.w * _s);
        public static nVector4 operator *(float _s, nVector4 _v) => new nVector4(_v.x * _s, _v.y * _s, _v.z * _s, _v.w * _s);
        public static nVector4 operator /(nVector4 _v, float _d) => new nVector4(_v.x / _d, _v.y / _d, _v.z / _d, _v.w / _d);
        public static nVector4 operator /(float _d, nVector4 _v) => new nVector4(_v.x / _d, _v.y / _d, _v.z / _d, _v.w / _d);
        #endregion

        public nVector4 Normalize() => Magnitude > 0 ? this / Magnitude : this;

        public static float Dot(nVector4 _a, nVector4 _b, bool _shouldNormalize = true)
        {
            nVector4 a = _a;
            nVector4 b = _b;

            if (_shouldNormalize)
            {
                a = _a.Normalized;
                b = _b.Normalized;
            }

            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }
        public static nVector4 Lerp(nVector4 _a, nVector4 _b, float _t) => (_a * (1 - Mathf.Clamp01(_t))) + (_b * Mathf.Clamp01(_t));

        #region System Overrides
        public override bool Equals(object _obj)
        {
            return _obj is nVector4 vector4 &&
                x == vector4.x &&
                y == vector4.y &&
                z == vector4.z &&
                w == vector4.w;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0} {1} {2} {3}]", x.ToString("0.00"), y.ToString("0.00"), z.ToString("0.00"), w.ToString("0.00"));
        }
        #endregion
    }
}