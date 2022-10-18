using BoardSystem;
using ChessSystem;
using CommandSystem;
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
        
        private readonly CommandQueue _commandQueue;
        private readonly Board<PieceView> _board;
        private readonly Engine<PieceView> _engine;

        private BoardView _boardView;
        private Position? _fromPosition;
        private ReplayView _replayView;

        public PlayingState(CommandQueue commandQueue)
        {
            _commandQueue = commandQueue;

            _board = new Board<PieceView>(PositionHelper.Rows, PositionHelper.Columns);
            _engine = new Engine<PieceView>(_board, _commandQueue);

            _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));

        }

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
                _boardView.PositionSelected += (e, s) => StateMachine.CurrentState.SelectTile(e,s);
            }

            _replayView = GameObject.FindObjectOfType<ReplayView>();
            if (_replayView)
            {
                _replayView.Backward += (e, s) => StateMachine.CurrentState.Backward(e,s);
                _replayView.Forward += (e, s) => StateMachine.CurrentState.Forward(e, s);
                _commandQueue.Changed += (e, s) =>
                {
                    _replayView.CanGoBack = !_commandQueue.IsAtStart;
                    _replayView.CanGoForward = !_commandQueue.IsAtEnd;
                };
            }

            var pieces = GameObject.FindObjectsOfType<PieceView>();
            foreach(var piece in pieces)
                _board.Place(piece, PositionHelper.GridPosition(piece.WorldPosition));
        }


        public override void Backward(object sender, EventArgs e)
        {
            StateMachine.Push(ReplayState.Name);    
        }

        public override void SelectTile(object source, PositionEventArgs eventArgs)
        {
            
            var position = eventArgs.Position;
            if (_engine.TryGetPiece(position, out var piece))
            {
                _fromPosition = position;
                _boardView.SetActivePositions(_engine.MoveSets[piece.Type].Positions(position));
            }
            else if (_fromPosition != null && _engine.Move((Position)_fromPosition, position))
            {
                _boardView.SetActivePositions(new List<Position>());
            }
                
        }
    }
}
