using BoardSystem;
using ChessSystem;
using CommandSystem;
using GameSystem.GameStates;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private GameStateMachine _gameSM;


        private void OnEnable()
        {
            var commandQueue = new CommandQueue();


            _gameSM = new GameStateMachine();
            _gameSM.Register(PlayingState.Name, new PlayingState(commandQueue));
            _gameSM.Register(MenuState.Name, new MenuState());
            _gameSM.Register(ReplayState.Name, new ReplayState(commandQueue));
            _gameSM.InitialState = MenuState.Name;
        }
    }
}
