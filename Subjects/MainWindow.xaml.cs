using CarsParser.SQL;
using HtmlAgilityPack;
using Parser.Core;
using Parser.Parsers;
using Parser.Subjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            string urlAddress = "https://www.ilcats.ru/toyota/?function=getModels&language=en&market=EU";

            HtmlDocument doc = HtmlDocReceiver.GetHtmlByUrl(urlAddress);
            ModelParser parser = new ModelParser();
            List<Model> models = await Task.Run(() => parser.Parse(doc).Result);

            SQLWorker sQLWorker = new SQLWorker("");
            sQLWorker.WriteDataToDBAsync(models);

        }
    }
}
