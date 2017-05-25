using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GamePrediction.Annotations;

namespace GamePrediction
{
    [DataContract(Name = "Game",Namespace = "GamePrediction")]
    public class Game:INotifyPropertyChanged
    {
        private Team _team1;
        private Team _team2;
        private DateTime _date;
        public Game() { }
        [DataMember()]
        public Team Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
                OnPropertyChanged(nameof(Team1));
            }
        }
        [DataMember()]
        public Team Team2
        {
            get { return _team2; }
            set
            {
                _team2 = value;
                OnPropertyChanged(nameof(Team2));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
