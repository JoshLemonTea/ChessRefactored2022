using BoardSystem;
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

        public virtual void OnEnter() {}

        public virtual void OnExit() {}

    }
}
