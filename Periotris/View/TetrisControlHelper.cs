using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Periotris.Model;

namespace Periotris.View
{
    public static class TetrisControlHelper
    {
        public static FrameworkElement BlockControlFactory(IBlock block, double scale, int atomicNumber)
        {
            AnnotatedBlockControl newBlockControl = new AnnotatedBlockControl();
            newBlockControl.SetFill(GetBlockColorByTetriminoKind(block.FilledBy));
            newBlockControl.SetElementName(GetElementNameByAtomicNumber(atomicNumber));
            newBlockControl.Height = AnnotatedBlockControl.OriginalHeight * scale;
            newBlockControl.Width = AnnotatedBlockControl.OriginalWidth * scale;
            SetCanvasLocation(newBlockControl,
                block.Position.X * AnnotatedBlockControl.OriginalHeight * scale,
                block.Position.Y * AnnotatedBlockControl.OriginalWidth * scale
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

        public static string GetElementNameByAtomicNumber(int atomicNumber)
        {
            throw new NotImplementedException();
        }
    }
}
