using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessSystem
{
    public class Engine<TPiece>
        where TPiece : IPiece
    {
        private readonly Board<TPiece> _board;

        public MoveSetCollection<TPiece> MoveSet => null;

        public Engine(Board<TPiece> board)
        {
            _board = board;
        }

        

        public bool Move(Position fromPosition, Position toPosition)
        {    
            return false;
        }
    }

}
