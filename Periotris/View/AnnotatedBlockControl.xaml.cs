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

namespace Periotris.View
{
    /// <summary>
    /// Interaction logic for AnnotatedBlockControl.xaml
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
