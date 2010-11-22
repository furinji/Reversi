using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ReversiWpf
{
    public class EnumRadioConverter : IValueConverter
    {
        // Enum → bool
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // バインドする列挙値の文字列表記がパラメータに渡されているか
            var paramString = parameter as string;
            if (paramString == null)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            // パラメータが Enum の値として正しいか
            if (!Enum.IsDefined(value.GetType(), paramString))
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            // パラメータを Enum に変換
            var paramParsed = Enum.Parse(value.GetType(), paramString);

            // 値が一致すれば true を返す
            return (value.Equals(paramParsed));
        }

        // bool → Enum
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // バインドする列挙値の文字列表記がパラメータに渡されているか
            var paramString = parameter as string;
            if (paramString == null)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            // チェックが入っているか (この評価は不要？)
            var isChecked = (bool)value;
            if (!isChecked)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            // 列挙型にパースして返す
            return Enum.Parse(targetType, paramString);
        }
    }
}