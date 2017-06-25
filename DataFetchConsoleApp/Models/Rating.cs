using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Models
{
    public class Rating
    {
        public string DataSourceName { get; set; }

        public int TotalVote { get; set; }

        public int FullPoint { get; set; }

        public double AveragePoint { get; set; }

    }
}
