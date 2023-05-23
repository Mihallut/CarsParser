using CarsParser.Core;
using CarsParser.SQL;
using HtmlAgilityPack;
using Parser.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Parsers
{
    public class SubmodelParser : IParser<Submodel>
    {
        public async Task<List<Submodel>> Parse(HtmlDocument document)
        {
            List<Submodel> submodels = new List<Submodel>();
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'List')][.//div[contains(@class, 'id')]][.//div[contains(@class, 'dateRange')]][.//div[contains(@class, 'modelCode')]]");
            nodes.RemoveAt(0);
            foreach (HtmlNode node in nodes)
            {
                Submodel submodel = new Submodel();
                submodel.ID = node.SelectSingleNode(".//a").InnerHtml;
                submodel.ModelCode = node.SelectSingleNode(".//div[contains(@class, 'modelCode')]").InnerHtml;
                string dates = node.SelectSingleNode(".//div[contains(@class, 'dateRange')]").InnerHtml;
                DatesParser.ParseDates(dates, out DateTime startDate, out DateTime? endDate);
                submodel.ProductionStartDate = startDate;
                submodel.ProductionEndDate = endDate;


                string url = "https://www.ilcats.ru/" + node.SelectSingleNode(".//a").GetAttributeValue("href", "");
                Thread.Sleep(2000);
                HtmlDocument doc = HtmlDocReceiver.GetHtmlByUrl(url);
                ComplectationParser parser = new ComplectationParser();
                submodel.Complectations = await Task.Run(() => parser.Parse(doc).Result);



                submodels.Add(submodel);
            }

            return submodels;
        }

    }
}
