using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GamePrediction.Annotations;
using Newtonsoft.Json;

namespace GamePrediction
{
    [DataContract(Name = "Statistic", Namespace = "GamePrediction")]
    public class Statistic:INotifyPropertyChanged
    {
        private ObservableCollection<SportEvent> _events;

        public void LoadData()
        {

            Events = new ObservableCollection<SportEvent>();
            try
            {
                string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                using (StreamReader inputFile = new StreamReader(myDocPath + @"\Events.txt"))
                {
                    string resLine = inputFile.ReadLine();
                    if (resLine.Length > 0)
                    {
                        Events = JsonConvert.DeserializeObject<ObservableCollection<SportEvent>>(resLine);
                        

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Check path to file");
            }
                       
        }

        public void Save_Data()
        {
            string jsonStatistic = JsonConvert.SerializeObject(this.Events);
            string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            using (StreamWriter outputFile = new StreamWriter(myDocPath + @"\Events.txt"))
            {
                outputFile.WriteLine(jsonStatistic);
            }
        }
        public Statistic()
        {
            Events = new ObservableCollection<SportEvent>();
            LoadData();           
        }
        [DataMember()]
        public ObservableCollection<SportEvent> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
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
