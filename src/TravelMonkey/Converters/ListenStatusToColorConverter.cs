using System;
using System.Globalization;
using TravelMonkey.Models;
using Xamarin.Forms;

namespace TravelMonkey.Converters
{
    public class ListenStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Enum.TryParse<ListenStatus>(value.ToString(), true, out var status))
                return Color.Black;

            switch (status)
            {
                case ListenStatus.Default:
                    return Color.Black;
                case ListenStatus.Listening:
                    return Color.MediumSeaGreen;
                case ListenStatus.Processing:
                    return Color.DodgerBlue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
