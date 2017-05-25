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
    [DataContract(Name = "Prediction", Namespace = "GamePrediction")]
    public class Prediction:INotifyPropertyChanged
    {
        private double _team1W;
        private double _draw;
        private double _team2W;

        public Prediction()
        {
            
        }
        [DataMember()]
        public double Team1W
        {
            get { return _team1W; }
            set
            {
                _team1W = value;
                OnPropertyChanged(nameof(Team1W));
            }
        }
        [DataMember()]
        public double Draw
        {
            get { return _draw; }
            set
            {
                _draw = value; 
                OnPropertyChanged(nameof(Draw));
            }
        }
        [DataMember()]
        public double Team2W
        {
            get { return _team2W; }
            set
            {
                _team2W = value; 
                OnPropertyChanged(nameof(Team2W));
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
