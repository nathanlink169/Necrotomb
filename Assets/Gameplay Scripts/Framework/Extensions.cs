using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public static class Extensions
    {
        public static float TotalDistance(this UnityEngine.AI.NavMeshPath aPath)
        {
            return TotalDistance(aPath.corners);
        }

        public static float TotalDistance(this Vector3[] aPath)
        {
            float dist = 0f;

            for (int i = 0; i < aPath.Length - 1; i++)
            {
                dist += Vector3.Distance(aPath[i], aPath[i + 1]);
            }

            return dist;
        }

        public static bool IsGrounded(this Rigidbody in_rRigidbody)
        {
            FirstPersonMovement fMovement = in_rRigidbody.GetComponent<FirstPersonMovement>();
            return fMovement == null ? false : fMovement.GetIsGrounded();
        }

        public static int ToInt(this float aFloat)
        {
            return Mathf.RoundToInt(aFloat);
        }

        public static float ToFloat(this int in_iInteger)
        {
            return (float)(in_iInteger);
        }

        public static Vector2 xy(this Vector3 aVector)
        {
            return new Vector2(aVector.x, aVector.y);
        }
        public static Vector2 xz(this Vector3 aVector)
        {
            return new Vector2(aVector.x, aVector.z);
        }
        public static Vector2 yz(this Vector3 aVector)
        {
            return new Vector2(aVector.y, aVector.z);
        }
        public static Vector2 yx(this Vector3 aVector)
        {
            return new Vector2(aVector.y, aVector.x);
        }
        public static Vector2 zx(this Vector3 aVector)
        {
            return new Vector2(aVector.z, aVector.x);
        }
        public static Vector2 zy(this Vector3 aVector)
        {
            return new Vector2(aVector.z, aVector.y);
        }

        public static Vector3 Average(this Vector3[] aVectors)
        {
            return aVectors.Sum() / aVectors.Length;
        }

        public static Vector3 Position(this GameObject aObj)
        {
            return aObj.transform.position;
        }

        public static Quaternion Rotation(this GameObject aObj)
        {
            return aObj.transform.rotation;
        }

        public static Vector3 EulerAngles(this GameObject aObj)
        {
            return aObj.transform.eulerAngles;
        }

        public static Vector3 DirectionVectorRemoveY(this Vector3 aVector)
        {
            Vector3 vec = aVector;

            vec.y = 0f;
            vec.Normalize();

            return vec;
        }

        public static Vector3 XZRemoveY(this Vector3 aVector)
        {
            Vector3 vec = aVector;
            vec.y = 0f;
            return vec;
        }

        public static Transform SetX(this Transform in_transform, float in_x)
        {
            in_transform.position = new Vector3(in_x, in_transform.position.y, in_transform.position.z);
            return in_transform;
        }

        public static Transform SetY(this Transform in_transform, float in_y)
        {
            in_transform.position = new Vector3(in_transform.position.x, in_y, in_transform.position.z);
            return in_transform;
        }

        public static Transform SetZ(this Transform in_transform, float in_z)
        {
            in_transform.position = new Vector3(in_transform.position.x, in_transform.position.y, in_z);
            return in_transform;
        }

        public static Color SetR(this Color in_Colour, float in_value)
        {
            in_Colour.r = in_value;
            return in_Colour;
        }

        public static Color SetG(this Color in_Colour, float in_value)
        {
            in_Colour.g = in_value;
            return in_Colour;
        }

        public static Color SetB(this Color in_Colour, float in_value)
        {
            in_Colour.b = in_value;
            return in_Colour;
        }

        public static Color SetA(this Color in_Colour, float in_value)
        {
            in_Colour.a = in_value;
            return in_Colour;
        }

        public static Rigidbody Rigidbody(this GameObject in_object)
        {
            Rigidbody toReturn = in_object.GetComponent<Rigidbody>();
            if (toReturn == null)
            {
                GDebug.LogError("Trying to access " + in_object + "'s Rigidbody when there is none attached!");
            }
            return toReturn;
        }
    }
}