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

        /// <summary>
        /// Hacker News List by filter 1
        /// </summary>
        [HttpGet]
        [Route("filter1")]
        public async Task<IEnumerable<HackerNew>> ListFilter1Async()
        {
            var result = await _hackerNewService.FindByFilter1();
            return result;
        }

        /// <summary>
        /// Hacker News List by filter 2
        /// </summary>
        [HttpGet]
        [Route("filter2")]
        public async Task<IEnumerable<HackerNew>> ListFilter2Async()
        {
            var result = await _hackerNewService.FindByFilter2();
            return result;
        }
    }
}