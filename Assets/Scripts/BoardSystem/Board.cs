using System;
using System.Collections.Generic;

namespace BoardSystem
{
    public class PiecePlacedEventArgs<TPiece> : EventArgs
    {
        public Position ToPosition { get; }
        //generic parameter
        public TPiece Piece { get; }

        public PiecePlacedEventArgs(Position toPosition, TPiece piece)
        {
            ToPosition = toPosition;
            Piece = piece;
        }
    }

    public class PieceMovedEventArgs<TPiece> : EventArgs
    {
        public Position ToPosition { get; }

        public Position FromPosition { get; }

        public TPiece Piece { get; }

        public PieceMovedEventArgs(Position toPosition, Position fromPosition, TPiece piece)
        {
            ToPosition = toPosition;
            FromPosition = fromPosition;
            Piece = piece;
        }
    }
    //generic class because it contains generic parameter
    public class PieceTakenEventArgs<TPiece> : EventArgs
    {
        public Position FromPosition { get; }

        public TPiece Piece { get; }

        public PieceTakenEventArgs(Position fromPosition, TPiece piece)
        {
            FromPosition = fromPosition;
            Piece = piece;
        }
    }

    public class Board<TPiece>
    {
        private int _rows;
        private int _columns;

        //linking position with board
        //generic parameter 
        //define generic parameters for class instead of using a concrete class
        private readonly Dictionary<Position, TPiece> _pieces = new Dictionary<Position, TPiece>();


        public event EventHandler<PiecePlacedEventArgs<TPiece>> PiecePlaced;
        public event EventHandler<PieceMovedEventArgs<TPiece>> PieceMoved;
        public event EventHandler<PieceTakenEventArgs<TPiece>> PieceTaken;


        public Board(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public bool TryGetPiece(Position position, out TPiece piece)
            => _pieces.TryGetValue(position, out piece);

        public bool IsValidPosition(Position position)
            => (0 <= position.X && position.X < _columns) && (0 <= position.Y && position.Y < _rows);

        public bool Place(TPiece piece, Position toPosition)
        {
            if (!IsValidPosition(toPosition))
                return false;

            if (_pieces.ContainsKey(toPosition))
                return false;

            if (_pieces.ContainsValue(piece))
                return false;

            _pieces.Add(toPosition, piece);

            OnPiecePlaced(new PiecePlacedEventArgs<TPiece>(toPosition, piece));

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

            OnPieceMoved(new PieceMovedEventArgs<TPiece>(toPosition, fromPosition, piece));

            return true;
        }

        public bool Take(Position fromPosition)
        {
            if (!_pieces.TryGetValue(fromPosition, out var piece))
                return false;

            if (!_pieces.Remove(fromPosition))
                return false;

            OnPieceTaken(new PieceTakenEventArgs<TPiece>(fromPosition, piece));

            return true;
        }                                                     


        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece> eventArgs)
        {
            var handler = PiecePlaced;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPieceMoved(PieceMovedEventArgs<TPiece> eventArgs)
        {
            var handler = PieceMoved;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPieceTaken(PieceTakenEventArgs<TPiece> eventArgs)
        {
            var handler = PieceTaken;
            handler?.Invoke(this, eventArgs);
        }
    }
}
