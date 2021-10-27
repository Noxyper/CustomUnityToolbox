using System;
using UnityEngine;

namespace nMath
{
    public class nMatrix
    {
        public float[,] values;

        #region Properties
        public static nMatrix Identity
        {
            get
            {
                return new nMatrix(new nVector4(1, 0, 0, 0),
                                   new nVector4(0, 1, 0, 0),
                                   new nVector4(0, 0, 1, 0),
                                   new nVector4(0, 0, 0, 1));
            }
        }
        #endregion

        #region Constructors
        public nMatrix(nMatrix _matrix) 
        { 
            values = new float[4, 4];

            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    values[x, y] = _matrix.values[x, y];
                }
            }
        }
        public nMatrix(nVector4 _column1, nVector4 _column2, nVector4 _column3, nVector4 _column4)
        {
            values = new float[4, 4];

            values[0, 0] = _column1.x;
            values[1, 0] = _column1.y;
            values[2, 0] = _column1.z;
            values[3, 0] = _column1.w;

            values[0, 1] = _column2.x;
            values[1, 1] = _column2.y;
            values[2, 1] = _column2.z;
            values[3, 1] = _column2.w;

            values[0, 2] = _column3.x;
            values[1, 2] = _column3.y;
            values[2, 2] = _column3.z;
            values[3, 2] = _column3.w;

            values[0, 3] = _column4.x;
            values[1, 3] = _column4.y;
            values[2, 3] = _column4.z;
            values[3, 3] = _column4.w;
        }

        public nMatrix(nVector3 _column1, nVector3 _column2, nVector3 _column3, nVector3 _column4)
        {
            values = new float[4, 4];

            values[0, 0] = _column1.x;
            values[1, 0] = _column1.y;
            values[2, 0] = _column1.z;
            values[3, 0] = 0;

            values[0, 1] = _column2.x;
            values[1, 1] = _column2.y;
            values[2, 1] = _column2.z;
            values[3, 1] = 0;

            values[0, 2] = _column3.x;
            values[1, 2] = _column3.y;
            values[2, 2] = _column3.z;
            values[3, 2] = 0;

            values[0, 3] = _column4.x;
            values[1, 3] = _column4.y;
            values[2, 3] = _column4.z;
            values[3, 3] = 1;
        }
        #endregion

        #region Operators
        public static nVector4 operator *(nMatrix _matrix, nVector4 _vector4)
        {
            return new nVector4()
            {
                x = (_matrix.values[0, 0] * _vector4.x) + (_matrix.values[0, 1] * _vector4.y) + (_matrix.values[0, 2] * _vector4.z) + (_matrix.values[0, 3] * _vector4.w),
                y = (_matrix.values[1, 0] * _vector4.x) + (_matrix.values[1, 1] * _vector4.y) + (_matrix.values[1, 2] * _vector4.z) + (_matrix.values[1, 3] * _vector4.w),
                z = (_matrix.values[2, 0] * _vector4.x) + (_matrix.values[2, 1] * _vector4.y) + (_matrix.values[2, 2] * _vector4.z) + (_matrix.values[2, 3] * _vector4.w),
                w = (_matrix.values[3, 0] * _vector4.x) + (_matrix.values[3, 1] * _vector4.y) + (_matrix.values[3, 2] * _vector4.z) + (_matrix.values[3, 3] * _vector4.w)
            };
        }
        public static nVector4 operator *(nVector4 _vector4, nMatrix _matrix)
        {
            return new nVector4()
            {
                x = (_matrix.values[0, 0] * _vector4.x) + (_matrix.values[0, 1] * _vector4.y) + (_matrix.values[0, 2] * _vector4.z) + (_matrix.values[0, 3] * _vector4.w),
                y = (_matrix.values[1, 0] * _vector4.x) + (_matrix.values[1, 1] * _vector4.y) + (_matrix.values[1, 2] * _vector4.z) + (_matrix.values[1, 3] * _vector4.w),
                z = (_matrix.values[2, 0] * _vector4.x) + (_matrix.values[2, 1] * _vector4.y) + (_matrix.values[2, 2] * _vector4.z) + (_matrix.values[2, 3] * _vector4.w),
                w = (_matrix.values[3, 0] * _vector4.x) + (_matrix.values[3, 1] * _vector4.y) + (_matrix.values[3, 2] * _vector4.z) + (_matrix.values[3, 3] * _vector4.w)
            };
        }
        public static nVector3 operator *(nMatrix _matrix, nVector3 _vector3)
        {
            return new nVector3()
            {
                x = (_matrix.values[0, 0] * _vector3.x) + (_matrix.values[0, 1] * _vector3.y) + (_matrix.values[0, 2] * _vector3.z) + (_matrix.values[0, 3] * 1),
                y = (_matrix.values[1, 0] * _vector3.x) + (_matrix.values[1, 1] * _vector3.y) + (_matrix.values[1, 2] * _vector3.z) + (_matrix.values[1, 3] * 1),
                z = (_matrix.values[2, 0] * _vector3.x) + (_matrix.values[2, 1] * _vector3.y) + (_matrix.values[2, 2] * _vector3.z) + (_matrix.values[2, 3] * 1)
            };
        }
        public static nVector3 operator *(nVector3 _vector3, nMatrix _matrix)
        {
            return new nVector3()
            {
                x = (_matrix.values[0, 0] * _vector3.x) + (_matrix.values[0, 1] * _vector3.y) + (_matrix.values[0, 2] * _vector3.z) + (_matrix.values[0, 3] * 1),
                y = (_matrix.values[1, 0] * _vector3.x) + (_matrix.values[1, 1] * _vector3.y) + (_matrix.values[1, 2] * _vector3.z) + (_matrix.values[1, 3] * 1),
                z = (_matrix.values[2, 0] * _vector3.x) + (_matrix.values[2, 1] * _vector3.y) + (_matrix.values[2, 2] * _vector3.z) + (_matrix.values[2, 3] * 1)
            };
        }

        public static nMatrix operator *(nMatrix _a, nMatrix _b)
        {
            //row * column

            return new nMatrix(
                new nVector4() //Column 1
                {
                    x = ((_a.values[0, 0] * _b.values[0, 0]) + (_a.values[0, 1] * _b.values[1, 0]) + (_a.values[0, 2] * _b.values[2, 0]) + (_a.values[0, 3] * _b.values[3, 0])),
                    y = ((_a.values[1, 0] * _b.values[0, 0]) + (_a.values[1, 1] * _b.values[1, 0]) + (_a.values[1, 2] * _b.values[2, 0]) + (_a.values[1, 3] * _b.values[3, 0])),
                    z = ((_a.values[2, 0] * _b.values[0, 0]) + (_a.values[2, 1] * _b.values[1, 0]) + (_a.values[2, 2] * _b.values[2, 0]) + (_a.values[2, 3] * _b.values[3, 0])),
                    w = ((_a.values[3, 0] * _b.values[0, 0]) + (_a.values[3, 1] * _b.values[1, 0]) + (_a.values[3, 2] * _b.values[2, 0]) + (_a.values[3, 3] * _b.values[3, 0]))
                },
                new nVector4() //Column 2
                {
                    x = ((_a.values[0, 0] * _b.values[0, 1]) + (_a.values[0, 1] * _b.values[1, 1]) + (_a.values[0, 2] * _b.values[2, 1]) + (_a.values[0, 3] * _b.values[3, 1])),
                    y = ((_a.values[1, 0] * _b.values[0, 1]) + (_a.values[1, 1] * _b.values[1, 1]) + (_a.values[1, 2] * _b.values[2, 1]) + (_a.values[1, 3] * _b.values[3, 1])),
                    z = ((_a.values[2, 0] * _b.values[0, 1]) + (_a.values[2, 1] * _b.values[1, 1]) + (_a.values[2, 2] * _b.values[2, 1]) + (_a.values[2, 3] * _b.values[3, 1])),
                    w = ((_a.values[3, 0] * _b.values[0, 1]) + (_a.values[3, 1] * _b.values[1, 1]) + (_a.values[3, 2] * _b.values[2, 1]) + (_a.values[3, 3] * _b.values[3, 1]))
                },
                new nVector4() //Column 3
                {
                    x = ((_a.values[0, 0] * _b.values[0, 2]) + (_a.values[0, 1] * _b.values[1, 2]) + (_a.values[0, 2] * _b.values[2, 2]) + (_a.values[0, 3] * _b.values[3, 2])),
                    y = ((_a.values[1, 0] * _b.values[0, 2]) + (_a.values[1, 1] * _b.values[1, 2]) + (_a.values[1, 2] * _b.values[2, 2]) + (_a.values[1, 3] * _b.values[3, 2])),
                    z = ((_a.values[2, 0] * _b.values[0, 2]) + (_a.values[2, 1] * _b.values[1, 2]) + (_a.values[2, 2] * _b.values[2, 2]) + (_a.values[2, 3] * _b.values[3, 2])),
                    w = ((_a.values[3, 0] * _b.values[0, 2]) + (_a.values[3, 1] * _b.values[1, 2]) + (_a.values[3, 2] * _b.values[2, 2]) + (_a.values[3, 3] * _b.values[3, 2]))
                },
                new nVector4() //Column 4
                {
                    x = ((_a.values[0, 0] * _b.values[0, 3]) + (_a.values[0, 1] * _b.values[1, 3]) + (_a.values[0, 2] * _b.values[2, 3]) + (_a.values[0, 3] * _b.values[3, 3])),
                    y = ((_a.values[1, 0] * _b.values[0, 3]) + (_a.values[1, 1] * _b.values[1, 3]) + (_a.values[1, 2] * _b.values[2, 3]) + (_a.values[1, 3] * _b.values[3, 3])),
                    z = ((_a.values[2, 0] * _b.values[0, 3]) + (_a.values[2, 1] * _b.values[1, 3]) + (_a.values[2, 2] * _b.values[2, 3]) + (_a.values[2, 3] * _b.values[3, 3])),
                    w = ((_a.values[3, 0] * _b.values[0, 3]) + (_a.values[3, 1] * _b.values[1, 3]) + (_a.values[3, 2] * _b.values[2, 3]) + (_a.values[3, 3] * _b.values[3, 3]))
                });
        }
        #endregion

        public nMatrix ScaleInverse()
        {
            nMatrix tempMatrix = Identity;
            tempMatrix.values[0, 0] = 1f / values[0, 0];
            tempMatrix.values[1, 1] = 1f / values[1, 1];
            tempMatrix.values[2, 2] = 1f / values[2, 2];

            return tempMatrix;
        }
        public static nMatrix Scale(nVector3 _scales) => new nMatrix(new nVector3(_scales.x, 0, 0),
                                                                     new nVector3(0, _scales.y, 0),
                                                                     new nVector3(0, 0, _scales.z),
                                                                     nVector3.Zero);

        public nMatrix ShearInverse()
        {
            nMatrix tempMatrix = Identity;

            tempMatrix.values[1, 0] = -values[1, 0];
            tempMatrix.values[0, 1] = -values[0, 1];

            tempMatrix.values[2, 0] = -values[2, 0];
            tempMatrix.values[0, 2] = -values[0, 2];

            tempMatrix.values[2, 1] = -values[2, 1];
            tempMatrix.values[1, 2] = -values[1, 2];

            return tempMatrix;
        }
        public static nMatrix Shear(nVector3 _xShear, nVector3 _yShear, nVector3 _zShear) => new nMatrix(new nVector3(1, Mathf.Tan(Mathf.Clamp(_zShear.x, -Mathf.PI / 4, Mathf.PI / 4)), Mathf.Tan(Mathf.Clamp(_yShear.x, -Mathf.PI / 4, Mathf.PI / 4))),
                                                                                                        new nVector3(Mathf.Tan(Mathf.Clamp(_zShear.y, -Mathf.PI / 4, Mathf.PI / 4)), 1, Mathf.Tan(Mathf.Clamp(_xShear.y, -Mathf.PI / 4, Mathf.PI / 4))),
                                                                                                        new nVector3(Mathf.Tan(Mathf.Clamp(_yShear.z, -Mathf.PI / 4, Mathf.PI / 4)), Mathf.Tan(Mathf.Clamp(_xShear.z, -Mathf.PI / 4, Mathf.PI / 4)), 1),
                                                                                                        nVector3.Zero);

        public nMatrix RotateInverse() => new nMatrix(GetRow(0), GetRow(1), GetRow(2), GetRow(3));
        public static nMatrix Rotate(nVector3 _eulerAngles) => new nMatrix(new nMatrix(new nVector3(Mathf.Cos(_eulerAngles.y), 0, Mathf.Sin(_eulerAngles.y)),           //Yaw Matrix
                                                                                       nVector3.Up,
                                                                                       new nVector3(-Mathf.Sin(_eulerAngles.y), 0, Mathf.Cos(_eulerAngles.y)),
                                                                                       nVector3.Zero) *
                                                                          (new nMatrix(nVector3.Right,                                                                  //Pitch Matrix
                                                                                       new nVector3(0, Mathf.Cos(_eulerAngles.x), -Mathf.Sin(_eulerAngles.x)),
                                                                                       new nVector3(0, Mathf.Sin(_eulerAngles.x), Mathf.Cos(_eulerAngles.x)),
                                                                                       nVector3.Zero) *
                                                                           new nMatrix(new nVector3(Mathf.Cos(_eulerAngles.z), -Mathf.Sin(_eulerAngles.z), 0),          //Roll Matrix
                                                                                       new nVector3(Mathf.Sin(_eulerAngles.z), Mathf.Cos(_eulerAngles.z), 0),
                                                                                       nVector3.Forward,
                                                                                       nVector3.Zero)));
        public static nMatrix Rotate(nQuaternion _quaternion) => new nMatrix(new nVector3(2 * ((_quaternion.w * _quaternion.w) + (_quaternion.x * _quaternion.x)) - 1,
                                                                                          2 * ((_quaternion.x * _quaternion.y) - (_quaternion.w * _quaternion.z)),        
                                                                                          2 * ((_quaternion.x * _quaternion.z) + (_quaternion.w * _quaternion.y))),

                                                                             new nVector3(2 * ((_quaternion.x * _quaternion.y) + (_quaternion.w * _quaternion.z)),      
                                                                                          2 * ((_quaternion.w * _quaternion.w) + (_quaternion.y * _quaternion.y)) - 1,    
                                                                                          2 * ((_quaternion.y * _quaternion.z) - (_quaternion.w * _quaternion.x))),

                                                                             new nVector3(2 * ((_quaternion.x * _quaternion.z) - (_quaternion.w * _quaternion.y)),      
                                                                                          2 * ((_quaternion.y * _quaternion.z) + (_quaternion.w * _quaternion.x)),        
                                                                                          2 * ((_quaternion.w * _quaternion.w) + (_quaternion.z * _quaternion.z)) - 1), 
                                                                             nVector3.Zero);

        public nMatrix TranslateInverse()
        {
            nMatrix tempMatrix = Identity;
            tempMatrix.values[0, 3] = -values[0, 3];
            tempMatrix.values[1, 3] = -values[1, 3];
            tempMatrix.values[2, 3] = -values[2, 3];

            return tempMatrix;
        }
        public static nMatrix Translate(nVector3 _position) => new nMatrix(nVector3.Right, 
                                                                           nVector3.Up,
                                                                           nVector3.Forward,
                                                                           _position);
        

        public nVector4 GetColumn(int _index)
        {
            return new nVector4(values[0, _index], values[1, _index], values[2, _index], values[3, _index]);
        }
        public nVector4 GetRow(int _index)
        {
            return new nVector4(values[_index, 0], values[_index, 1], values[_index, 2], values[_index, 3]);
        }

        public override string ToString()
        {
            return string.Format("[{0}\t{1}\t{2}\t{3}]\n[{4}\t{5}\t{6}\t{7}]\n[{8}\t{9}\t{10}\t{11}]\n[{12}\t{13}\t{14}\t{15}]",
                                values[0, 0].ToString("0.00"), values[0, 1].ToString("0.00"), values[0, 2].ToString("0.00"), values[0, 3].ToString("0.00"),
                                values[1, 0].ToString("0.00"), values[1, 1].ToString("0.00"), values[1, 2].ToString("0.00"), values[1, 3].ToString("0.00"),
                                values[2, 0].ToString("0.00"), values[2, 1].ToString("0.00"), values[2, 2].ToString("0.00"), values[2, 3].ToString("0.00"),
                                values[3, 0].ToString("0.00"), values[3, 1].ToString("0.00"), values[3, 2].ToString("0.00"), values[3, 3].ToString("0.00"));
        }
    }
}