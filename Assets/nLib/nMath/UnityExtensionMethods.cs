using System;
using UnityEngine;

namespace nMath
{
    public static class UnityConversionExtensions
    {
        public static Vector2[] ToUnityVector2Array(this nVector2[] _vector2s)
        {
            Vector2[] tempArray = new Vector2[_vector2s.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = _vector2s[i].ToUnityVector2();
            }
            return tempArray;
        }
        public static Vector3[] ToUnityVector3Array(this nVector3[] _vector3s)
        {
            Vector3[] tempArray = new Vector3[_vector3s.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = _vector3s[i].ToUnityVector3();
            }
            return tempArray;
        }
        public static Vector4[] ToUnityVector4Array(this nVector4[] _vector4s)
        {
            Vector4[] tempArray = new Vector4[_vector4s.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = _vector4s[i].ToUnityVector4();
            }
            return tempArray;
        }

        public static nVector2[] ToNaomiVector2Array(this Vector2[] _vector2s) 
        {
            nVector2[] tempArray = new nVector2[_vector2s.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = _vector2s[i].ToNaomiVector2();
            }
            return tempArray;
        }
        public static nVector3[] ToNaomiVector3Array(this Vector3[] _vector3s)
        {
            nVector3[] tempArray = new nVector3[_vector3s.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = _vector3s[i].ToNaomiVector3();
            }
            return tempArray;
        }
        public static nVector4[] ToNaomiVector4Array(this Vector4[] _vector4s)
        {
            nVector4[] tempArray = new nVector4[_vector4s.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = _vector4s[i].ToNaomiVector4();
            }
            return tempArray;
        }

        public static Vector2 ToUnityVector2(this nVector2 _vector2) => new Vector2(_vector2.x, _vector2.y);
        public static Vector3 ToUnityVector3(this nVector3 _vector3) => new Vector3(_vector3.x, _vector3.y, _vector3.z);
        public static Vector4 ToUnityVector4(this nVector4 _vector4) => new Vector4(_vector4.x, _vector4.y, _vector4.z, _vector4.w);

        public static nVector2 ToNaomiVector2(this Vector2 _vector2) => new nVector2(_vector2.x, _vector2.y);
        public static nVector3 ToNaomiVector3(this Vector3 _vector3) => new nVector3(_vector3.x, _vector3.y, _vector3.z);
        public static nVector4 ToNaomiVector4(this Vector4 _vector4) => new nVector4(_vector4.x, _vector4.y, _vector4.z, _vector4.w);

        /// <summary>
        /// Converts the 2D vector into an angle.
        /// </summary>
        /// <param name="_vector2">The vector</param>
        /// <returns>An angle</returns>
        public static float ToRadians(this nVector2 _vector2) => Mathf.Atan2(_vector2.y, _vector2.x);
        /// <summary>
        /// Interprets the float as an angle and converts it into a 2D vector
        /// </summary>
        /// <param name="_angle">The angle</param>
        /// <returns>A 2D vector</returns>
        public static nVector2 ToNaomiVector2(this float _angle) => new nVector2(Mathf.Cos(_angle), Mathf.Sin(_angle));
        /// <summary>
        /// Interprets the vector as Euler angles and converts it into a 3D vector
        /// </summary>
        /// <param name="_euler">The Euler angles</param>
        /// <returns>A 3D direction</returns>
        public static nVector3 ToDirection(this nVector3 _euler) => new nVector3(Mathf.Cos(_euler.x) * Mathf.Sin(_euler.y), Mathf.Sin(-_euler.x), Mathf.Cos(_euler.y) * Mathf.Cos(_euler.x));

        public static nQuaternion ToNaomiQuaternion(this nVector4 _vector4) => new nQuaternion() { x = _vector4.x, y = _vector4.y, z = _vector4.z, w = _vector4.w };
        public static nQuaternion ToNaomiQuaternion(this Quaternion _quaternion) => new nVector4(_quaternion.x, _quaternion.y, _quaternion.z, _quaternion.w).ToNaomiQuaternion();
        public static Quaternion ToUnityQuaternion(this nQuaternion _quaternion) => new Quaternion(_quaternion.x, _quaternion.y, _quaternion.z, _quaternion.w);

