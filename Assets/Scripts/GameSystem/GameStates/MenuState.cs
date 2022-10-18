using Cysharp.Threading.Tasks;
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

        public override async UniTask OnEnter()
        {
            await base.OnEnter();
            await SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            _menuView = GameObject.FindObjectOfType<MenuView>(true);
            
        }

        public override async UniTask OnResume()
        {
            await base.OnResume();
            if (_menuView != null)
            {
                _menuView.StartClicked += async (s, e) => await StateMachine.MoveTo(PlayingState.Name);
                _menuView.Show();
            }
        }

        public override async UniTask OnSuspend()
        {
            await base.OnSuspend();
            if (_menuView != null)
            {
                _menuView.StartClicked += async (s, e) => await StateMachine.MoveTo(PlayingState.Name);
                _menuView.Hide();
            }
        }

        public override async UniTask OnExit()
        {
            await base.OnExit();
            await SceneManager.UnloadSceneAsync(1);
        }
    }
}
