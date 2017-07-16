using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class BaseBehaviour : MonoBehaviour
    {
        #region Public Accessors
        public CharacterController CharacterController { get { return GetComponent<CharacterController>(); } }
        public Rigidbody Rigidbody { get { return GetComponent<Rigidbody>(); } }

        public Vector3 vWorldPosition { get { return transform.position; } set { transform.position = value; } }
        public Vector3 vLocalPosition { get { return transform.localPosition; } set { transform.localPosition = value; } }
        public Vector3 vForward { get { return transform.forward; } set { transform.forward = value; } }
        public Quaternion qWorldRotation { get { return transform.rotation; } set { transform.rotation = value; } }
        public Quaternion qLocalRotaiton { get { return transform.localRotation; } set { transform.localRotation = value; } }
        public Vector3 vWorldEulerAngles { get { return transform.eulerAngles; } set { transform.eulerAngles = value; } }
        public Vector3 vLocalEulerAngles { get { return transform.localEulerAngles; } set { transform.localEulerAngles = value; } }
        #endregion

        #region Public Extenders
        public T FindComponentInParents<T>() where T : Component
        {
            T comp = null;

            Transform t = transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }

        public T[] FindComponenstInParents<T>() where T : Component
        {
            List<T> comps = new List<T>();

            Transform t = transform.parent;
            while (t != null)
            {
                T comp = t.gameObject.GetComponent<T>();
                if (comp != null)
                {
                    comps.Add(comp);
                }
                t = t.parent;
            }
            return comps.ToArray();
        }
        #endregion

        #region public 
        public BaseState CurrentState
        {
            get
            {
                if (m_CurrentState == null)
                {
                    m_CurrentState = transform.root.GetComponent<BaseState>();
                }
                return m_CurrentState;
            }
        }
        public bool IsPaused { get { return CurrentState.IsPaused; } }
        #endregion

        #region Protected Mono
        private BaseState m_CurrentState;
        protected virtual void OnDestroy()
        {
            CancelInvoke();
            StopAllCoroutines();
        }
        #endregion
    }
}

