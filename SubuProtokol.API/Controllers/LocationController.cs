using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SubuProtokol.Core;
using SubuProtokol.Models;
using SubuProtokol.Services.NoContext;


namespace SubuProtokol.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //public class LocationController : MyControllerBase
    //{
    //    private readonly ILocationService _locationService;
    //    private readonly IMemoryCache _memoryCache;

    //    public LocationController(ILocationService locationService, IMemoryCache memoryCache)
    //    {
    //        _locationService = locationService;
    //        _memoryCache = memoryCache;
    //    }

    //    [HttpGet("{id}")]
    //    [ResponseCache(Duration = 10, VaryByQueryKeys = new string[] { "id" })]
    //    //[ResponseCache(CacheProfileName = "TenSecond")]
    //    //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Get([FromRoute] int id)
    //    {
    //        //if (!_memoryCache.TryGetValue(CacheKeys.Weathers, out IEnumerable<WeatherForecast> weatherForecasts))
    //        //{
    //        //    weatherForecasts = _locationService.List();

    //        //MemoryCacheEntryOptions cacheEntryOptions
    //        //    = new MemoryCacheEntryOptions()
    //        //        .SetSlidingExpiration(TimeSpan.FromSeconds(10))
    //        //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
    //        //        .SetSize(1024);

    //        //    _memoryCache.Set(CacheKeys.Weathers, weatherForecasts, cacheEntryOptions);
    //        //}

    //        //return weatherForecasts;

    //        var data = _locationService.List();

    //        //var response = new ResponseResult<IEnumerable<WeatherForecast>>();
    //        //response.Success = true;
    //        //response.Messages.Add(Messages.DataListed);
    //        //response.Data = data;

    //        //return Ok(response);


    //        if (_locationService.IsExists("istanbul"))
    //        {
    //            return Error(Messages.CityIsExists);
    //        }
    //        else
    //        {
    //            return Success(data, Messages.DataListed, Messages.UserNotFound);
    //        }
    //    }

    //    [HttpDelete]
    //    public IActionResult Remove(int id)
    //    {
    //        //_locationService.Remove();
    //        _memoryCache.Remove(CacheKeys.Weathers);

    //        return Ok();
    //    }
    //}

    public class MyControllerBase : ControllerBase
    {
        [NonAction]
        public IActionResult Success<T>(T data, params string[] messages)
        {
            var response = new ResponseResult<T>();
            response.Success = true;
            response.Messages.AddRange(messages);
            response.Data = data;

            return Ok(response);
        }

        [NonAction]
        public IActionResult Success(params string[] messages)
        {
            var response = new ResponseResult<object>();
            response.Success = true;
            response.Messages.AddRange(messages);
            response.Data = null;

            return Ok(response);
        }

        [NonAction]
        public IActionResult Error(params string[] messages)
        {
            var response = new ResponseResult<object>();
            response.Success = false;
            response.Messages.AddRange(messages);
            response.Data = null;

            return BadRequest(response);
        }

        [NonAction]
        public IActionResult NotFound(params string[] messages)
        {
            var response = new ResponseResult<object>();
            response.Success = false;
            response.Messages.AddRange(messages);
            response.Data = null;

            return NotFound(response);
        }
    }
}
//}
