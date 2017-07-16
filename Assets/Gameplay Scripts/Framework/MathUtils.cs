using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public static class MathUtils
    {
        public static readonly float e = 2.71828182845904523536f;

        public static bool RaycastFirstObjectHit(out RaycastHit aHit, Vector3 aStartingPoint, Vector3 aEndingPoint, List<GameObject> aObjectsToIgnore = null, int aLayerMask = -1)
        {
            RaycastHit[] hits = Physics.RaycastAll(aStartingPoint,
                                        MathUtils.DirectionVector(aStartingPoint, aEndingPoint),
                                        Vector3.Distance(aStartingPoint, aEndingPoint),
                                        aLayerMask,
                                        QueryTriggerInteraction.Ignore);

            for (int i = 0; i < hits.Length; i++)
            {
                if (aObjectsToIgnore != null)
                {
                    if (aObjectsToIgnore.Contains(hits[i].collider.gameObject))
                    {
                        continue;
                    }
                }

                aHit = hits[i];
                return true;
            }

            aHit = new RaycastHit();
            return false;
        }

        public static bool RaycastFirstObjectHit(out RaycastHit aHit, Vector3 aStartingPoint, Vector3 aDirection, float aMaxDistance, List<GameObject> aObjectsToIgnore = null, int aLayerMask = -1)
        {
            RaycastHit[] hits = Physics.RaycastAll(aStartingPoint,
                                                   aDirection,
                                                   aMaxDistance,
                                                   aLayerMask,
                                                   QueryTriggerInteraction.Ignore);

            for (int i = 0; i < hits.Length; i++)
            {
                if (aObjectsToIgnore != null)
                {
                    if (aObjectsToIgnore.Contains(hits[i].collider.gameObject))
                    {
                        continue;
                    }
                }

                aHit = hits[i];
                return true;
            }

            aHit = new RaycastHit();
            return false;
        }

        public static float CompareEpsilon = 0.0001f;

        public static bool IsBetween(float aNum, float aMin, float aMax)
        {
            if (aMin >= aMax)
            {
                float temp = aMin;
                aMin = aMax;
                aMax = temp;
            }

            if (aNum < aMin || aNum > aMax)
                return false;

            return true;
        }

        public static bool AlmostEquals(float v1, float v2, float epsilon)
        {
            return Mathf.Abs(v2 - v1) <= epsilon;
        }

        public static bool AlmostEquals(float v1, float v2)
        {
            return AlmostEquals(v1, v2, CompareEpsilon);
        }

        public static T WeightedRandom<T>(Dictionary<T, float> aOptions)
        {
            // Get the sum of all the weights
            float sum = 0f;

        looprestart:
            foreach (var option in aOptions)
            {
                if (option.Value < 0f)
                {
                    aOptions.Remove(option.Key);
                    sum = 0f;
                    goto looprestart; // Yes, I know I'm using goto's. They get the job done cleanly here. -Nathan
                }
                sum += option.Value;
            }

            // Get a random number between 0 and the sum
            float num = UnityEngine.Random.Range(0f, sum);

            // Subtract weights until num is equal to 0
            foreach (var option in aOptions)
            {
                if (num <= option.Value || MathUtils.AlmostEquals(num, option.Value))
                    return option.Key;
                num -= option.Value;
            }
#if UNITY_EDITOR
            Debug.LogError("Weighted Random Could Not Select An Object");
#endif
            return default(T);
        }

        public static Vector3 Sum(this Vector3[] aVectors)
        {
            Vector3 sum = Vector3.zero;

            for (int i = 0; i < aVectors.Length; i++)
            {
                Vector3 vec = aVectors[i];

                sum += vec;
            }

            return sum;
        }

        public static float Distance(float lhs, float rhs)
        {
            return Mathf.Abs(lhs - rhs);
        }

        public static Vector3 CreateXZVector3(Vector2 aXZ, float aY = 0f)
        {
            return new Vector3(aXZ.x, aY, aXZ.y);
        }

        public static Vector3 DirectionVector(Vector3 aStartPoint, Vector3 aEndPoint)
        {
            return (aEndPoint - aStartPoint).normalized;
        }

        public static Vector3 ToXZVector3(this Vector2 aVector)
        {
            return new Vector3(aVector.x, 0f, aVector.y);
        }
    }
}