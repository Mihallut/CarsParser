using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsParser.SQL;

namespace Parser.Subjects
{
    public class Model
    {
        public string Name { get; set; }
        public List<Submodel> Submodels { get; set; }
    }

}
