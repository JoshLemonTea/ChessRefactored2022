using BoardSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    internal class PlayingState : GameState
    {
        public const string Name = "Playing";
        private BoardView _boardView;

        public override void OnEnter()
        {
            var loading = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            loading.completed += OnSceneLoaded;
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(2);
        }

        private void OnSceneLoaded(AsyncOperation obj)
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();  
            if(_boardView != null)
            {
                _boardView.PositionSelected += (s,e) => StateMachine.MoveTo(MenuState.Name);
            }
        }
    }
}
