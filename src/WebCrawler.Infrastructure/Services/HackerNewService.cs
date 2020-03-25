using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCrawler.Core.Entities;
using WebCrawler.Core.Interfaces;

namespace WebCrawler.Infrastructure.Services
{
    public class HackerNewService : IHackerNewService
    {
        public async Task<IEnumerable<HackerNew>> FindAllAsync()
        {
            // The url of the page we want to test
            var url = "https://news.ycombinator.com/";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var entries = new List<HackerNew>();

            var tableElement = htmlDocument.DocumentNode.Descendants("table")
                .Where(node => node.GetAttributeValue("class", "").Equals("itemlist"))
                .FirstOrDefault();

            if (tableElement != null)
            {
                var rows = tableElement.Descendants("tr").ToList();

                for (var i = 0; i < rows.Count; i++)
                {
                    var row = rows[i];

                    var firstElement = row.Descendants("td")
                        .Where(node => node.GetAttributeValue("class", "").Equals("title"))
                        .Where(node => node.GetAttributeValue("align", "").Equals("right"))
                        .FirstOrDefault();

                    if (firstElement == null)
                    {
                        break;
                    }

                    var secondElement = row.Descendants("td")
                        .Where(node => node.GetAttributeValue("class", "").Equals("title"))
                        .Skip(1)
                        .FirstOrDefault();

                    i++;

                    row = rows[i];

                    var thirdElement = row.Descendants("td")
                        .Where(node => node.GetAttributeValue("class", "").Equals("subtext"))
                        .FirstOrDefault();

                    var orderNumberText = firstElement.Descendants("span")
                        .FirstOrDefault()
                        .InnerText
                        .Replace(".", "");

                    var pointsText = thirdElement.Descendants("span")
                            .Where(node => node.GetAttributeValue("class", "").Equals("score"))
                            .FirstOrDefault()
                            .InnerText
                            .Replace(" points", "");

                    var commentsAmountText = thirdElement.Descendants("a")
                            .LastOrDefault()
                            .InnerText
                            .Replace("&nbsp;", "")
                            .Replace("comments", "");

                    var pointsNumber = 0;
                    var commentsNumber = 0;

                    int.TryParse(pointsText, out pointsNumber);
                    int.TryParse(commentsAmountText, out commentsNumber);

                    var entry = new HackerNew
                    {
                        OrderNumber = int.Parse(orderNumberText),
                        Title = secondElement.Descendants("a").FirstOrDefault().InnerText,
                        Points = pointsNumber,
                        CommentsAmount = commentsNumber
                    };

                    entries.Add(entry);

                    i++;
                }
            }

            return entries;
        }
    }
}
