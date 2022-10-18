using BoardSystem;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.GameStates
{
    internal abstract class GameState
    {
        public GameStateMachine StateMachine { get; internal set; }

        
        public virtual  UniTask OnEnter() { return UniTask.CompletedTask; }
        
        public virtual UniTask OnResume() { return UniTask.CompletedTask; }

        public virtual UniTask OnSuspend() { return UniTask.CompletedTask; }

        public virtual  UniTask OnExit() { return UniTask.CompletedTask; }


    }
}
