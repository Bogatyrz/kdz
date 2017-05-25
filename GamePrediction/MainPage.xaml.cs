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

namespace GamePrediction
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
            set_Label();
        }

        private MainWindow _mainWindow;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Content = new AddEventPage(_mainWindow);
        }

        private void set_Label()
        {
            //Wlabel.Content =
              //  "\tДанная программа предназначена для \nпрогноза результатов командных\nспортивных игр на основе некоторых\n статистических данных";
        }

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Content = new ResultPage(_mainWindow);
        }
    }
}
