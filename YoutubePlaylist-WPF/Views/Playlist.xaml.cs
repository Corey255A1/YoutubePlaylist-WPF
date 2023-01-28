using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace YoutubePlaylistWPF.Views
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class Playlist : UserControl
    {
        public Playlist()
        {
            InitializeComponent();
        }
    }

    public class SelectedItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == values[1])
            {
                return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.AliceBlue);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
