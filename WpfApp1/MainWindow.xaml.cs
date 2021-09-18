using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            

            MainViewModel viewModel = new MainViewModel();
            viewModel.getList();

            this.DataContext = viewModel;
           
        }


    }

    public class Result<T>
    {
        public string code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }

    public class Page
    {
        public string total { get; set; }
        public List<People> list { get; set; }
        public string pageNum { get; set; }
        public string pageSize { get; set; }
        public string size { get; set; }
        public string startRow { get; set; }
        public string endRow { get; set; }
        public string pages { get; set; }
        public string prePage { get; set; }
        public string nextPage { get; set; }
        public string isFirstPage { get; set; }
        public string isLastPage { get; set; }
        public string hasPreviousPage { get; set; }
        public string hasNextPage { get; set; }
    }
    
}
