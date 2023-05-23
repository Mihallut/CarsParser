using HtmlAgilityPack;
using Parser.Core;
using Parser.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Parser.Parsers
{
    public class ModelParser : IParser<Model>
    {
        public async Task<List<Model>> Parse(HtmlDocument document)
        {
            List<Model> models = new List<Model>();

            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'List')][.//div[contains(@class, 'List')]][.//div[contains(@class, 'Header')]]");
            nodes.RemoveAt(0); //по какой-то принчине (я так и не нашел по какой) метод DocumentNode.SelectNodes всегда записывает первую найденную ноду дважды
            foreach (HtmlNode node in nodes)
            {
                Model model = new Model();
                model.Name = node.SelectSingleNode(".//div[contains(@class, 'name')]").InnerHtml;
                SubmodelParser subparser = new SubmodelParser();
                HtmlDocument nodeAsHtmlDoc = new HtmlDocument();
                nodeAsHtmlDoc.LoadHtml(node.InnerHtml);
                model.Submodels = await Task.Run(() => subparser.Parse(nodeAsHtmlDoc).Result);
                models.Add(model);
            }

            return models;
        }


    }
}
