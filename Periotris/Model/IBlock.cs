namespace Periotris.Model
{
    /// <summary>
    ///     Represent a single block in an <see cref="ITetrimino" />.
    /// </summary>
    public interface IBlock
    {
        TetriminoKind FilledBy { get; }

        Position Position { get; }

        /// <summary>
        ///     The atomic number of the element this block representing.
        /// </summary>
        /// <remarks>
        ///     As for grouping headers the number is negative of the grouping id.
        ///     i.e. group 1 header block has an AtomicNumber of -1.
        /// </remarks>
        int AtomicNumber { get; }
    }
}