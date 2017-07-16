using UnityEngine;

namespace GameFramework
{
    public static class AIUtils
    {
        public static Vector3 GetClosestPointOnNavMesh(Vector3 aPoint)
        {
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(aPoint, out hit, 1000, -1);
            return hit.position;
        }
    }
}