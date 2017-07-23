using UnityEngine;
using System.Collections;

namespace GameFramework
{
    public class LoadingScreen : BaseBehaviour
    {
        // Enums.
        public enum eEffect
        {
            Invalid = -1,
            Black,
            BlackFade,

            MaxEffect
        }

        // Constants.
        public const float BLACK_FADE_EFFECT_TIME = 1.5f;
        // Delegates.
        public delegate void LoadingScreenDelegate();

        #region Public
        public Transform BaseObject = null;

        // Public methods.
        public void LoadLevel(string in_loadingLevelName, bool in_isLoadingAdditive, eEffect in_effect, LoadingScreenDelegate in_onLoadingBegin, LoadingScreenDelegate in_onLoadingEnd)
        {
            m_sLoadingLevelName = in_loadingLevelName;
            m_bLoadingAdditive = in_isLoadingAdditive;

            m_onLoadingBegin = in_onLoadingBegin;
            m_onLoadingEnd = in_onLoadingEnd;

            m_eEffect = in_effect;

            switch (m_eEffect)
            {
                case eEffect.Black:
                    {
                        BaseObject.gameObject.SetActive(true);
                        ChangeLoadingState();
                    }
                    break;
                case eEffect.Invalid:
                    {
                        ChangeLoadingState();
                    }
                    break;
                case eEffect.BlackFade:
                    //GOverlayMgr.GetInstance().SetEffect( new GOverlayMgr.EffectInfo(GOverlayMgr.eEffect.FadeInLoading, BLACK_FADE_EFFECT_TIME, Color.black, 0.0f) );
                    ChangeState(eState.BlackFadeIn);
                    break;
            }
        }
        #endregion

