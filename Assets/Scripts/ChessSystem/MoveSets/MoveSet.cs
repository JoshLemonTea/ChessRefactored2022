using BoardSystem;
using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem.MoveSets
{
    internal abstract class MoveSet<TPiece> : IMoveSet
    {
        protected Board<TPiece> Board { get; }

        protected MoveSet(Board<TPiece> board)
        {
            Board = board;
        }

        public abstract List<Position> Positions(Position fromPosition);

        internal virtual bool Execute(Position fromPosition, Position toPosition, CommandQueue commandQueue)
        {
            var pieceTaken = Board.TryGetPiece(toPosition, out var toPiece);
            Func<bool> commit = () =>
            {
                bool success = true;

                if (pieceTaken)
                    success &= Board.Take(toPosition);

                success &= Board.Move(fromPosition, toPosition);

                return success;
            };

            Func<bool> rollback = () => {
                var success = Board.Move(toPosition, fromPosition);

                if (pieceTaken)
                    success &= Board.Place(toPiece, fromPosition);

                return success;
            };

            return commandQueue.Execute(new DelegatingCommand(commit, rollback));
        }
    }
}