using UnityEngine;

namespace DR.Framework.FSM
{
    public interface IState
    {
        bool CanEnterState { get; }
        
        bool CanExitState { get; }
        
        void OnEnterState();
        
        void OnExitState();
    }
}

