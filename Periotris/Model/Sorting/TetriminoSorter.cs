using System;
using System.Collections.Generic;
using System.Linq;
using Periotris.Model.Generation;

namespace Periotris.Model.Sorting
{
    /// <summary>
    ///     Topological sorting performer.
    /// </summary>
    internal static class TetriminoSorter
    {
        public static IReadOnlyList<Tetrimino> GetSortedTetriminos(IReadOnlyList<ITetrimino> tetriminos,
            int playAreaWidth, int playAreaHeight)
        {
            var graph = DependencyBuilder.GetTetriminoDependencyGraph(
                tetriminos, playAreaWidth, playAreaHeight);

            var startNodes = new List<TetriminoNode>(
                from node in graph
                where node.Depending.Count == 0
                select node
            );
            var result = new List<TetriminoNode>();

            while (startNodes.Count != 0)
            {
                var n = startNodes[0];
                startNodes.Remove(n);
                result.Add(n);
                var dependedBy = n.DependedBy.ToList();
                foreach (var m in dependedBy)
                {
                    n.DependedBy.Remove(m);
                    m.Depending.Remove(n);
                    if (m.Depending.Count == 0) startNodes.Add(m);
                }
            }

            if (graph.Any(node => node.DependedBy.Count != 0 || node.Depending.Count != 0))
                throw new ArgumentException(nameof(tetriminos));
            return result;
        }
    }
}