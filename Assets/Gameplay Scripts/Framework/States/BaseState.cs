using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GameFramework
{
    public class BaseState : BaseBehaviour
    {
        // ensure to set this during the awake or start 
        #region Delegates
        public GStateManager.PauseDelegate OnUITogglePause;
        public GStateManager.PauseDelegate OnUIToggleResume;
        public GStateManager.InitializeDelegate OnInitializeDelegate;
        #endregion

        #region Properties
        public StateInfo StateInfo
        {
            get { return _stateInfo; }
        }
        public new bool IsPaused
        {
            get { return _bPaused; }
        }
        #endregion

        #region MonoBehavior 
        protected virtual void Start()
        {
            // this supports stuff from the editor right away
            GCore.Instance.EmptyStartup();

            if (GCore.Instance.IsInitialized)
            {
                ContinueOnEnter();
            }
            else
            {
                StartCoroutine(SpinUntilManagersAreSetup());
            }
        }

        protected virtual void OnEnter()
        {
            GStateManager stateMgr = GStateManager.Instance;
            stateMgr.OnEnterNewState(_stateInfo);
            stateMgr.RetrieveInitializedDelegate(this);
            if (OnInitializeDelegate != null)
                OnInitializeDelegate(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            m_eventSystem = null;
            Destroy(this.gameObject);
        }
        #endregion

        #region Public Methods
        public void SetUIEnabled(bool in_isEnabled)
        {
            if (m_eventSystem)
                m_eventSystem.enabled = in_isEnabled;
        }

        public void OnPauseState()
        {
            _bPaused = true;
            enabled = false;

            if (OnUITogglePause != null)
                OnUITogglePause();

            SetUIEnabled(false);

            OnPauseStateImpl();
        }

        public void OnResumeState(bool wasPause)
        {
            if (wasPause)
            {
                _bPaused = false;
                enabled = true;

                if (OnUIToggleResume != null)
                    OnUIToggleResume();
            }

            SetUIEnabled(true);

            OnResumeStateImpl(wasPause);
        }

        protected virtual void OnPauseStateImpl() { }
        protected virtual void OnResumeStateImpl(bool wasPaused) { }
        #endregion

        #region Protected Properties
        protected StateInfo _stateInfo = null;

        protected bool _bPaused = false;
        #endregion

        #region Private
        private IEnumerator SpinUntilManagersAreSetup()
        {
            while (!GCore.Instance.IsInitialized)
            {
                yield return YieldFactory.GetWaitForEndOfFrame();
            }

            ContinueOnEnter();
        }

        private void ContinueOnEnter()
        {
            _bPaused = false;
            m_eventSystem = this.transform.GetComponentInChildren<EventSystem>();
            OnEnter();
        }

        private EventSystem m_eventSystem = null;
        #endregion
    }

    #region State Info Helper Class
    public class StateInfo
    {
        // using string as an ID in this case because using a value 
        // may change after editor build changes
        public StateInfo(string in_id, BaseState in_state)
        {
            m_state = in_state;
            m_stateId = in_id;
        }

        // public getters 
        public string StateId
        {
            get { return m_stateId; }
        }
        public BaseState State
        {
            get { return m_state; }
        }

        private string m_stateId = GStateManager.UNDEFINED_STATE;
        private BaseState m_state = null;

        public void Cleanup()
        {
            if (m_state != null) GameObject.Destroy(m_state);
            m_state = null;

            if (!string.IsNullOrEmpty(m_stateId) && m_stateId != GStateManager.UNDEFINED_STATE)
                SceneManager.UnloadSceneAsync(m_stateId);
            m_stateId = GStateManager.UNDEFINED_STATE;
        }
    }
    #endregion
}
