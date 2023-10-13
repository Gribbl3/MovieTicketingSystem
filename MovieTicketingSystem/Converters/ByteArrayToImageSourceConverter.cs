using System.Globalization;

namespace MovieTicketingSystem.Converters;

public class ByteArrayToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is byte[] byteArray)
        {
            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
