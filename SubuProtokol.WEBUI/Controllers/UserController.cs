using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SubuProtokol.Core.Enums;
using SubuProtokol.WEBUI.Models;
using SubuProtokol.WEBUI.Service;

namespace SubuProtokol.WEBUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiService _apiService;

        public UserController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult GetAllUser()
        {
            var result = _apiService.GetAllUser();
            return View(result);
        }
    
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateUser([FromForm] ApiUserModel model)
        {
            foreach (var item in model.GetType().GetProperties())
            {
                if (item.Name == "Id")
                    continue;

                var value = item.GetValue(model, null);
                if (value == null)
                {
                    TempData["user"] = "Eksik bilgileri doldurunuz.";
                    return RedirectToAction("CreateUser", "User");
                }
            }

            var result = _apiService.CreateUser(model);
            return RedirectToAction("GetAllUser","User");
        }
        public IActionResult EditUser(int id)
        {
            var result = _apiService.UserGetById(id);

            var userRoleSelectListItems = Enum.GetValues(typeof(EnumUserRole))
                .Cast<EnumUserRole>()
                .Select(q => new SelectListItem
                {
                    Selected = false,
                    Text = q.ToString(),
                    Value = q.GetHashCode().ToString()
                });

            ViewData["UserRoleSelectListItems "] = new SelectList(userRoleSelectListItems);

            return View(result);
        }
        [HttpPost]
        public IActionResult EditUser(int id, ApiUserModel model)
        {
            var result = _apiService.EditUser(id, model);
          return   RedirectToAction("GetAllUser", "User");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _apiService.DeleteUser(id);
            return Ok(Json(result));
        }
    }
}
