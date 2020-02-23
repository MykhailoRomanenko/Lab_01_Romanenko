using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab_01_Romanenko.Models;
using Lab_01_Romanenko.Tools;

namespace Lab_01_Romanenko.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _a, _w, _c;

        // private int _ageToDisplay;
        // private string _chineseToDisplay;
        // private string _westernToDisplay;
        public MainWindowViewModel()
        {
            AstroDate = new AstrologicalDate();
            UpdateCommand = new DateUpdateCommand(this);
        }

        public ICommand UpdateCommand { get; }

        public string AgeToDisplay
        {
            get => _a;
            set
            {
                _a = value;
                OnPropertyChanged("AgeToDisplay");
            }
        }

        public string WesternToDisplay
        {
            get => _w;
            set
            {
                _w = value;
                OnPropertyChanged("WesternToDisplay");
            }
        }

        public string ChineseToDisplay
        {
            get => _c;
            set
            {
                _c = value;
                OnPropertyChanged("ChineseToDisplay");
            }
        }

        public AstrologicalDate AstroDate { get; }

        public async void UpdateDisplayedValues()
        {
            if (!IsValid(AstroDate.SelectedDate))
            {
                MessageBox.Show(AstroDate.SelectedDate.ToShortDateString() + " is an invalid date of birth.");
            }
            else
            {
                if(AstroDate.SelectedDate.Month==DateTime.Today.Month&&AstroDate.SelectedDate.Day == DateTime.Today.Day)
                    MessageBox.Show("Happy Birthday!");

                await Task.Run(() =>
                {
                    //async proof
                    Thread.Sleep(1000);
                    
                    AgeToDisplay = $"{AstroDate.CalculateAge()} y. o.";
                    WesternToDisplay = AstroDate.CalculateWestern();
                    ChineseToDisplay = AstroDate.CalculateChinese();
                });
            }
        }


        private bool IsValid(DateTime d)
        {
            if (DateTime.Compare(d, DateTime.Today) > 0)
                return false;
            if (DateTime.Today.Year - d.Year > 135)
                return false;
            return true;
        }

        public bool CanUpdate()
        {
            if (AstroDate == null)
                return false;
            return AstroDate.SelectedDate != null;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}