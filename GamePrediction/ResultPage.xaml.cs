using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static GamePrediction.Helper;
namespace GamePrediction
{
    /// <summary>
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage(MainWindow window)
        {
            InitializeComponent();           
            _mainWindow = window;
            this.DataContext = _mainWindow.DataContext;
            
        }

        private MainWindow _mainWindow;
        public Statistic Statistic { get; set; }
        public SportEvent CurrEvent { get; set; }
        private void DelHistoryGame_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <(DataContext as Statistic).Events.Count; ++i)
            {
                if ((this.DataContext as Statistic).Events[i].GameHistory.Remove((sender as Button).DataContext as Game))
                {
                    (DataContext as Statistic).Events[i].calc_prediction();                    
                    break;
                }
            }
        }
        private void AddHistoryGame_OnClick(object sender, RoutedEventArgs e)
        {
            string team1Name = (((sender as Button).Parent as Grid).DataContext as SportEvent).Team1Name;
            string team2Name = (((sender as Button).Parent as Grid).DataContext as SportEvent).Team2Name;
            string team1Score =  (((sender as Button).Parent as Grid).Children[4] as TextBox).Text;
            string team2Score = (((sender as Button).Parent as Grid).Children[5] as TextBox).Text;                       
            if (!(((sender as Button).Parent as Grid).Children[3] as DatePicker).SelectedDate.HasValue)
            {
                check_string_fields(null, "Выберите дату!");
                return;
            }
            if ((((sender as Button).Parent as Grid).Children[3] as DatePicker).SelectedDate.Value.Ticks >= DateTime.Now.Ticks)
            {
                check_string_fields(null, "Статистика учитывает только реальные данные, а не предсказания!");
                return;
            }
            if(!check_int_fields(team1Score, "Счет команды  "+team1Score+"  должен быть целым числом!"))
                return;
            if (!check_int_fields(team2Score, "Счет команды  " + team2Score + "  должен быть целым числом!"))
                return;            
            foreach (SportEvent x in (DataContext as Statistic).Events)
            {
                if (x.Team1Name == (((sender as Button).Parent as Grid).DataContext as SportEvent).Team1Name
                    &&
                    x.Team2Name == (((sender as Button).Parent as Grid).DataContext as SportEvent).Team2Name)
                {
                    x.GameHistory.Add(
                        new Game
                        {
                            Date = (((sender as Button).Parent as Grid).Children[3] as DatePicker).SelectedDate.Value,
                            Team1 = new Team {Name = team1Name, Score = int.Parse(team1Score)},
                            Team2 = new Team {Name = team2Name, Score = int.Parse(team2Score)}
                        });
                    x.calc_prediction();
                    break;                    
                }                
            }
            foreach (var child in ((sender as Button).Parent as Grid).Children)
            {
                if (child is TextBox)
                    (child as TextBox).Clear();
                if (child is DatePicker)
                {
                    (child as DatePicker).SelectedDate = null;
                    (child as DatePicker).DisplayDate = DateTime.Today;
                }
            }

        }

        private void ListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem != null)
            {
                GamesGrid.DataContext = ((sender as ListView).SelectedItem as SportEvent);
                (GamesGrid.Children[2] as Grid).IsEnabled = true;
            }
            else
            {
                (GamesGrid.Children[2] as Grid).IsEnabled = false;
            }            
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DatePicker).SelectedDate.HasValue)
                if ((sender as DatePicker).SelectedDate.Value < DateTime.Now)
                {
                    foreach (SportEvent x in (DataContext as Statistic).Events)
                    {
                        if (x.Team1Name == (((sender as DatePicker).Parent as Grid).DataContext as Game).Team1.Name
                            &&
                            x.Team2Name == (((sender as DatePicker).Parent as Grid).DataContext as Game).Team2.Name)
                            x.calc_prediction();
                    }

                }
                else
                {
                    check_string_fields(null, "Статистика учитывает только фактические данные, а не предсказания!");
                }
        }


        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            
                if (check_int_fields((sender as TextBox).Text,"Счет должен быть представлен целлым числом"))
                {
                    foreach (SportEvent x in (DataContext as Statistic).Events)
                    {
                        if (x.Team1Name == (((sender as TextBox).Parent as Grid).DataContext as Game).Team1.Name
                            &&
                            x.Team2Name == (((sender as TextBox).Parent as Grid).DataContext as Game).Team2.Name)
                            x.calc_prediction();
                    }

                }                
        }
    }
}
