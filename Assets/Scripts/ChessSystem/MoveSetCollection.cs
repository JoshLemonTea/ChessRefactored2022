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
        public IMoveSet MoveSet(PieceType type)
        {
            return null;
        }
    }
}
