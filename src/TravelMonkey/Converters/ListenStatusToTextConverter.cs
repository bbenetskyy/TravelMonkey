using System;
using System.Globalization;
using TravelMonkey.Models;
using Xamarin.Forms;

namespace TravelMonkey.Converters
{
    public class ListenStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Enum.TryParse<ListenStatus>(value.ToString(), true, out var status))
                return "Tap and Speak";

            switch (status)
            {
                case ListenStatus.Default:
                    return "Tap and Speak";
                case ListenStatus.Listening:
                    return "Listening";
                case ListenStatus.Processing:
                    return "Processing";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
