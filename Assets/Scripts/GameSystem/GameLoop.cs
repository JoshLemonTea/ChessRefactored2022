using GameSystem.Models;
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
        private Board _board = new Board(PositionHelper.Columns, PositionHelper.Columns);

        private void OnEnable()
        {
            var boardView = FindObjectOfType<BoardView>();
            boardView.PositionSelected += PositionViewSelected;

            var pieceViews = FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(pieceView, PositionHelper.GridPosition(pieceView.WorldPosition));

        }

        private void PositionViewSelected(object sender, PositionEventArgs e)
        {
            Debug.Log($"Position Selected: {e.Position}");

            if (_board.TryGetPiece(e.Position, out var piece))
            {
                Debug.Log($"Position contains {piece.Name}");

                var toPosition = new Position(e.Position.X, e.Position.Y + 1);
                _board.Move(e.Position, toPosition);

                piece.transform.position = PositionHelper.WorldPosition(toPosition);

                
            }
        }

    }
}
