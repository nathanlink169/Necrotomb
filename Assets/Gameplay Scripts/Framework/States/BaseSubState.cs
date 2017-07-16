using System;

namespace GameFramework
{
    public class BaseSubState : BaseState
    {
        protected override void OnEnter()
        {
            GStateManager stateMgr = GStateManager.Instance;
            stateMgr.RetrieveInitializedDelegate(this);
            stateMgr.OnEnterNewSubState(_stateInfo);
            if(OnInitializeDelegate != null)
                OnInitializeDelegate(this);
        }
    }
}

