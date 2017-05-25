using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GamePrediction
{
    public static class Helper
    {
        public static bool check_string_fields(string text, string message)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(message, "Неверный ввод", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        public static bool check_int_fields(string text, string message)
        {
            int res = 0;
            if (String.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(message, "Неверный ввод", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(text, out res))
            {
                MessageBox.Show(message, "Неверный ввод", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }
            return true;

        }
    }
}
