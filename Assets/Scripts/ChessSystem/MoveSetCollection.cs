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
                                     .Forward(1, MoveSetHelper<TPiece>.Empty)
                                     //.North(2, MoveSetHelper<TPiece>.Empty, MoveSetHelper<TPiece>.HasnotMove)
                                     .ForwardRight(1, MoveSetHelper<TPiece>.ContainsEnemy)
                                     .ForwardLeft(1, MoveSetHelper<TPiece>.ContainsEnemy)
                                     .CollectValidPositions()));

            _moveSets.Add(PieceType.Rook,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                 .Forward()
                                 .Right()
                                 .Backward()
                                 .Left()
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
                                 .ForwardRight()
                                 .ForwardLeft()
                                 .BackwardRight()
                                 .BackwardLeft()
                                 .CollectValidPositions()));

            _moveSets.Add(ChessSystem.PieceType.Queen,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                 .Forward()
                                 .Right()
                                 .Backward()
                                 .Left()
                                 .ForwardRight()
                                 .ForwardLeft()
                                 .BackwardRight()
                                 .BackwardLeft()
                                 .CollectValidPositions()));

            _moveSets.Add(ChessSystem.PieceType.King,
                new ConfigurableMoveSet<TPiece>(board,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                .Forward(1)
                                .Right(1)
                                .Backward(1)
                                .Left(1)
                                .ForwardRight(1)
                                .ForwardLeft(1)
                                .BackwardRight(1)
                                .BackwardLeft(1)
                                .CollectValidPositions()));
        }

        public IMoveSet this[PieceType type]
            => _moveSets[type];
        //internal means only accessible in this class
        internal bool TryGetMoveSet(PieceType type, out MoveSet<TPiece> moveSet)
            => _moveSets.TryGetValue(type, out moveSet);
    }
}
