using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeIsGreat.Common.Models
{
    public class JobModel
    {
        public string Name { get; set; }
        public string[] JobDependencyList { get; set; }

        public JobModel(string name, params string[] jobdependencylist)
        {
            Name = name;
            JobDependencyList = jobdependencylist;
        }        
    }
}
