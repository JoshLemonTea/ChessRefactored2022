using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.GameStates
{
    internal class GameStateMachine
    {

        private Dictionary<string, GameState> _states = new Dictionary<string, GameState>();

        private List<string> _currentStateNames = new List<string>();

        public string CurrentStateName => _currentStateNames[_currentStateNames.Count - 1];

        public GameState CurrentState => _states[CurrentStateName];

        public string InitialState
        {
            set
            {
                _currentStateNames.Clear();
                _currentStateNames.Add(value);
            }
        }

        public async UniTask Start()
        {
            await CurrentState.OnEnter();
            await CurrentState.OnResume();
        }

        public void Register(string stateName, GameState state)
        {
            state.StateMachine = this;
            _states.Add(stateName, state);
        }

        public async UniTask MoveTo(string stateName)
        {
            if (CurrentState != null)
            {
                await CurrentState.OnSuspend();
                await CurrentState.OnExit();
            }

            _currentStateNames[_currentStateNames.Count - 1] = stateName;

            if (CurrentState != null)
            {
                await CurrentState.OnEnter();
                await CurrentState.OnResume();
            }
        }

        public async UniTask Push(string stateName)
        {
            if(CurrentState != null)
                await CurrentState.OnSuspend();

            _currentStateNames.Add(stateName);

            if (CurrentState != null)
            {
                await CurrentState.OnEnter();
                await CurrentState.OnResume();
            }
        }

        public async UniTask Pop()
        {
            if (CurrentState != null)
            {
                await CurrentState.OnSuspend();
                await CurrentState.OnExit();
            }

            _currentStateNames.RemoveAt(_currentStateNames.Count - 1);

            if(CurrentState != null)
                await CurrentState.OnResume();
        }



        public GameState State(string stateName) => _states[stateName];
    }
}
