using UnityEngine;
using System.Collections;

namespace GameFramework
{
    // this is persistent and doesn't get destroyed
    public class SingletonBehaviour<T> : BaseBehaviour
        where T : Component
    {
        public static T Instance
        {
            get { return GetInstance(); }
        }

        public virtual void EmptyStartup()
        {
        }
        #region MonoB
        public virtual void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (m_instance == null)
            {
                m_instance = this as T;
                this.name = this.GetType().Name;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public virtual void OnApplicationQuit()
        {
            // release reference on exit
            m_instance = null;
        }
        #endregion

        #region PRIVATE
        private static T GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
                if (m_instance == null)
                {
                    GameObject obj = new GameObject();
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    m_instance = obj.AddComponent<T>();
                }
            }
            return m_instance;
        }

        private static T m_instance;
        #endregion
    }
}
