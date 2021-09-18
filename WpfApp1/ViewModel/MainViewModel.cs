using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using WpfApp1.Model;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            peopleQuery = new Query
            {
                currentPage = 1,
                pageSize = 10,
                name = ""
            };

            QueryCommand = new RelayCommand(getList);
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand<long>(t => Edit(t));
            DelCommand = new RelayCommand<long>(t => Del(t));
        }

        private ObservableCollection<People> _gridModelList;

        public ObservableCollection<People> gridModelList
        {
            get { return _gridModelList; }
            set
            {
                _gridModelList = value;
                RaisePropertyChanged();
            }
        }

        #region Command

        public RelayCommand QueryCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand<long> EditCommand { get; set; }
        public RelayCommand<long> DelCommand { get; set; }

        #endregion

        private Query _peopleQuery;

        public Query peopleQuery
        {
            get { return _peopleQuery; }
            set
            {
                _peopleQuery = value;
                RaisePropertyChanged();
            }
        }

        public void getList()
        {
            string url = "http://localhost:8080/people/list";
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
            request.Method = "Post";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(peopleQuery));
            request.ContentLength = bytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();

                Result<Page> result = JsonConvert.DeserializeObject<Result<Page>>(str);
                var list = result.data.list;

                gridModelList = new ObservableCollection<People>();
                if (list != null)
                {
                    list.ForEach(arg => { gridModelList.Add(arg); });
                }
                
            }
        }

        public void Add()
        {
            var people = new People();
            var peopleView = new PeopleView(people);
            var r = peopleView.ShowDialog();
            if (r.Value)
            {
                SaveToDb(people);
                getList();
            }
        }

        public void Edit(long id)
        {
            string url = "http://localhost:8080/people/get/" + id;
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "application/json";

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();

                Result<People> result = JsonConvert.DeserializeObject<Result<People>>(str);
                var people = result.data;

                if (people != null)
                {
                    var peopleView = new PeopleView(people);
                    var r = peopleView.ShowDialog();
                    if (r.Value)
                    {
                        UpdateToDb(people);
                        this.getList();
                    }
                }
            }
        }

        public void SaveToDb(People people)
        {
            string url = "http://localhost:8080/people/save";
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
            request.Method = "Post";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(people));
            request.ContentLength = bytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();

                Result<int> result = JsonConvert.DeserializeObject<Result<int>>(str);

                var r = result.data;
            }
        }

        public void UpdateToDb(People people)
        {
            string url = "http://localhost:8080/people/update";
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
            request.Method = "Post";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(people));
            request.ContentLength = bytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();

                Result<int> result = JsonConvert.DeserializeObject<Result<int>>(str);

                var r = result.data;
            }
        }

        public void Del(long id)
        {
            var r = MessageBox.Show($"确定删除id为 {id} 的用户?", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (r == MessageBoxResult.OK)
            {
                string url = "http://localhost:8080/people/del/" + id;
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
                request.Method = "Get";
                request.KeepAlive = true;
                request.ContentType = "application/json";

                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string str = reader.ReadToEnd();
                    Result<int> result = JsonConvert.DeserializeObject<Result<int>>(str);
                    if (result.data == 1)
                    {
                        MessageBox.Show("删除成功！");
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                    }
                }
            }

            this.getList();
        }
    }
}