        #region Monobehaviour
        private void Update()
        {
            m_fStateTimer += Time.deltaTime;

            switch (m_eState)
            {
                case eState.LoadingDummy:
                    {
                        if (m_iDummyTick == 0)
                        {
                            // One tick for the update to finish.
                        }
                        else if (m_iDummyTick == 1)
                        {
                            // Unload entity factory resources.
                            GEntityFactory.Instance.UnloadAllReferencedAssets();
                        }
                        else if (m_iDummyTick == 2)
                        {
                            // Do a garbage collector collect.
                            System.GC.Collect();
                        }
                        else if (m_iDummyTick == 3)
                        {
                            // Load the next state.
                            ChangeState(eState.Loading);
                        }
                        ++m_iDummyTick;
                    }
                    break;

                case eState.BlackFadeIn:
                    {
                        // Wait for overlay to be done.
                        //if( GOverlayMgr.GetInstance().IsEffectDone() )
                        {
                            ChangeState(eState.LoadingDummy);
                        }

                    }
                    break;

                case eState.Loading:
                    {
                    }
                    break;

                default:
                    break;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (BaseObject) Destroy(BaseObject.gameObject);
        }
        #endregion
        private void ChangeState(eState in_state)
        {
            if (gameObject.activeInHierarchy == false)
                gameObject.SetActive(true);
            StartCoroutine(StateChangeAfterTick(in_state));
        }

        private IEnumerator StateChangeAfterTick(eState in_state)
        {
            yield return YieldFactory.GetWaitForEndOfFrame();
            State = in_state;
        }

        // Accessors.
        private eState State
        {
            set
            {
                s_canHide = false;
                m_eState = value;
                m_fStateTimer = 0.0f;

                switch (m_eState)
                {
                    case eState.Invalid:
                        {
                            Hide();
                        }
                        break;

                    case eState.LoadingDummy:
                        {

                            //UnityEngine.SceneManagement.SceneManager.LoadScene("dummy");
                            m_iDummyTick = 0;
                        }
                        break;

                    case eState.Loading:
                        {
                            m_fStartLoadingTime = Time.realtimeSinceStartup;

                            // Begin loading callback.
                            if (m_onLoadingBegin != null)
                                m_onLoadingBegin();

                            m_onLoadingBegin = null;
#if ENABLE_ASSET_BUNDLES
                            string assetBundleTag = GHelper.Instance.GetAssetBundleForSceneName(m_loadingLevelName);
                            if ( assetBundleTag != "")
                            StartLoadingAssetBundleLevelNow(assetBundleTag, m_loadingLevelName); 
                            else
#endif
                            {
                                StartLoadingNow(m_sLoadingLevelName);
                            }
                        }
                        break;

                    case eState.Hide:
                        s_canHide = true;
                        Invoke("DelayedDoHide", HIDE_DELAY);
                        break;
                }
            }
        }

        private void DelayedDoHide()
        {
            if (s_canHide)
                DoHide();
        }

        private void StartLoadingNow(string in_name)
        {
            // Load next level.
            AsyncOperation loadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(in_name, m_bLoadingAdditive ?
                UnityEngine.SceneManagement.LoadSceneMode.Additive : UnityEngine.SceneManagement.LoadSceneMode.Single);

            if (Application.HasProLicense())
            {
                StartCoroutine(LoadLevelProgressProLicense(loadingOperation));
            }
            else
            {
                StartCoroutine(LoadLevelProgress(loadingOperation));
            }
        }
#if ENABLE_ASSET_BUNDLES
        private void StartLoadingAssetBundleLevelNow (string in_assetBundleTag, string in_name) 
        {
        // Load next level.
        AssetBundles.AssetBundleLoadOperation loadingOperation = AssetBundles.AssetBundleManager.LoadLevelAsync(in_assetBundleTag, in_name, m_isLoadingAdditive);

        if ( Application.HasProLicense() )
        {
        StartCoroutine( LoadAssetLevelProgress(loadingOperation) );
        }
        else
        {
        TransitionLoadComplete();
        }
        }

        IEnumerator LoadAssetLevelProgress( AssetBundles.AssetBundleLoadOperation in_loadingOperation ) 
        {
        if ( !in_loadingOperation.IsDone() )
        {
            yield return YieldFactory.GetWaitForEndOfFrame();
        }

        // now its done, load the level
#if UNITY_EDITOR
        if ( AssetBundles.AssetBundleManager.SimulateAssetBundleInEditor )
        {
        TransitionLoadComplete();
        }
        else
#endif
        {
            AssetBundles.AssetBundleLoadLevelOperation loadingOperation = (AssetBundles.AssetBundleLoadLevelOperation)in_loadingOperation;
            while ( loadingOperation == null || loadingOperation._Request == null )
                yield return null;

            if (Application.HasProLicense())
            {
                StartCoroutine( LoadLevelProgressProLicense(loadingOperation._Request) );
            }
            else
            {
                StartCoroutine( LoadLevelProgress(loadingOperation._Request) );
            }
        }
    }
#endif

        IEnumerator LoadLevelProgress(AsyncOperation in_loadingOperation)
        {
            float normTime = Mathf.Clamp01((Time.realtimeSinceStartup - m_fStartLoadingTime) / MIN_LOADING_TIME);

            while (normTime < 1.0f)                            // waiting for the time to transition
            {
                normTime = Mathf.Clamp01((Time.realtimeSinceStartup - m_fStartLoadingTime) / MIN_LOADING_TIME);
                yield return YieldFactory.GetWaitForEndOfFrame();
            }

            TransitionLoadComplete();
        }

        IEnumerator LoadLevelProgressProLicense(AsyncOperation in_loadingOperation)
        {
            float normTime = Mathf.Clamp01((Time.realtimeSinceStartup - m_fStartLoadingTime) / MIN_LOADING_TIME);

            while (!in_loadingOperation.isDone ||           // NOT , DONE
                in_loadingOperation.progress < 0.9f ||      // progress less then 90%
                normTime < 1.0f)                            // waiting for the time to transition
            {
                normTime = Mathf.Clamp01((Time.realtimeSinceStartup - m_fStartLoadingTime) / MIN_LOADING_TIME);

                // This is where I'm actually changing the scene
                if (in_loadingOperation.progress >= 0.9f)
                    in_loadingOperation.allowSceneActivation = true;

                yield return YieldFactory.GetWaitForEndOfFrame();
            }

            TransitionLoadComplete();
        }

        private void TransitionLoadComplete()
        {
            switch (m_eEffect)
            {
                case eEffect.Invalid:
                case eEffect.Black:
                case eEffect.BlackFade:
                    {
                        if (m_onLoadingEnd != null)
                            m_onLoadingEnd();

                        m_onLoadingEnd = null;
                        ChangeState(eState.Invalid);
                    }
                    break;
                default:
                    break;
            }
        }

        private void DoHide()
        {
            ChangeState(eState.Invalid);
        }

        private void Hide()
        {
            BaseObject.gameObject.SetActive(false);
        }

        private void ChangeLoadingState()
        {
            if (m_bLoadingAdditive)
                ChangeState(eState.Loading);
            else
                ChangeState(eState.LoadingDummy);
        }

        private const float LOADING_IN_TIME = 0.15f;
        private const float LOADING_OUT_TIME = 0.05f;
        private const float MIN_LOADING_TIME = 0.25f;
        private const float HIDE_DELAY = 1.5f;

        private static bool s_canHide = false;

        // Private members.
        private eState m_eState = eState.Invalid;
        private eEffect m_eEffect = eEffect.Black;

        private float m_fStartLoadingTime = 0.0f;
        private float m_fStateTimer = 0.0f;

        private int m_iDummyTick = 0;
        private bool m_bLoadingAdditive = false;

        private string m_sLoadingLevelName = "";

        private LoadingScreenDelegate m_onLoadingBegin = null;
        private LoadingScreenDelegate m_onLoadingEnd = null;

        private enum eState
        {
            Invalid = -1,
            BlackFadeIn,
            LoadingDummy,
            Loading,
            Hide,

            eStateMax
        }
    }
}
