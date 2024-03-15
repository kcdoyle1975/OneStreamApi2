using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi2.Controllers
{
    [Route("api/the_flash/runner")]
    [ApiController]
    public class TheFlashRunnerController : ControllerBase
    {
        private IMemoryCache _cache;

        public TheFlashRunnerController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET: api/<RunnerController>
        [HttpGet]
        public long Get()
        {
            if (!_cache.TryGetValue(true, out int delay))
            {
                delay = 0;
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < 100; i++)
            {
                Task.Delay(delay).Wait();
            }
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }


        // POST api/<RunnerController>
        [HttpPost]
        public void Post([FromBody] int delay)
        {
            _cache.Set<int>(true, delay);
        }
    }
}
