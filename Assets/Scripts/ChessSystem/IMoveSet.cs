using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    public interface IMoveSet
    {
        //to get back a list of valid positions
        //just a description of what we wanna do
        List<Position> Positions(Position fromPosition);
    }
}
