using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LifeIsGreat.Common.Interfaces;
using LifeIsGreat.Service;
using System.Linq;
using LifeIsGreat.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LifeIsGreat.Service.Service;

namespace LifeIsGreat.Test
{
    [TestClass]
    public class SortArgorithmTest
    {
        private ISortArgorithmService sortargorithmService;

        /// <summary>
        /// Initialize the test
        /// </summary>
        [TestInitialize]
        public void Setup() => sortargorithmService = new SortArgorithmService();       

        /// <summary>
        /// It test a normal sequence
        /// </summary>
        [TestMethod]
        public void When_Job_Added_Return_Sorted()
        {
            //Arrange
            var dependecieslist = new Collection<JobModel>()
            {
                    new JobModel("A"),
                    new JobModel("F"),
                    new JobModel("C"),
                    new JobModel("B"),
                    new JobModel("D"),
                    new JobModel("E")
            };
  

            //Act
            var result = sortargorithmService.GroupJobs(dependecieslist, x => x.Name, x => x.JobDependencyList).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 1);
            Assert.AreEqual(dependecieslist[0].Name, result[0].Name);
            Assert.AreEqual(dependecieslist[1].Name, result[1].Name);
            Assert.AreEqual(dependecieslist[2].Name, result[2].Name);
            Assert.AreEqual(dependecieslist[3].Name, result[3].Name);
            Assert.AreEqual(dependecieslist[4].Name, result[4].Name);
            Assert.AreEqual(dependecieslist[5].Name, result[5].Name);
        }

        /// <summary>
        /// It tests a sequence with dependencies
        /// </summary>
        [TestMethod]
        public void When_Job_Added_With_Dependency_Return_Sorted()
        {
            //Arrange            
            var dependecieslist = new Collection<JobModel>()
            {
                    new JobModel("A"),
                    new JobModel("B", "C"),
                    new JobModel("C", "F"),
                    new JobModel("D", "A"),
                    new JobModel("E", "B"),
                    new JobModel("F"),                    
            };

            //Act
            var result = sortargorithmService.GroupJobs(dependecieslist, x => x.Name, x => x.JobDependencyList).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 1);
            Assert.AreEqual("A", result[0].Name);
            Assert.AreEqual("F", result[1].Name);
            Assert.AreEqual("C", result[2].Name);
            Assert.AreEqual("B", result[3].Name);
            Assert.AreEqual("D", result[4].Name);
            Assert.AreEqual("E", result[5].Name);
        }

        /// <summary>
        /// It tests a sequence with circular dependencies
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void When_Job_Added_With_Circular_Dependency_Return_Exception()
        {
            //Arrange
            var a = new JobModel("A");
            var f = new JobModel("B", "C");
            var c = new JobModel("C", "F");
            var b = new JobModel("D", "A");
            var d = new JobModel("E", "A");
            var e = new JobModel("F", "B");

            var unsorted = new Collection<JobModel>() { a, b, c, d, e, f };

            //Act
            //Assert
            sortargorithmService.GroupJobs(unsorted, x => x.Name, x => x.JobDependencyList).ToList();            
        }

        /// <summary>
        /// It tests a sequence with invalid dependencies
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void When_Job_Added_With_Depend_ItSelf_Return_Exception()
        {
            //Arrange
            var dependecieslist = new Collection<JobModel>()
             {
                    new JobModel("A"),
                    new JobModel("F"),
                    new JobModel("C", "C")
            };

            //Act
            //Assert
            sortargorithmService.GroupJobs(dependecieslist, x => x.Name, x => x.JobDependencyList).ToList();            
        }
    }
}
