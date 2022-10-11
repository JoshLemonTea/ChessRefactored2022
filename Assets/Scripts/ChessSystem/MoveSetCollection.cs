using BoardSystem;
using ChessSystem.MoveSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    public class MoveSetCollection<TPiece>
        where TPiece : IPiece
    {
        private Dictionary<PieceType, MoveSet<TPiece>> _moveSets = new Dictionary<PieceType, MoveSet<TPiece>>(); 

        internal MoveSetCollection(Board<TPiece> board)
        {
            _moveSets.Add(PieceType.Pawn,
                    new ConfigurableMoveSet<TPiece>(board,
                        (Board<TPiece> b, Position p) => new MoveSetHelper<TPiece>(b, p)
                                     .North(1, MoveSetHelper<TPiece>.Empty)
                                     //.North(2, MoveSetHelper<TPiece>.Empty, MoveSetHelper<TPiece>.HasnotMove)
                                     .NorthEast(1, MoveSetHelper<TPiece>.ContainsEnemy)
                                     .NorthWest(1, MoveSetHelper<TPiece>.ContainsEnemy)
                                     .CollectValidPositions()));

            _moveSets.Add(PieceType.Rook,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                 .North()
                                 .East()
                                 .South()
                                 .West()
                                 .CollectValidPositions()));

            _moveSets.Add(ChessSystem.PieceType.Knight,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                 .Collect(2, 1, 1)
                                 .Collect(2, -1, 1)
                                 .Collect(-2, 1, 1)
                                 .Collect(-2, -1, 1)
                                 .Collect(1, 2, 1)
                                 .Collect(1, -2, 1)
                                 .Collect(-1, 2, 1)
                                 .Collect(-1, -2, 1)
                                 .CollectValidPositions()));

            _moveSets.Add(ChessSystem.PieceType.Bishop,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                 .NorthEast()
                                 .NorthWest()
                                 .SouthEast()
                                 .SouthWest()
                                 .CollectValidPositions()));

            _moveSets.Add(ChessSystem.PieceType.Queen,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                 .North()
                                 .East()
                                 .South()
                                 .West()
                                 .NorthEast()
                                 .NorthWest()
                                 .SouthEast()
                                 .SouthWest()
                                 .CollectValidPositions()));

            _moveSets.Add(ChessSystem.PieceType.King,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                .North(1)
                                .East(1)
                                .South(1)
                                .West(1)
                                .NorthEast(1)
                                .NorthWest(1)
                                .SouthEast(1)
                                .SouthWest(1)
                                .CollectValidPositions()));
        }

        public IMoveSet MoveSet(PieceType type)
        {
            return _moveSets[type];
        }
    }
}
