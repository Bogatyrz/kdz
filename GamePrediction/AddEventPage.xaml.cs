using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static GamePrediction.Helper;
namespace GamePrediction
{
    /// <summary>
    /// Логика взаимодействия для AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Page
    {
        public AddEventPage(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
        }

        private MainWindow _mainWindow;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (check_user_chose())
            {
                SportEvent tempEvent=new SportEvent();
                tempEvent.Date=DatePicker.SelectedDate.Value;
                tempEvent.Team1Name = Team1Box.Text;
                tempEvent.Team2Name = Team2Box.Text;
                tempEvent.Title = ChooseBox.SelectedItem.ToString();
                bool cannAdd = true;
                foreach (var event1 in (_mainWindow.DataContext as Statistic).Events)
                {
                    if (event1.Date == tempEvent.Date && event1.Team1Name == tempEvent.Team1Name ||
                        event1.Team2Name == tempEvent.Team2Name)
                    cannAdd= check_string_fields(null, "Одна или обе команды уже зарегистрированы на данную дату");
                }
                if (cannAdd)
                    (_mainWindow.DataContext as Statistic).Events.Add(tempEvent);
                _mainWindow.MainFrame.Content=new ResultPage(_mainWindow);
            }                
        }

        private bool check_user_chose()
        {
            if (ChooseBox.SelectionBoxItem == null||ChooseBox.SelectedIndex<0)
            
                return    check_string_fields(null, "Выберите тип игры");
            
            
            if (!DatePicker.SelectedDate.HasValue)
            
                return check_string_fields(null, "Выберите дату соревнования");


            if (DatePicker.SelectedDate.Value < DateTime.Now)
                return check_string_fields(null, "Предстоящее событие не может иметь прошедшую дату");
                    
            if(!check_string_fields(Team1Box.Text, "Введите название 1ой команды"))
                return false;
            if (!check_string_fields(Team2Box.Text, "Введите название 2ой команды")) ;
            return true;
        }
    }
}
