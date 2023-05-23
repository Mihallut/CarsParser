using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Subjects;

namespace CarsParser.SQL
{
    public class Submodel
    {
        public string ModelCode { get; set; }
        public List<Complectation> Complectations { get; set; }
        public string ID { get; set; }
        public DateTime ProductionStartDate { get; set; }
        public DateTime? ProductionEndDate { get; set; }
    }
}