        /// <summary>
        /// Interprets the vector as Euler angles and converts it into a quaternion
        /// </summary>
        /// <param name="_eulerAngles">The Euler angles</param>
        /// <returns>A quaternion</returns>
        public static nQuaternion ToNaomiQuaternion(this nVector3 _eulerAngles)
        {
            float sinPitch = Mathf.Sin(_eulerAngles.y * 0.5f), sinYaw = Mathf.Sin(_eulerAngles.z * 0.5f), sinRoll = Mathf.Sin(_eulerAngles.x * 0.5f);
            float cosPitch = Mathf.Cos(_eulerAngles.y * 0.5f), cosYaw = Mathf.Cos(_eulerAngles.z * 0.5f), cosRoll = Mathf.Cos(_eulerAngles.x * 0.5f);

            return new nVector4(sinRoll * cosPitch * cosYaw - cosRoll * sinPitch * sinYaw,
                                cosRoll * sinPitch * cosYaw + sinRoll * cosPitch * sinYaw,
                                cosRoll * cosPitch * sinYaw - sinRoll * sinPitch * cosYaw,
                                cosRoll * cosPitch * cosYaw + sinRoll * sinPitch * sinYaw).ToNaomiQuaternion();
        }
        public static nVector3 ToEulerAngles(this nQuaternion _quaternion)
        {
            //Z
            float sinRollCosPitch = 2 * (_quaternion.w * _quaternion.x + _quaternion.y * _quaternion.z);
            float cosRollCosPitch = 1 - 2 * (_quaternion.x * _quaternion.x + _quaternion.y * _quaternion.y);

            //X
            float sinPitch = 2 * (_quaternion.w * _quaternion.y - _quaternion.z * _quaternion.x);

            //Y
            float sinYawCosPitch = 2 * (_quaternion.w * _quaternion.z + _quaternion.x * _quaternion.y);
            float cosYawCosPitch = 1 - 2 * (_quaternion.y * _quaternion.y + _quaternion.z * _quaternion.z);

            return new nVector3(Mathf.Atan2(sinRollCosPitch, cosRollCosPitch),
                               (Mathf.Abs(sinPitch) >= 1) ? Mathf.Sign(Mathf.Abs(_quaternion.w) - Mathf.Abs(_quaternion.y)) * Mathf.Sign(sinPitch) :
                                                           (Mathf.Sign(Mathf.Abs(_quaternion.w) - Mathf.Abs(_quaternion.y)) < 0 ? ((((Mathf.Sign(sinPitch) * 2) - 1) * Mathf.PI / 2) + Mathf.Abs(Mathf.Asin(sinPitch) - (Mathf.PI / 2))) : Mathf.Asin(sinPitch)), //RESOLVED: Converting a Quaternion back to an Euler angle only applies to numbers between -PI/2 and PI/2. The additional code here reclamps the conversion to be between -PI and PI.
                                Mathf.Atan2(sinYawCosPitch, cosYawCosPitch));
        }


        public static Quaternion ToUnityQuaternion(this nMatrix _matrix)
        {
            float s;
            if ((1 + _matrix.values[0, 0] + _matrix.values[1, 1] + _matrix.values[2, 2]) > 0)
            {
                s = Mathf.Sqrt(1 + _matrix.values[0, 0] + _matrix.values[1, 1] + _matrix.values[2, 2]) * 2;
                return new Quaternion((_matrix.values[1, 2] - _matrix.values[2, 1]) / s, (_matrix.values[2, 0] - _matrix.values[0, 2]) / s, (_matrix.values[0, 1] - _matrix.values[1, 0]) / s, 0.25f * s);
            }

            if (_matrix.values[0, 0] > _matrix.values[1, 1] && _matrix.values[0, 0] > _matrix.values[2, 2])
            {
                s = Mathf.Sqrt(1 + _matrix.values[0, 0] - _matrix.values[1, 1] - _matrix.values[2, 2]) * 2;
                return new Quaternion(0.25f * s, (_matrix.values[0, 1] + _matrix.values[1, 0]) / s, (_matrix.values[2, 0] + _matrix.values[0, 2]) / s, (_matrix.values[1, 2] - _matrix.values[2, 1]) / s);
            }
            else if (_matrix.values[1, 1] > _matrix.values[2, 2])
            {
                s = Mathf.Sqrt(1 + _matrix.values[1, 1] - _matrix.values[0, 0] - _matrix.values[2, 2]) * 2;
                return new Quaternion((_matrix.values[0, 1] + _matrix.values[1, 0]) / s, 0.25f * s, (_matrix.values[1, 2] + _matrix.values[2, 1]) / s, (_matrix.values[2, 0] - _matrix.values[0, 2]) / s);
            }
            else
            {
                s = Mathf.Sqrt(1 + _matrix.values[2, 2] - _matrix.values[0, 0] - _matrix.values[5, 5]) * 2;
                return new Quaternion((_matrix.values[2, 0] + _matrix.values[0, 2]) / s, (_matrix.values[1, 2] + _matrix.values[2, 1]) / s, 0.25f * s, (_matrix.values[0, 1] - _matrix.values[1, 0]) / s);
            }
        }
    }
}