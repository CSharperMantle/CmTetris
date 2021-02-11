using SimpleTetris.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleTetris.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TetrisModel _tetrisModel = new TetrisModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonTemp_Click(object sender, RoutedEventArgs e)
        {
            var _frozenBlocks = new List<Model.Block>() {
                new Model.Block(TetriminoKind.Cubic, new Position(0, 5)),
                new Model.Block(TetriminoKind.Cubic, new Position(1, 5)),
                new Model.Block(TetriminoKind.Cubic, new Position(0, 4)),
                new Model.Block(TetriminoKind.Cubic, new Position(0, 3)),
                new Model.Block(TetriminoKind.Cubic, new Position(1, 3)),
                new Model.Block(TetriminoKind.Cubic, new Position(0, 2)),
                new Model.Block(TetriminoKind.Cubic, new Position(1, 2)),
                new Model.Block(TetriminoKind.Cubic, new Position(0, 1)),
                new Model.Block(TetriminoKind.Cubic, new Position(1, 1)),
                new Model.Block(TetriminoKind.Cubic, new Position(1, 0))
            };
            var frozenBlocksCopy = _frozenBlocks.ToArray();
            var rowsCleared = 0;
            var rowsClearedYVal = new List<int>();
            // Group blocks by row number (aka. Block.Position.Y), bottom-up
            var rows = from block in frozenBlocksCopy
                       group block by block.Position.Y into row
                       orderby row.Key descending
                       select row;
            foreach (var row in rows)
            {
                // If elements in one row is more than Width, then it is well filled.
                if (row.Count() >= 2)
                {
                    // Remove the entire line.
                    rowsCleared++;
                    rowsClearedYVal.Add(row.Key);
                    foreach (var block in row)
                    {
                        _frozenBlocks.Remove(block);
                    }
                }
            }
            /* As we are using bottom-up method, we can iterate over the list of Y values, moving down blocks.
             * - - - -
             * + + + +
             * + + + +
             * - - - -   <-- Line deleted Y=3
             * + + + +
             * - - - -   <-- Line deleted Y=5
             * ITERATION 1:
             * - - - -
             * - - - -
             * + + + +
             * + + + +
             * - - - -
             * + + + +   <-- Empty space zapped and moved down
             * ITERATION 2:
             * - - - -
             * - - - -
             * - - - -
             * + + + +
             * + + + +   <-- Empty space zapped and moved down
             * + + + +
             */
            while (rowsClearedYVal.Count > 0)
            {
                frozenBlocksCopy = _frozenBlocks.ToArray();
                // Move out first Y value
                var currentClearedRowYVal = rowsClearedYVal[0];
                rowsClearedYVal.RemoveAt(0);

                // Increase all remaining Y values by 1 in order to match the Y values after moving down
                for (var idx = 0; idx < rowsClearedYVal.Count; idx++)
                {
                    rowsClearedYVal[idx]++;
                }

                var blocksFalling = from block in frozenBlocksCopy
                                    where block.Position.Y < currentClearedRowYVal
                                    select block;
                foreach (var block in blocksFalling)
                {
                    _frozenBlocks.Remove(block);
                    var newBlock = new Model.Block(block.FilledBy, new Position(block.Position.X, block.Position.Y + 1));
                    _frozenBlocks.Add(newBlock);
                }
            }

            Debug.WriteLine(rowsCleared);
        }

        private void Handler(object sender, BlockChangedEventArgs eventArgs)
        {
            Debug.WriteLine(string.Format("UPDATED:{0} DIS:{1}", eventArgs.BlockUpdated, eventArgs.Disappeared));
        }
    }
}
