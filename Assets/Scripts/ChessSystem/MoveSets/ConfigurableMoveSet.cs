using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem.MoveSets
{
    internal class ConfigurableMoveSet<TPiece> : MoveSet<TPiece>
    {
        public delegate List<Position> PositionsCollector(Board<TPiece> board, Position fromPosition);

        private PositionsCollector _collectPositions;

        public ConfigurableMoveSet(Board<TPiece> board, PositionsCollector collectPositions) : base(board)
        {
            _collectPositions = collectPositions;
        }

        //constructor
        public override List<Position> Positions(Position fromPosition)
            => _collectPositions(Board, fromPosition);
    }
}
