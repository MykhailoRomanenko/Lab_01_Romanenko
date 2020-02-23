using System;
using System.ComponentModel;

namespace Lab_01_Romanenko.Models
{
    public class AstrologicalDate : INotifyPropertyChanged
    {
        private static readonly DateTime DefaultDate = new DateTime(2000, 1, 1);

        private static readonly string[] Chinese =
            {"Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"};

        private static readonly string[] Western =
        {
            "Capricorn", "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra",
            "Scorpio", "Sagittarius"
        };

        private DateTime _selectedDateTime;

        public AstrologicalDate()
        {
            SelectedDate = DefaultDate;
        }

        public DateTime SelectedDate
        {
            get => _selectedDateTime; 
            set
            {
                _selectedDateTime = value;
                OnPropertyChanged("Date");
            }
        }

        public string CalculateChinese()
        {
            return Chinese[_selectedDateTime.Year % 12];
        }

        public string CalculateWestern()
        {
            var m = _selectedDateTime.Month;
            var d = _selectedDateTime.Day;
            var thresh = Threshold(m);
            if (d <= thresh) m = (m + 11) % 12;
            return Western[m];
        }

        public int CalculateAge()
        {
            var y = _selectedDateTime.Year;
            var m = _selectedDateTime.Month;
            var d = _selectedDateTime.Day;
            var todayM = DateTime.Today.Month;
            var todayD = DateTime.Today.Day;
            var age = DateTime.Today.Year - y;
            if (m > todayM || m == todayM && d > todayD)
                --age;
            return age;
        }

        private int Threshold(int m)
        {
            var thresh = -1;
            if (m >= 1 && m <= 6)
                thresh = 21;
            else if (m == 2)
                thresh = 20;
            else
                thresh = 22;
            return thresh;
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