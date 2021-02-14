using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleTetris.View
{
    /// <summary>
    /// Interaction logic for BlockControl.xaml
    /// </summary>
    public partial class BlockControl : UserControl
    {
        public static int OriginalHeight = 5;

        public static int OriginalWidth = 5;

        public BlockControl()
        {
            InitializeComponent();
        }

        public void SetFill(SolidColorBrush solidColorBrush)
        {
            StarPolygon.Fill = solidColorBrush;
        }
    }
}
