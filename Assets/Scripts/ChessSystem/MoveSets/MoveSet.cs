using BoardSystem;
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

        internal virtual bool Execute(Position fromPosition, Position toPosition)
        {
            Board.Take(toPosition);

            return Board.Move(fromPosition, toPosition);
        }
    }
}