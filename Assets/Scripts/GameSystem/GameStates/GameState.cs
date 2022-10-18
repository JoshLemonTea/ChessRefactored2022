using BoardSystem;
using GameSystem.Views;
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

        public virtual void OnSuspend() {}

        public virtual void OnResume() {}

        public virtual void SelectTile(object source, PositionEventArgs eventArgs) { }

        public virtual void Forward(object sender, EventArgs e) {}

        public virtual void Backward(object sender, EventArgs e) { }
    }
}
