using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models
{
    public class PiecePlacedEventArgs : EventArgs
    {
        public Position ToPosition { get; }

        public PieceView Piece { get; }

        public PiecePlacedEventArgs(Position toPosition, PieceView piece)
        {
            ToPosition = toPosition;
            Piece = piece;
        }
    }

    public class PieceMovedEventArgs : EventArgs
    {
        public Position ToPosition { get; }

        public Position FromPosition { get; }

        public PieceView Piece { get; }

        public PieceMovedEventArgs(Position toPosition, Position fromPosition, PieceView piece)
        {
            ToPosition = toPosition;
            FromPosition = fromPosition;
            Piece = piece;
        }
    }

    public class PieceTakenEventArgs : EventArgs
    {
        public Position FromPosition { get; }

        public PieceView Piece { get; }

        public PieceTakenEventArgs(Position fromPosition, PieceView piece)
        {
            FromPosition = fromPosition;
            Piece = piece;
        }
    }

    public class Board
    {
        private int _rows;
        private int _columns;
        
        private readonly Dictionary<Position, PieceView> _pieces = new Dictionary<Position, PieceView>();

        public event EventHandler<PiecePlacedEventArgs> PiecePlaced;
        public event EventHandler<PieceMovedEventArgs> PieceMoved;
        public event EventHandler<PieceTakenEventArgs> PieceTaken;

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

            OnPiecePlaced(new PiecePlacedEventArgs(toPosition, piece));

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

            OnPieceMoved(new PieceMovedEventArgs(toPosition, fromPosition, piece));

            return true;
        }

        public bool Take(Position fromPosition)
        {
            if (!_pieces.TryGetValue(fromPosition, out var piece))
                return false;

            if (!_pieces.Remove(fromPosition))
                return false;

            OnPieceTaken(new PieceTakenEventArgs(fromPosition, piece));

            return true;
        }                                                     


        protected virtual void OnPiecePlaced(PiecePlacedEventArgs eventArgs)
        {
            var handler = PiecePlaced;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPieceMoved(PieceMovedEventArgs eventArgs)
        {
            var handler = PieceMoved;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPieceTaken(PieceTakenEventArgs eventArgs)
        {
            var handler = PieceTaken;
            handler?.Invoke(this, eventArgs);
        }
    }
}
