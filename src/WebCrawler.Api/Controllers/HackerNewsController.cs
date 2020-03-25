using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawler.Core.Entities;
using WebCrawler.Core.Interfaces;

namespace WebCrawler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewService _hackerNewService;

        public HackerNewsController(IHackerNewService hackerNewService)
        {
            _hackerNewService = hackerNewService;
        }

        /// <summary>
        /// Hacker News List
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<HackerNew>> ListAsync()
        {
            var result = await _hackerNewService.FindAllAsync();
            return result;
        }
    }
}