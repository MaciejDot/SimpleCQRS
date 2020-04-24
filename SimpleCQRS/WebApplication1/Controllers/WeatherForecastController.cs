using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCQRS.Query;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IQueryProcessor _queryProcessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IQueryProcessor queryProcessor)
        {
            _logger = logger;
            _queryProcessor = queryProcessor;
        }
        public class Ans 
        { 
        
        }
        public class Get1 : IQuery<Ans> 
        { 
        
        }
        public class Hans : IQueryHandler<Get1, Ans>
        {
            public Task<Ans> Handle(Get1 query, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            IQuery<Ans> xs = new Get1();
            _queryProcessor.Process(new Get1(), default);
           
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
