using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubuProtokol.Core.Enums;
using SubuProtokol.Models.Filter;
using SubuProtokol.Models;
using SubuProtokol.Services.EntityFramework.Managers;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Web.Helpers;
using SDFSClientCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Misc;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace SubuProtokol.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    
        public class ProtokolController : MyControllerBase
        {
        private readonly IProtokolService _protokolService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ISmtpService _smtpService;

        public ProtokolController(IProtokolService protokolService, IMapper mapper, IFileService fileService, ISmtpService smtpService)
        {
            _protokolService = protokolService;
            _mapper = mapper;
            _fileService = fileService;
            _smtpService = smtpService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<ProtokolQuery>))]
        public IActionResult FilterGet([FromBody] FilterModel model)
        {
            try
            {
                var result = _protokolService.GetAllFilter(model);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(new ErrorDataResult<string>(e.StackTrace));
            }

        }


        [Authorize]
        [HttpDelete]
        [Route("Protokol/Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult Delete(int id)
        {

            if (id == null)
                return Error(Messages.ParameterRequired);


            _protokolService.Delete(id);
            return Success("Protokol silindi.");
        }

        [Authorize]
        [HttpPost]
        [Route("Protokol/FileKey")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult FileKeyUret(IFormFile? dosya)
        {

            if (!dosya.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase) &&
                     !dosya.ContentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                     !dosya.ContentType.Equals("image/jpg", StringComparison.OrdinalIgnoreCase) &&
                     !dosya.ContentType.Equals("image/png", StringComparison.OrdinalIgnoreCase) &&
                       !dosya.ContentType.Equals("image/png", StringComparison.OrdinalIgnoreCase)) 
                _protokolService.UploadFile(dosya);
            var filekey = _protokolService.UploadFile(dosya);

            return Ok(filekey);
        }

        //[Authorize]
        //[HttpPost]
        //[Route("Protokol/Edit")]

        //public IActionResult Edit(int id, [FromForm] ProtokolModel model)
        //{
        //    _protokolService.Update(id, model);
        //    return Success();
        //}
        [HttpGet]
        //[Route("Protokol/getall")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public  IActionResult getall()
        {
           
            return Success(_mapper.Map<IEnumerable<ProtokolQuery>>(  _protokolService.Getall()));
        }

        [HttpPost]
        [Route("MailGonder")]
        public async Task<IActionResult> MailGonder()
        {
              await _smtpService.SendEmailAsync();
            return Success();
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult ProtokolList()
        {
         
            string username = HttpContext.User.Identity.Name;

            var query = _mapper.Map<IEnumerable<ProtokolQuery>>(_protokolService.ProtokolList(username));
            if (query.Any())
            {
                foreach(var item in query)
                {
                    item.FileKey = _fileService.Get(item.FileKey);
                }
            }
            return Success(query);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult ProtokolListByTarih(DateTime tarih1, DateTime tarih2 )
        {

           

            var query = _mapper.Map<IEnumerable<ProtokolQuery>>(_protokolService.ProtokolListByTarih(tarih1, tarih2));
            if (query.Any())
            {
                foreach (var item in query)
                {
                    item.FileKey = _fileService.Get(item.FileKey);
                }
            }
            return Success(query);
        }
        //[HttpGet]
        //[Route("Protokol/GetallOgr")]
        //public IActionResult GetallOgr([FromForm] ProtokolGetAllFilter? filter)
        //{
        //    var result = _protokolService.GetallOgr(filter);
        //    return Success(result);
        //}
        //[HttpGet]
        //[Route("Protokol/GetByMainType")]
        //public IActionResult GetByMainType(EnumProtokolMainType? maintype)
        //{
        //    var result = _protokolService.GetByMainType(maintype);
        //    return Success(result);
        //}
        //[HttpGet]
        //[Route("Protokol/GettableId")]
        //public IActionResult GettableId(int? id)
        //{
        //    var result = _protokolService.GettableId(id);
        //    return Success(result);
        //}
        [HttpPost]
        [Authorize]
        [Route("Protokol/Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult Update(int id, [FromBody] ProtokolModel model)
        {
            if (model.FileKey != null)
            {
                var result = _protokolService.Update(model.Id, model);
                return Success(result);
            }
            else
            {
                var entity = _protokolService.GetById(model.Id);
                model.FileKey = entity.FileKey;
                var query = _protokolService.Update(model.Id, model);
                return Success(query);
            }
            return null;
        }




        [Authorize]
        [HttpPost]
        [Route("Protokol/Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]

        public IActionResult ProtokolCreate(ProtokolModel model)
        {

            var result = _protokolService.Create(model);

            return Success(result);

        }

        [HttpGet]
        //[Route("Protokol/getall")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult GetById(int id)
        {
            var result = _protokolService.GetById(id);
            return Success(result);
        }
    }
    }
    
