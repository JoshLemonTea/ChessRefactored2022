using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.GameStates
{
    internal class ReplayState : GameState
    {
        public const string Name = "Replay";

        private readonly CommandQueue _commandQueue;

        public ReplayState(CommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public override void OnEnter()
        {
            _commandQueue.Backward();
        }

        public override void OnExit()
        {
            _commandQueue.ToEnd();
        }

        public override void Backward(object sender, EventArgs e)
        {
            _commandQueue.Backward();
        }

        public override void Forward(object sender, EventArgs e)
        {
            _commandQueue.Forward();
            if (_commandQueue.IsAtEnd)
                StateMachine.Pop();
        }
    }
}
