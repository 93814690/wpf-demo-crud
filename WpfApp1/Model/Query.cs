using GalaSoft.MvvmLight;

namespace WpfApp1.Model
{
    public class Query : ViewModelBase
    {
        private int _currentPage;

        private int _pageSize;

        private string _name;

        public int currentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged();
            }
        }

        public int pageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                RaisePropertyChanged();
            }
        }

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
    }
}