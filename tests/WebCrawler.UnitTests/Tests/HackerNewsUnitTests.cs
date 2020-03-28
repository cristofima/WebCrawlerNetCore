using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Core.Entities;
using WebCrawler.Infrastructure.Services;

namespace WebCrawler.UnitTests.Tests
{
    public class HackerNewsUnitTests
    {
        private HackerNewService hackerNewService;
        private IEnumerable<HackerNew> originalList;

        [SetUp]
        public void Setup()
        {
            this.hackerNewService = new HackerNewService();

            string textOriginal = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "/Files/original_list.json");

            this.originalList = JsonConvert.DeserializeObject<IEnumerable<HackerNew>>(textOriginal);
        }

        [Test]
        public void FindByFilter1()
        {
            // Arrange

            string textExpected = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "/Files/expected_list_filter_1.json");
            var expected = JsonConvert.DeserializeObject<IEnumerable<HackerNew>>(textExpected);

            // Act

            Task<IEnumerable<HackerNew>> task = Task.Run(async () => await this.hackerNewService.FindByFilter1(this.originalList));
            var result = task.Result;

            // Assert
            Assert.AreEqual(expected.Count(), result.Count());
            this.CheckValuesFromCollections(expected.ToList(), result.ToList());
        }

        [Test]
        public void FindByFilter2()
        {
            // Arrange

            string textExpected = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "/Files/expected_list_filter_2.json");
            var expected = JsonConvert.DeserializeObject<IEnumerable<HackerNew>>(textExpected);

            // Act

            Task<IEnumerable<HackerNew>> task = Task.Run(async () => await this.hackerNewService.FindByFilter2(this.originalList));
            var result = task.Result;

            // Assert
            Assert.AreEqual(expected.Count(), result.Count());
            this.CheckValuesFromCollections(expected.ToList(), result.ToList());
        }


        private void CheckValuesFromCollections(List<HackerNew> expectedList, List<HackerNew> resultList)
        {
            for (var i = 0; i < expectedList.Count(); i++)
            {
                Assert.AreEqual(expectedList[i].OrderNumber, resultList[i].OrderNumber);
                Assert.AreEqual(expectedList[i].Title, resultList[i].Title);
                Assert.AreEqual(expectedList[i].Points, resultList[i].Points);
                Assert.AreEqual(expectedList[i].CommentsAmount, resultList[i].CommentsAmount);
            }
        }
    }
}
