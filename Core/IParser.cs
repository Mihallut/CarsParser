using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core
{
    public interface IParser<T> where T : class
    {
        public Task<List<T>> Parse(HtmlDocument document);
    }
}
