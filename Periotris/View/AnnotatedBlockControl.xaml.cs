using System.Windows.Controls;
using System.Windows.Media;

namespace Periotris.View
{
    /// <summary>
    ///     Interaction logic for AnnotatedBlockControl.xaml
    /// </summary>
    public partial class AnnotatedBlockControl : UserControl
    {
        // TODO: 1 or 10?
        public static int OriginalHeight = 1;

        public static int OriginalWidth = 1;

        public AnnotatedBlockControl()
        {
            InitializeComponent();
        }

        public void SetFill(Brush brush)
        {
            Background = brush;
        }

        public void SetElementName(string str)
        {
            ElementNameTextBlock.Text = str;
        }
    }
}