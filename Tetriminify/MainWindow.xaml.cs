using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Tetriminify.Generator;

namespace Tetriminify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Block> currentRow = new List<Block>();

        private List<Block[]> currentTemplate = new List<Block[]>();

        private int currentRowId = 0;

        private int currentColId = 0;

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
            Block block = new Block(kind, new Position(currentColId, currentRowId));
            currentRow.Add(block);
            currentColId++;
            RefreshCurrentRowDisplay();
        }

        private void RefreshCurrentRowDisplay()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Block block in currentRow)
            {
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
            }
            RowTextBlock.Text = sb.ToString();
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            currentTemplate.Add(currentRow.ToArray());
            currentRow.Clear();
            currentRowId = 0;
            currentColId++;
            RefreshCurrentTemplateDisplay();
            RefreshCurrentRowDisplay();
        }

        private void RefreshCurrentTemplateDisplay()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Block[] blocks in currentTemplate)
            {
                foreach (Block block in blocks)
                {
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
                }
                sb.AppendLine();
                
            }
            TemplateTextBlock.Text = sb.ToString();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            Block[,] template = To2D(currentTemplate.ToArray());

            var tetriminos = PatternGenerator.GetPattern(template);

            foreach (Tetrimino tetrimino in tetriminos)
            {
                foreach (Block block in tetrimino.Blocks)
                {
                    template[block.Position.Y, block.Position.X].FilledBy = block.FilledBy;
                }
            }
            RefreshCurrentResultDisplay(template);
            currentTemplate.Clear();
            currentRow.Clear();
            RefreshCurrentRowDisplay();
            RefreshCurrentTemplateDisplay();
        }

        private void RefreshCurrentResultDisplay(Block[,] result)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.GetLength(0); i++)
		    {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Block block = result[i, j];
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

        private static T[,] To2D<T>(T[][] source)
        {
            try
            {
                int firstDim = source.Length;
                int secondDim = source.GroupBy(row => row.Length).Single().Key;

                var result = new T[firstDim, secondDim];
                for (int i = 0; i < firstDim; i++)
                {
                    for (int j = 0; j < secondDim; j++)
                    {
                        result[i, j] = source[i][j];
                    }
                }
                return result;
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException(nameof(source), ex);
            }
        }
    }
}
