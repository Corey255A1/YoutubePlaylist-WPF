//Corey Wunderlich WunderVision 2023
//https://www.wundervisionenvisionthefuture.com/
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace YoutubePlaylistWPF.Views
{
    public class SelectedItemConverter : IMultiValueConverter
    {
        public Brush DefaultBrush { get; set; } = new SolidColorBrush(Colors.AliceBlue);
        public Brush SelectedBrush { get; set; } = new SolidColorBrush(Colors.Green);
        

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == values[1])
            {
                return SelectedBrush;
            }
            return DefaultBrush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
