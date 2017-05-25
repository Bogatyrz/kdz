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
    [DataContract(Name = "Team",Namespace = "GamePrediction")]
    public class Team:INotifyPropertyChanged
    {
        private string _name;
        private int _score;        

        public Team()
        {
            
        }
        [DataMember()]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        [DataMember()]
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
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
