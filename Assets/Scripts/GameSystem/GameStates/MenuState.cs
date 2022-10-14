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
    internal class MenuState : GameState
    {
        public const string Name = "Menu";

        private MenuView _menuView;

        public override void OnEnter()
        {
            var loading = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            loading.completed += OnSceneLoaded;
        }

        public override void OnExit()
        {
            _menuView.Hide();
            SceneManager.UnloadSceneAsync(1);
        }

        

        private void OnSceneLoaded(AsyncOperation obj)
        {
            _menuView = GameObject.FindObjectOfType<MenuView>(true);
            if (_menuView != null)
            {
                _menuView.StartClicked += (s, e) => StateMachine.MoveTo(PlayingState.Name); ;
                _menuView.Show();
            }
        }
    }
}
