using Authorization.Shared.Dto.BackEnd;
using Authorization.WebBFFApplication.AppServices.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace Authorization.WebBFFApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IBackendApiService backendApiService;



    public WeatherForecastController(ILogger<WeatherForecastController> logger, IBackendApiService backendApiService)
    {
        _logger = logger;
        this.backendApiService = backendApiService;
    }

    [HttpGet(Name = "GetWeatherForecastHttpResponseMessage")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecastHttpResponseMessage()
    {

        var weatherForecast = await backendApiService.GetWeatherForecastHttpResponseMessage();
        return Ok(weatherForecast);

    }
}