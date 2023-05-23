using CarsParser.Core;
using HtmlAgilityPack;
using Parser.Core;
using Parser.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Parser.Parsers
{
    public class ComplectationParser : IParser<Complectation>
    {

        public async Task<List<Complectation>> Parse(HtmlDocument document)
        {
            List<Complectation> complectations = new List<Complectation>();
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            HtmlNode headerNode = document.DocumentNode.SelectSingleNode("//h1[text()='Select Car Complectation']");


            FillDictionary(dictionary, headerNode);

            CreateComplactations(complectations, dictionary);
            return complectations;

        }
        //summary
        //создание объектов окмплектаций из dictionary
        private static void CreateComplactations(List<Complectation> complectations, Dictionary<string, List<string>> dictionary)
        {
            var properties = typeof(Complectation)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public);


            for (int i = 0; i < dictionary.Values.First().Count; i++)
            {
                Complectation complectation = new Complectation();

                foreach (List<string> list in dictionary.Values)
                {
                    string key = dictionary.FirstOrDefault(x => x.Value == list).Key;
                    string value = list[i];


                    switch (key)
                    {
                        // Name
                        case "Complectation":
                            {
                                complectation.Name = value;
                                break;
                            }
                        // Dates
                        case "Date":
                            {
                                DatesParser.ParseDates(value, out DateTime startDate, out DateTime? endDate);
                                complectation.ProductionStartDate = startDate;
                                complectation.ProductionEndDate = endDate;
                                break;
                            }
                        default:
                            {
                                var filteredColumnName = key
                                          .Replace(" ", "")
                                          .Replace(",", "")
                                          .Replace("(", "")
                                          .Replace(")", "")
                                          .Replace(".", "")
                                          .Replace("'", "");
                                // Find and set property value
                                var property = properties
                                    .First(e => string.Equals(e.Name, filteredColumnName,
                                        StringComparison.InvariantCultureIgnoreCase));
                                property.SetValue(complectation, value);
                                break;
                            }
                    }
                }
                complectations.Add(complectation);

            }
        }

        //summary
        //Вычитывание всех данных из таблицы комплектаций и сохранение их в dictionary,
        //где key - название характеристики, value - список всех значений с столбца
        private static void FillDictionary(Dictionary<string, List<string>> dictionary, HtmlNode headerNode)
        {
            if (headerNode != null)
            {
                // Поиск таблицы table, находящейся под заголовком
                HtmlNode tableNode = headerNode.SelectSingleNode("following::table[1]");

                if (tableNode != null)
                {
                    // Получение всех строк таблицы
                    HtmlNodeCollection rowNodes = tableNode.SelectNodes(".//tr");
                    HtmlNodeCollection headers = rowNodes[0].SelectNodes(".//th");
                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (!dictionary.ContainsKey(headers[i].InnerText))
                        {
                            dictionary.Add(headers[i].InnerText, new List<string>());
                        }
                    }
                    rowNodes.RemoveAt(0);

                    foreach (HtmlNode rowNode in rowNodes)
                    {
                        // Получение всех ячеек текущей строки
                        HtmlNodeCollection cellNodes = rowNode.SelectNodes(".//td");

                        for (int i = 0; i < cellNodes.Count; i++)
                        {

                            dictionary[headers[i].InnerText].Add(cellNodes[i].InnerText);
                        }

                    }
                }
            }
        }
    }
}
