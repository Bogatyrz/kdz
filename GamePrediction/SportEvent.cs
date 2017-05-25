using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;
using GamePrediction.Annotations;

namespace GamePrediction
{
    [DataContract(Name = "SportEvent", Namespace = "GamePrediction")]
    public class SportEvent:INotifyPropertyChanged
    {
        private ObservableCollection<Game> _gameHistory;
        private string _team1Name;
        private string _team2Name;
        private string _title;
        private DateTime _date;
        
        public SportEvent()
        {
            GameHistory = new ObservableCollection<Game>();
            Prediction=new Prediction();
        }

        public void calc_prediction()
        {
            long prescription;
            double scoreMargin;
            double drawCoeff=1;
            double matchTotal;
            double totalCoeffT1=1;
            double totalCoeffT2 =1;
            //Промежуточный коэффициент на победу 1 команды
            foreach (var game in GameHistory)
            {
                prescription = (_date.Ticks - game.Date.Ticks)/_date.Ticks;           //Чем старее игра, тем меньшую значимость имеет
                if (prescription <= 0)
                    prescription = 1;
                scoreMargin = game.Team1.Score - game.Team2.Score;                  //Чем больше разница в счете тем больше у команды шансов на победу в след матчах
                matchTotal = game.Team1.Score + game.Team2.Score;                   //Чем больше голов в итоге забито, тем меньше вероятность ничьи в дальнейшем
                if (scoreMargin == 0)
                    if (matchTotal == 0)
                    {
                        matchTotal = 1;
                        drawCoeff = drawCoeff + ((drawCoeff + scoreMargin) * prescription) / matchTotal;
                    }
                if (matchTotal != 0)
                {
                    totalCoeffT1 += scoreMargin * prescription / matchTotal;
                    totalCoeffT2 -= scoreMargin * prescription / matchTotal;
                }

            }            

            Prediction.Team1W = Math.Round(Math.Abs(totalCoeffT1) / (Math.Abs(totalCoeffT1) + Math.Abs(totalCoeffT2) + Math.Abs(drawCoeff)),3);
            Prediction.Draw = Math.Round(Math.Abs(drawCoeff) / (Math.Abs(totalCoeffT1) + Math.Abs(totalCoeffT2) + Math.Abs(drawCoeff)),3);
            Prediction.Team2W = Math.Round(Math.Abs(totalCoeffT2) / (Math.Abs(totalCoeffT1) + Math.Abs(totalCoeffT2) + Math.Abs(drawCoeff)),3);
        }
        [DataMember()]
        public string Team1Name
        {
            get { return _team1Name; }
            set
            {
                _team1Name = value;
                OnPropertyChanged(nameof(Team1Name));
            }
        }
        [DataMember()]
        public string Team2Name
        {
            get { return _team2Name; }
            set
            {
                _team2Name = value;
                OnPropertyChanged(nameof(Team2Name));
            }
        }
        [DataMember()]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        [DataMember()]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value; 
                OnPropertyChanged(nameof(Date));
            }
        }
        [DataMember()]
        public ObservableCollection<Game> GameHistory
        {
            get { return _gameHistory; }
            set
            {
                _gameHistory = value; 
                OnPropertyChanged(nameof(GameHistory));
            }
        }
        [DataMember()]
        public Prediction Prediction { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));            
        }
    }
}
