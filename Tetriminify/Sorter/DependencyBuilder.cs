using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetriminify.Sorter
{
    /// <summary>
    /// Tetrimino-tetrimino dependency relationship builder.
    /// </summary>
    public static class DependencyBuilder
    {
        internal static IReadOnlyList<TetriminoNode> GetTetriminoDependencyGraph(IReadOnlyList<ITetrimino> tetriminos, int playAreaWidth, int playAreaHeight)
        {
            // Build block map
            List<TetriminoNode> tetriminoNodes = new List<TetriminoNode>(tetriminos.Count);
            MemoizedBlock[,] memoizedMap = new MemoizedBlock[playAreaHeight, playAreaWidth];
            foreach (ITetrimino tetrimino in tetriminos)
            {
                TetriminoNode tetriminoNode = new TetriminoNode
                {
                    Kind = tetrimino.Kind,
                    Position = tetrimino.Position,
                    FacingDirection = tetrimino.FacingDirection,
                };
                tetriminoNode.MemoizedBlocks = GetMemoizedBlocksForTetriminoNode(tetriminoNode, tetrimino);
                foreach (MemoizedBlock block in tetriminoNode.MemoizedBlocks)
                {
                    memoizedMap[block.Position.Y, block.Position.X] = block;
                }
                tetriminoNodes.Add(tetriminoNode);
            }
            // Get dependency relationship
            foreach (TetriminoNode tetriminoNode in tetriminoNodes)
            {
                foreach (MemoizedBlock block in tetriminoNode.MemoizedBlocks)
                {
                    // if a blocker under the current block is occupied then
                    // this tetrimino can not be placed until the underlying block's
                    // owner is placed, i.e., this tetrimino depends on the underlying
                    // block's owner.
                    int dependedBlockRow = block.Position.Y + 1;
                    int dependedBlockCol = block.Position.X;
                    if (TryGetOccupiedTetriminoNode(
                            memoizedMap,
                            dependedBlockRow, dependedBlockCol,
                            playAreaWidth, playAreaHeight,
                            out TetriminoNode dependOn
                       )
                        && (dependOn != tetriminoNode))
                    {
                        dependOn.DependedBy.Add(tetriminoNode);
                        tetriminoNode.Depending.Add(dependOn);
                    }
                }
            }
            return tetriminoNodes;
        }

        private static IReadOnlyList<MemoizedBlock> GetMemoizedBlocksForTetriminoNode(TetriminoNode node, ITetrimino tetrimino)
        {
            List<MemoizedBlock> memoizedBlocks = new List<MemoizedBlock>();
            foreach (IBlock block in tetrimino.Blocks)
            {
                MemoizedBlock newBlock = new MemoizedBlock
                {
                    Position = block.Position,
                    FilledBy = block.FilledBy,
                    Owner = node
                };
                memoizedBlocks.Add(newBlock);
            }
            return memoizedBlocks;
        }

        private static bool TryGetOccupiedTetriminoNode(MemoizedBlock[,] map,
            int row, int col,
            int playAreaWidth, int playAreaHeight,
            out TetriminoNode result)
        {
            if (row < 0 || row >= playAreaHeight || col < 0 || col >= playAreaWidth)
            {
                result = null;
                return false;
            }
            MemoizedBlock cell = map[row, col];
            if (cell == null
                || cell.FilledBy == TetriminoKind.AvailableToFill
                || cell.FilledBy == TetriminoKind.UnavailableToFill)
            {
                result = null;
                return false;
            }
            result = cell.Owner;
            return true;
        }
    }
}
