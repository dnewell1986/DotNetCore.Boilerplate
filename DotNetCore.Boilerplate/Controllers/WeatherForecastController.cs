using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore.Boilerplate.Controllers
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

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			_logger.LogInformation("GET WeatherForecastController has been called");
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpGet("{id:int}")]
		public string GetById([FromRoute] int id)
		{
			if (id == 21)
			{
				var exception = new Exception("21 is not a number supported by this endpoint");
				_logger.LogError(exception, "This value is not supported by this endpoint");
			}
			var rng = new Random();
			var randomNumber = rng.Next(id, id + 100);
			_logger.LogInformation("Random number generated {0}", randomNumber);
			return randomNumber.ToString();
		}
	}
}
