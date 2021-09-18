using System.Windows;
using MahApps.Metro.Controls;
using WpfApp1.Model;

namespace WpfApp1.Views
{
    public partial class PeopleView : MetroWindow
    {
        public PeopleView(People people)
        {
            InitializeComponent();
            this.DataContext = new
            {
                Model = people
            };
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}