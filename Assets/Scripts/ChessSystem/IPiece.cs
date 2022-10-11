using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    public enum PieceType
    {
        Pawn, King, Queen, Rook, Bishop, Knight
    }

    public enum Player
    {
        Player1, Player2
    }

    public interface IPiece
    {
        PieceType Type { get; }

        Player Player { get; }
    }
}
