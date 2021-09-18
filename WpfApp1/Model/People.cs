using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class People : ViewModelBase
    {
        private long _id;

        private string _username;
        private string _password;

        private string _name;

        private int _age;
        private string _sex;

        private DateTime _createTime;

        private DateTime _updateTime;

        public long id
        {
            get { return _id; }
            set
            {
                _id = value;RaisePropertyChanged();
            }
        }

        public string username
        {
            get { return _username; }
            set
            {
                _username = value; RaisePropertyChanged();
            }
        }

        public string password
        {
            get { return _password; }
            set
            {
                _password = value; RaisePropertyChanged();
            }
        }

        public string name
        {
            get { return _name; }
            set
            {
                _name = value; RaisePropertyChanged();
            }

        }



        public string sex
        {
            get { return _sex; }
            set
            {
                _sex = value; RaisePropertyChanged();
            }
        }


        public int age
        {
            get { return _age; }
            set
            {
                _age = value; RaisePropertyChanged();
            }
        }

        public DateTime createTime
        {
            get { return _createTime; }
            set
            {
                _createTime = value; RaisePropertyChanged();
            }
        }

        public DateTime updateTime
        {
            get { return _updateTime; }
            set
            {
                _updateTime = value; RaisePropertyChanged();
            }
        }
    }
}
