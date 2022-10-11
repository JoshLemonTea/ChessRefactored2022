using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessSystem
{
    public class Engine<TPiece>
        where TPiece : IPiece
    {
        private readonly Board<TPiece> _board;
        private Player _currentPlayer;

        public MoveSetCollection<TPiece> MoveSets { get; }

        

        public Engine(Board<TPiece> board)
        {
            _board = board;
            MoveSets = new MoveSetCollection<TPiece>(_board);
            _currentPlayer = Player.Player1;
        }

        public bool Move(Position fromPosition, Position toPosition)
        {
            if (!_board.TryGetPiece(fromPosition, out var piece))
                return false;

            if (piece.Player != _currentPlayer)
                return false;

            if (!MoveSets.TryGetMoveSet(piece.Type, out var moveSet))
                return false;

            if (!moveSet.Positions(fromPosition).Contains(toPosition))
                return false;

            if (!moveSet.Execute(fromPosition, toPosition))
                return false;

           ChangePlayer();

           return true;
        }

        private void ChangePlayer()
            =>  _currentPlayer = (_currentPlayer == Player.Player1) ? Player.Player2 : Player.Player1;        
    }

}
