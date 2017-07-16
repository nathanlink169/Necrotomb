using UnityEngine;
using System.Collections.Generic;

namespace GameFramework
{
    public class GEntityFactory : SingletonBehaviour<GEntityFactory>
    {
        #region Public
        public GameObject CreateResourceAtPath(string in_path, Transform in_parent = null, bool in_overridePos = true)
        {
            Object tempToReturn = GetResourceAtPath(in_path);
            GameObject toReturn = null;
            if (tempToReturn != null)
            {
                toReturn = GameObject.Instantiate(tempToReturn, in_parent) as GameObject;
                if (in_overridePos) toReturn.transform.localPosition = Vector3.zero;
            }

            return toReturn;
        }

        public Object GetResourceAtPath(string in_path)
        {
            Object tempItem = (Object)LazyLoadResourceAtPath(in_path);
            if (tempItem != null)
            {
                return tempItem;
            }
            return null;
        }

        public bool RemoveReferencedResource(string in_path, bool in_flushAssets = false)
        {
            bool bToReturn = false;
            if (m_factoryLookup.ContainsKey(in_path))
            {
                m_factoryLookup[in_path] = null;
                if (in_flushAssets)
                {
                    Resources.UnloadUnusedAssets();
                }
                bToReturn = true;
            }

            return bToReturn;
        }

        public void UnloadAllReferencedAssets()
        {
            GPoolManager.Instance.DestroyAllPools();

            m_factoryLookup.Clear();

            // drain assets
            Resources.UnloadUnusedAssets();
        }
        #endregion

        #region Private
        private Object LazyLoadResourceAtPath(string in_toLoad)
        {
            Object toReturn = null;
            bool bContainsKey = m_factoryLookup.ContainsKey(in_toLoad);
            if (!bContainsKey || m_factoryLookup[in_toLoad] == null)
            {
                m_factoryLookup[in_toLoad] = Resources.Load(in_toLoad);
                toReturn = m_factoryLookup[in_toLoad];
            }
            else if (bContainsKey)
            {
                toReturn = m_factoryLookup[in_toLoad];
            }
            return (Object)toReturn;
        }

        private Dictionary<string, Object> m_factoryLookup = new Dictionary<string, Object>();
        #endregion
    }
}
