using BoardSystem;
using ChessSystem;
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

        private void OnEnable()
        {
             _engine = new Engine<PieceView>(_board);

            var boardView = FindObjectOfType<BoardView>();
            boardView.PositionSelected += PositionViewSelected;

            var pieceViews = FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(pieceView, PositionHelper.GridPosition(pieceView.WorldPosition));

            _board.PieceMoved += (s,e) => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));
        }

        private void PositionViewSelected(object sender, PositionEventArgs e)
        {
            if (_board.TryGetPiece(e.Position, out var piece))
            {
                var validPositions = _engine.MoveSets[piece.Type].Positions(e.Position);
                if(validPositions.Count > 0)
                    _engine.Move(e.Position,  validPositions[0]);                
            }
        }

    }
}
