using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SimpleTetris.Model;

namespace SimpleTetris.View
{
    public static class TetrisControlHelper
    {
        public static FrameworkElement BlockControlFactory(Block block, double scale)
        {
            var newBlockControl = new BlockControl();
            newBlockControl.SetFill(GetBlockColorByTetriminoKind(block.FilledBy));
            newBlockControl.Height = BlockControl.OriginalHeight * scale;
            newBlockControl.Width = BlockControl.OriginalWidth * scale;
            SetCanvasLocation(newBlockControl,
                block.Position.X * BlockControl.OriginalHeight * scale,
                block.Position.Y * BlockControl.OriginalWidth * scale
            );
            Panel.SetZIndex(newBlockControl, 1);
            return newBlockControl;
        }

        public static void SetCanvasLocation(FrameworkElement element, double x, double y)
        {
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }

        public static SolidColorBrush GetBlockColorByTetriminoKind(TetriminoKind tetriminoKind)
        {
            switch (tetriminoKind)
            {
                case TetriminoKind.Linear:
                    return new SolidColorBrush(Colors.Blue);
                case TetriminoKind.Cubic:
                    return new SolidColorBrush(Colors.Lime);
                case TetriminoKind.LShapedCis:
                    return new SolidColorBrush(Colors.Green);
                case TetriminoKind.LShapedTrans:
                    return new SolidColorBrush(Colors.Red);
                case TetriminoKind.ZigZagCis:
                    return new SolidColorBrush(Colors.Yellow);
                case TetriminoKind.ZigZagTrans:
                    return new SolidColorBrush(Colors.Purple);
                case TetriminoKind.TShaped:
                    return new SolidColorBrush(Colors.Gray);
                default:
                    throw new ArgumentException(nameof(tetriminoKind));
            }
        }
    }
}