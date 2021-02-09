using SimpleTetris.Model;
using System;
using System.Collections.Generic;
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
        private readonly TetrisModel tetrisModel = new TetrisModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonTemp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ButtonTemp_Click");

            var tetrimino = new Tetrimino(TetriminoKind.ZigZagTrans);
            System.Diagnostics.Debug.WriteLine(string.Format("X:{0} Y:{1}", tetrimino.Position.X, tetrimino.Position.Y));
            var blocks = tetrimino.Blocks.ToArray();
            foreach (var block in blocks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Block X:{0} Y:{1}", block.Position.X, block.Position.Y));
            }

            tetrimino.Move(MoveDirection.Left, (Model.Block _) => { return false; });
            System.Diagnostics.Debug.WriteLine(string.Format("X:{0} Y:{1}", tetrimino.Position.X, tetrimino.Position.Y));
            blocks = tetrimino.Blocks.ToArray();
            foreach (var block in blocks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Block X:{0} Y:{1}", block.Position.X, block.Position.Y));
            }

            tetrimino.Rotate(RotationDirection.Left, (Model.Block _) => { return false; });
            System.Diagnostics.Debug.WriteLine(string.Format("X:{0} Y:{1}", tetrimino.Position.X, tetrimino.Position.Y));
            blocks = tetrimino.Blocks.ToArray();
            foreach (var block in blocks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Block X:{0} Y:{1}", block.Position.X, block.Position.Y));
            }

            tetrimino.Rotate(RotationDirection.Right, (Model.Block _) => { return false; });
            System.Diagnostics.Debug.WriteLine(string.Format("X:{0} Y:{1}", tetrimino.Position.X, tetrimino.Position.Y));
            blocks = tetrimino.Blocks.ToArray();
            foreach (var block in blocks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Block X:{0} Y:{1}", block.Position.X, block.Position.Y));
            }
        }
    }
}
