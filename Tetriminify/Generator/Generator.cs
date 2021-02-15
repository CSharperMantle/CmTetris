using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetriminify.Generator
{
    public static class Generator
    {
        /// <summary>
        /// Try to generate a <see cref="IReadOnlyList{T}"/> of <see cref="Tetrimino"/>s that fills the given template.
        /// </summary>
        /// <param name="template">
        /// <para>
        /// A two-dimentional <see cref="Array"/> of <see cref="Block"/>s.
        /// </para>
        /// <para>
        /// This array should only contain blocks with <see cref="TetriminoKind.Unsettled"/> or <see cref="TetriminoKind.Unavailable"/>.
        /// The former one indicates that this block is fillable with <see cref="Tetrimino"/> while the latter one has the opposite meaning.
        /// </para>
        /// </param>
        /// <param name="settledBlocks">
        /// When this method returns, contains a <see cref="IReadOnlyList{T}"/> of <see cref="Tetrimino"/>s of settled (placed) tetriminos, or
        /// <see cref="null"/> when fails to generate.
        /// </param>
        /// <returns>Whether the generation succeeds or not.</returns>
        public static bool TryGetPattern(Block[,] template, out IReadOnlyList<Tetrimino> settledBlocks)
        {
            throw new NotImplementedException();
        }

        private static bool CheckBlockCollision(Block block, IReadOnlyList<Block> placedBlocks, int playAreaWidth, int playAreaHeight)
        {
            // Left or right border collision
            if (block.Position.X < 0 || block.Position.X >= playAreaWidth)
            {
                return true;
            }
            // Bottom border collision
            if (block.Position.Y >= playAreaHeight)
            {
                return true;
            }
            // Block-block collision
            return placedBlocks.Any(
                (Block placedBlock) => (placedBlock.Position == block.Position) && (placedBlock.FilledBy != TetriminoKind.Unsettled)
            );
        }
    }
}
