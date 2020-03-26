using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawler.Core.Entities;

namespace WebCrawler.Core.Interfaces
{
    public interface IHackerNewService
    {
        Task<IEnumerable<HackerNew>> FindAllAsync();

        Task<IEnumerable<HackerNew>> FindByFilter1();

        Task<IEnumerable<HackerNew>> FindByFilter2();
    }
}
