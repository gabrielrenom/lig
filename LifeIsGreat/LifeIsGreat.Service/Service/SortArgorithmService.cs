using LifeIsGreat.Common.Interfaces;
using LifeIsGreat.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeIsGreat.Service.Service
{
    public class SortArgorithmService : ISortArgorithmService
    {
        /// <summary>
        /// Topological sorting Argorithm based in delegates.
        /// </summary>
        /// <param name="listofjobs"></param>
        /// <param name="getKey"></param>
        /// <param name="getDependencies"></param>
        /// <returns></returns>
        public ICollection<JobModel> GroupJobs(ICollection<JobModel> listofjobs, Func<JobModel, string> getKey, Func<JobModel, IEnumerable<string>> getDependencies)
        {
            var sorted = new List<JobModel>();
            var ischecked = new Dictionary<JobModel, bool>();

            // It goes thooug the list element by element.
            // For every element will rebuild the dependency list and will 
            // check any cyclic dependency
            listofjobs.ToList().ForEach(item=> IsJobCyclicChecked(item, ischecked, sorted, RebuildJobDependencyList(listofjobs, getKey, getDependencies)));            

            return sorted;
        }

        /// <summary>
        /// It rebuilds the depency list with delegates.
        /// </summary>
        /// <param name="listofjobs"></param>
        /// <param name="getKey"></param>
        /// <param name="getDependencies"></param>
        /// <returns></returns>
        private static Func<JobModel, IEnumerable<JobModel>> RebuildJobDependencyList(IEnumerable<JobModel> listofjobs, Func<JobModel, string> getKey, Func<JobModel, IEnumerable<string>> getDependencies)
        {
            // It maps the key
            var map = listofjobs.ToDictionary(getKey);

            // It remaps the dependencies
            return item =>
            {
                var dependencies = getDependencies(item);
                return dependencies?.Select(key => map[key]);
            };
        }

        /// <summary>
        /// Recursive function that checks the dyclic dependencies of the the jobs.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="ispassdicctionary"></param>
        /// <param name="listofjobs"></param>
        /// <param name="listofdependencies"></param>
        private void IsJobCyclicChecked(JobModel job, IDictionary<JobModel, bool> ispassdicctionary, ICollection<JobModel> listofjobs, Func<JobModel, IEnumerable<JobModel>> listofdependencies)
        {                        
            if (!ispassdicctionary.ContainsKey(job))
            {
                ispassdicctionary[job] = true;

                var dependencies = listofdependencies(job);

                // If the dependencies are not null
                // It will go trough them
                // and do it recursive
                dependencies?.ToList().ForEach(item=> IsJobCyclicChecked(item, ispassdicctionary, listofjobs, listofdependencies));

                ispassdicctionary[job] = false;
                listofjobs.Add(job);
            }
            else
            {
                if (ispassdicctionary[job])
                    throw new InvalidOperationException("Some jobs are causing circular dependencies");
            }
        }        
    }
}
