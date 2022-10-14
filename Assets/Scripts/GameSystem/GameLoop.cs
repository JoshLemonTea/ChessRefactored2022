using BoardSystem;
using ChessSystem;
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
        private Board<PieceView> _board = new Board<PieceView>(PositionHelper.Rows, PositionHelper.Columns);
        private Engine<PieceView> _engine;

        private GameStateMachine _gameSM;


        private void OnEnable()
        {
            
            _gameSM = new GameStateMachine();
            _gameSM.Register(PlayingState.Name, new PlayingState());
            _gameSM.Register(MenuState.Name, new MenuState());
            _gameSM.InitialState = MenuState.Name;
        }
    }
}
