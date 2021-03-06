using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Tetriminify.Generator;
using Tetriminify.Sorter;

namespace Tetriminify
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentColId;
        private readonly List<Block> currentRow = new List<Block>();

        private int currentRowId;

        private readonly List<Block[]> currentTemplate = new List<Block[]>();

        private List<Block[]> orderedResult = new List<Block[]>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddBlockButton_Click(object sender, RoutedEventArgs e)
        {
            TetriminoKind kind;
            switch (TypeComboBox.SelectedIndex)
            {
                case 0:
                    kind = TetriminoKind.AvailableToFill;
                    break;
                case 1:
                    kind = TetriminoKind.UnavailableToFill;
                    break;
                default:
                    return;
            }

            var block = new Block(kind, new Position(currentColId, currentRowId));
            currentRow.Add(block);
            currentColId++;
            RefreshCurrentRowDisplay();
        }

        private void RefreshCurrentRowDisplay()
        {
            var sb = new StringBuilder();
            foreach (var block in currentRow)
                switch (block.FilledBy)
                {
                    case TetriminoKind.AvailableToFill:
                        sb.Append("A ");
                        break;
                    case TetriminoKind.UnavailableToFill:
                        sb.Append("U ");
                        break;
                    default:
                        throw new ArgumentException(nameof(block.FilledBy));
                }

            RowTextBlock.Text = sb.ToString();
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRow.Count == 0)
            {
                MessageBox.Show("Current row is empty.",
                    "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error
                );
                return;
            }

            currentTemplate.Add(currentRow.ToArray());
            currentRow.Clear();
            currentColId = 0;
            currentRowId++;
            RefreshCurrentTemplateDisplay();
            RefreshCurrentRowDisplay();
        }

        private void RefreshCurrentTemplateDisplay()
        {
            var sb = new StringBuilder();
            foreach (var blocks in currentTemplate)
            {
                foreach (var block in blocks)
                    switch (block.FilledBy)
                    {
                        case TetriminoKind.AvailableToFill:
                            sb.Append("A ");
                            break;
                        case TetriminoKind.UnavailableToFill:
                            sb.Append("U ");
                            break;
                        default:
                            throw new ArgumentException(nameof(block.FilledBy));
                    }

                sb.AppendLine();
            }

            TemplateTextBlock.Text = sb.ToString();
        }

        private void GeneratePatternButton_Click(object sender, RoutedEventArgs e)
        {
            Block[,] template;

            try
            {
                template = To2D(currentTemplate.ToArray());
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Unable to generate a square array. Is the template empty?",
                    "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error
                );
                return;
            }

            var tetriminos = PatternGenerator.GetPattern(template);

            if (tetriminos.Count == 0)
                MessageBox.Show("Unable to find a solution.",
                    "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK
                );
            foreach (Tetrimino tetrimino in tetriminos)
            foreach (Block block in tetrimino.Blocks)
                template[block.Position.Y, block.Position.X].FilledBy = block.FilledBy;
            RefreshCurrentResultDisplay(template);

            var orderedTetriminos =
                PatternSorter.GetSortedTetriminos(tetriminos, template.GetLength(1), template.GetLength(0));
            var order = new int?[template.GetLength(0), template.GetLength(1)];
            RefreshCurrentOrderedDisplay(orderedTetriminos, order);

            currentTemplate.Clear();
            currentRow.Clear();
            currentRowId = 0;
            currentColId = 0;
            RefreshCurrentRowDisplay();
            RefreshCurrentTemplateDisplay();
        }

        private void RefreshCurrentResultDisplay(Block[,] result)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    var block = result[i, j];
                    switch (block.FilledBy)
                    {
                        case TetriminoKind.Linear:
                            sb.Append("I ");
                            break;
                        case TetriminoKind.Cubic:
                            sb.Append("C ");
                            break;
                        case TetriminoKind.LShapedCis:
                            sb.Append("L ");
                            break;
                        case TetriminoKind.LShapedTrans:
                            sb.Append("R ");
                            break;
                        case TetriminoKind.ZigZagCis:
                            sb.Append("Z ");
                            break;
                        case TetriminoKind.ZigZagTrans:
                            sb.Append("S ");
                            break;
                        case TetriminoKind.TShaped:
                            sb.Append("T ");
                            break;
                        case TetriminoKind.AvailableToFill:
                            sb.Append("A ");
                            break;
                        case TetriminoKind.UnavailableToFill:
                            sb.Append("U ");
                            break;
                        default:
                            throw new ArgumentException(nameof(block.FilledBy));
                    }
                }

                sb.AppendLine();
            }

            ResultTextBlock.Text = sb.ToString();
        }

        private void RefreshCurrentOrderedDisplay(IReadOnlyList<ITetrimino> tetriminos, int?[,] order)
        {
            var n = 0;

            foreach (var tetrimino in tetriminos)
            {
                foreach (var block in tetrimino.Blocks) order[block.Position.Y, block.Position.X] = n;
                n++;
            }

            var sb = new StringBuilder();
            for (var i = 0; i < order.GetLength(0); i++)
            {
                for (var j = 0; j < order.GetLength(1); j++)
                {
                    var nth = order[i, j];
                    if (!nth.HasValue)
                        sb.Append("  ");
                    else
                        sb.AppendFormat("{0} ", nth.Value);
                }

                sb.AppendLine();
            }

            OrderedTextBlock.Text = sb.ToString();
        }

        private static T[,] To2D<T>(T[][] source)
        {
            try
            {
                var firstDim = source.Length;
                var secondDim = source.GroupBy(row => row.Length).Single().Key;

                var result = new T[firstDim, secondDim];
                for (var i = 0; i < firstDim; i++)
                for (var j = 0; j < secondDim; j++)
                    result[i, j] = source[i][j];
                return result;
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException(nameof(source), ex);
            }
        }
    }
}