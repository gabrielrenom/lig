using LifeIsGreat.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeIsGreat.Common.Interfaces
{
    public interface ISortArgorithmService
    {
        ICollection<JobModel> GroupJobs(ICollection<JobModel> listofjobs, Func<JobModel, string> getKey, Func<JobModel, IEnumerable<string>> getDependencies);        
     }
}
