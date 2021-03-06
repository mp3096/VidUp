﻿#region

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

#endregion

namespace Drexel.VidUp.UI.Converters
{
    public class StringEmptyCollapsedConverter : MarkupExtension, IValueConverter
    {
        private static StringEmptyCollapsedConverter converter;

        public StringEmptyCollapsedConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return "Collapsed";
            }

            if (value is string)
            {
                string input = (string)value;
                if (string.IsNullOrWhiteSpace(input))
                {
                    return "Collapsed";
                }

                return "Visible";
            }

            throw new ArgumentException("Value to convert is no string.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ?? (converter = new StringEmptyCollapsedConverter());
        }
    }
}
