﻿using System.Collections.Generic;
using Periotris.Model.Generation;

namespace Periotris.Model.Sorting
{
    /// <summary>
    ///     Tetrimino-tetrimino dependency relationship builder.
    /// </summary>
    internal static class DependencyBuilder
    {
        public static IReadOnlyList<TetriminoNode> GetTetriminoDependencyGraph(IReadOnlyList<ITetrimino> tetriminos,
            int playAreaWidth, int playAreaHeight)
        {
            // Build block map
            var tetriminoNodes = new List<TetriminoNode>(tetriminos.Count);
            var memoizedMap = new MemoizedBlock[playAreaHeight, playAreaWidth];
            foreach (Tetrimino tetrimino in tetriminos)
            {
                var tetriminoNode = new TetriminoNode(tetrimino.Kind,
                    tetrimino.Position,
                    GeneratorHelper.GetFirstBlockPositionByPosition(tetrimino.Position, tetrimino.Kind,
                        tetrimino.FacingDirection),
                    tetrimino.FacingDirection
                );
                tetriminoNode.RealBlocks = tetriminoNode.MemoizedBlocks =
                    GetMemoizedBlocksForTetriminoNode(tetriminoNode, tetrimino);

                foreach (var block in tetriminoNode.MemoizedBlocks)
                    memoizedMap[block.Position.Y, block.Position.X] = block;
                tetriminoNodes.Add(tetriminoNode);
            }

            // Get dependency relationship
            foreach (var tetriminoNode in tetriminoNodes)
            foreach (var block in tetriminoNode.MemoizedBlocks)
            {
                // if a blocker under the current block is occupied then
                // this tetrimino can not be placed until the underlying block's
                // owner is placed, i.e., this tetrimino depends on the underlying
                // block's owner.
                var dependedBlockRow = block.Position.Y + 1;
                var dependedBlockCol = block.Position.X;
                if (TryGetOccupiedTetriminoNode(
                        memoizedMap,
                        dependedBlockRow, dependedBlockCol,
                        playAreaWidth, playAreaHeight,
                        out var dependOn
                    )
                    && dependOn != tetriminoNode)
                {
                    dependOn.DependedBy.Add(tetriminoNode);
                    tetriminoNode.Depending.Add(dependOn);
                }
            }

            return tetriminoNodes;
        }

        private static IReadOnlyList<MemoizedBlock> GetMemoizedBlocksForTetriminoNode(TetriminoNode node,
            ITetrimino tetrimino)
        {
            var memoizedBlocks = new List<MemoizedBlock>();
            foreach (Block block in tetrimino.Blocks)
            {
                var newBlock = new MemoizedBlock(block.FilledBy, block.Position, node, block.AtomicNumber,
                    block.Identifier);
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

            var cell = map[row, col];
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