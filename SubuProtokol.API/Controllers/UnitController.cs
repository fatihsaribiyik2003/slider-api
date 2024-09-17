using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubuProtokol.Models;
using SubuProtokol.Services.EntityFramework.Managers;
using Microsoft.AspNetCore.Authorization;
namespace SubuProtokol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : MyControllerBase
    {

        private readonly IUnitService _unitservice;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UnitController(IUnitService unitService, IMapper mapper, IFileService fileService)
        {
            _unitservice = unitService;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<UnitModel>>))]
        public IActionResult UnitGetAll()
        {

            return Success(_mapper.Map<IEnumerable<UnitModel>>(_unitservice.GetallUnit()));
        }

    }
}
