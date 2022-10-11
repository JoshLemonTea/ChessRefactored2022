using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem.MoveSets
{
    internal class MoveSetHelper<TPiece>
        where TPiece : IPiece
    {
        public delegate bool Validator(Board<TPiece> board, Position fromPosition, Position toPosition);

        private readonly Board<TPiece> _board;
        private readonly Position _fromPosition;
        private readonly Player _player;

        private List<Position> _validPositions = new List<Position>();

        public MoveSetHelper(Board<TPiece> board, Position fromPosition)
        {
            if (!board.TryGetPiece(fromPosition, out var piece))
                throw new ArgumentException($"{nameof(fromPosition)} {fromPosition} does not contain a {typeof(TPiece)}");

            _board = board;
            _fromPosition = fromPosition;
            _player = piece.Player;
        }

        public MoveSetHelper<TPiece> North(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(0, 1, maxSteps, validators);

        public MoveSetHelper<TPiece> NorthEast(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(1, 1, maxSteps, validators);

        public MoveSetHelper<TPiece> East(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(1, 0, maxSteps, validators);

        public MoveSetHelper<TPiece> SouthEast(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(1, -1, maxSteps, validators);

        public MoveSetHelper<TPiece> South(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(0, -1, maxSteps, validators);

        public MoveSetHelper<TPiece> SouthWest(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(-1, -1, maxSteps, validators);

        public MoveSetHelper<TPiece> West(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(-1, 0, maxSteps, validators);

        public MoveSetHelper<TPiece> NorthWest(int maxSteps = int.MaxValue, params Validator[] validators)
            => Collect(-1, 1, maxSteps, validators);

        public MoveSetHelper<TPiece> Collect(int xOffset, int yOffset, int maxSteps = int.MaxValue, params Validator[] validators)
        {
            xOffset *= (_player == Player.Player1) ? 1 : -1;
            yOffset *= (_player == Player.Player1) ? 1 : -1;

            var nextPosition = new Position(_fromPosition.X + xOffset, _fromPosition.Y + yOffset);

            var steps = 0;
            while (steps < maxSteps && _board.IsValidPosition(nextPosition) && validators.All((v) => v(_board, _fromPosition, nextPosition)))
            {
                if (_board.TryGetPiece(nextPosition, out var nextPiece))
                {
                    if (nextPiece.Player != _player)
                        _validPositions.Add(nextPosition);

                    break;
                }
                else
                {
                    _validPositions.Add(nextPosition);

                    nextPosition = new Position(nextPosition.X + xOffset, nextPosition.Y + yOffset);
                    steps++;
                }
            }

            return this;
        }

        public List<Position> CollectValidPositions()
        {
            return _validPositions;
        }


        public static bool Empty(Board<TPiece> board, Position fromPosition, Position toPosition)
            => !board.TryGetPiece(toPosition, out var _);

        public static bool ContainsEnemy(Board<TPiece> board, Position fromPosition, Position toPosition)
           => board.TryGetPiece(fromPosition, out var fromPiece)
                    && board.TryGetPiece(toPosition, out var toPiece)
                    && fromPiece.Player != toPiece.Player;
    }
}
