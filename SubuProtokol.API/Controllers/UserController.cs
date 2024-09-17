using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubuProtokol.Models;
using SubuProtokol.Services.EntityFramework.Managers;

namespace SubuProtokol.API.Controllers
{
    [Authorize(Roles = "4")]
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : MyControllerBase
    {

        private readonly IProtokolUserService _protokolUserService;


        public UserController(IProtokolUserService protokolUserService)
        {

            _protokolUserService = protokolUserService;

        }
        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult ListAll()
        {
            var result = _protokolUserService.GetAll();

            return Success(result);
        }


        [HttpPost]
        [Route("User/Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult Create(UserProtokolModel model)
        {
            var result = _protokolUserService.Add(model);
            return Success(result);
        }

        [HttpGet]
        [Route("User/FindById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<UserProtokolModel>))]
        public IActionResult FindById(int id)
        {
            var result = _protokolUserService.GetById(id);

            if (result.Id != null)
            {
                return Success(result);
            }
            else
            {
                return Success(new ErrorDataResult<UserProtokolModel>(null, "Kayıt bulunamadı!"));
            }
        }

        [HttpPost]
        [Route("User/Edit")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<UserProtokolModel>))]
        public IActionResult Edit(int id, UserProtokolModel model)
        {
            var result = _protokolUserService.Update(id, model);

            return Success(new SuccessDataResult<UserProtokolModel>(result));
        }

        [HttpDelete]
        [Route("User/Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<ProtokolQuery>>))]
        public IActionResult Delete(int id)
        {

            if (id == null)
                return Error(Messages.ParameterRequired);


            _protokolUserService.Delete(id);
            return Success("Kullanıcı silindi.");
        }

    }
}
