using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periotris.Model
{
    public interface IBlock
    {
        TetriminoKind FilledBy { get; }

        Position Position { get; }

        /// <summary>
        /// The atomic number. As for grouping headers the number is negative of the grouping id.
        /// i.e. group 1 header block has an AtomicNumber of -1.
        /// </summary>
        int AtomicNumber { get; }
    }
}
