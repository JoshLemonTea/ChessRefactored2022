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
                CurrentState.OnEnter();
            }
        }

        public void Register(string stateName, GameState state)
        {
            state.StateMachine = this;
            _states.Add(stateName, state);
        }

        public void MoveTo(string stateName)
        {
            CurrentState?.OnExit();

            _currentStateNames[_currentStateNames.Count - 1] = stateName;

            CurrentState?.OnEnter();
        }

        public void Push(string stateName)
        {
            CurrentState?.OnSuspend();

            _currentStateNames.Add(stateName);

            CurrentState?.OnEnter();
        }

        public void Pop()
        {
            CurrentState?.OnExit();

            _currentStateNames.RemoveAt(_currentStateNames.Count - 1);

            CurrentState?.OnResume();
        }



        public GameState State(string stateName) => _states[stateName];
    }
}
