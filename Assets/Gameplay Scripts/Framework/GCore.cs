using UnityEngine;
using System.Collections;

namespace GameFramework
{
    public class GCore : SingletonBehaviour<GCore>
    {
        #region Publics
        public bool IsInitialized
        {
            get { return m_bInitialized; }
        }
        public void EnsureMgrsAreSetup()
        {
            if (!IsInitialized && !m_bStarted)
            {
                m_bStarted = true;
                StartCoroutine(StartUpMgrs());
            }
        }
        public void SetCursorLockmode(bool in_bLocked)
        {
            Cursor.lockState = in_bLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !in_bLocked;
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        #endregion

        #region Singleton
        void Start()
        {
            EnsureMgrsAreSetup();
        }

        public void OnApplicationPause(bool aIsPaused)
        {
            if (aIsPaused)
            {
                PlayerPrefs.Save();
            }
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();

        }
        #endregion

        #region Private
        private IEnumerator StartUpMgrs()
        {
            Physics.queriesHitTriggers = false;
            yield return YieldFactory.GetWaitForEndOfFrame();

            GEventManager.Instance.EmptyStartup();
            yield return YieldFactory.GetWaitForEndOfFrame();

            GPlayerHealth.Instance.EmptyStartup();
            yield return YieldFactory.GetWaitForEndOfFrame();

            AudioMixerManager.Instance.EmptyStartup();
            yield return YieldFactory.GetWaitForEndOfFrame();

            m_bInitialized = true;
        }

        private bool m_bInitialized = false;
        private bool m_bStarted = false;
        #endregion
    }
}