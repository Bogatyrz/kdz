using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MainPage(this);
        }        

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {            
                switch (MessageBox.Show("\t\tСохранить изменения?", "\tСохранение", MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Cancel:
                    {
                        e.Cancel = true;
                        break;
                    }
                    case MessageBoxResult.Yes:
                    {
                        (this.DataContext as Statistic).Save_Data();
                        break;
                    }

                }

        }
    }
}
