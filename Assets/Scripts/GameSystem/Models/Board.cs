using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models
{
    public class Board
    {
        private int _rows;
        private int _columns;
        

        private readonly Dictionary<Position, PieceView> _pieces = new Dictionary<Position, PieceView>();

        public Board(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }


        public bool TryGetPiece(Position position, out PieceView piece)
            => _pieces.TryGetValue(position, out piece);

        public bool IsValidPosition(Position position)
            => (0 <= position.X && position.X < _columns) && (0 <= position.Y && position.Y < _rows);


        public bool Place(PieceView piece, Position toPosition)
        {
            if (!IsValidPosition(toPosition))
                return false;

            if (_pieces.ContainsKey(toPosition))
                return false;

            if (_pieces.ContainsValue(piece))
                return false;

            _pieces.Add(toPosition, piece);

            return true;
        }

        public bool Move(Position fromPosition, Position toPosition)
        {
            if (!IsValidPosition(toPosition))
                return false;

            if (!_pieces.TryGetValue(fromPosition, out var piece))
                return false;

            if (_pieces.ContainsKey(toPosition))
                return false;

            if (!_pieces.Remove(fromPosition))
                return false;

            _pieces.Add(toPosition, piece);

            return true;
        }

        public bool Take(Position fromPosition)
            => _pieces.Remove(fromPosition);
    }
}
