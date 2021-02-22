using System;
using System.Collections.Generic;

namespace Periotris.Model
{
    /// <summary>
    /// Represent a basic tetrimino with basic features.
    /// </summary>
    public interface ITetrimino
    {
        TetriminoKind Kind { get; }

        Position Position { get; }

        IReadOnlyList<IBlock> Blocks { get; }

        /// <summary>
        /// Move a <see cref="ITetrimino"/> towards a <see cref="MoveDirection"/> if permits.
        /// The <see cref="ITetrimino"/> will not be changed if the operation fails.
        /// </summary>
        /// <param name="collisionChecker">A <see cref="Func{Block, bool}"/> which returns <see cref="true"/>
        /// when the block will collide</param>
        /// <returns>Whether the <see cref="TryMove(MoveDirection, Func{IBlock, bool})"/> step succeeds</returns>
        bool TryMove(MoveDirection direction, Func<IBlock, bool> collisionChecker);

        /// <summary>
        /// Rotate a <see cref="ITetrimino"/> towards a <see cref="RotationDirection"/> if permits.
        /// The <see cref="ITetrimino"/> will not be changed if the operation fails.
        /// </summary>
        /// <param name="collisionChecker">A <see cref="Func{Block, bool}"/> which returns <see cref="true"/> when the block will collide</param>
        /// <returns>Whether the <see cref="TryRotate"/> step succeeds</returns>
        bool TryRotate(RotationDirection direction, Func<IBlock, bool> collisionChecker);
    }
}
