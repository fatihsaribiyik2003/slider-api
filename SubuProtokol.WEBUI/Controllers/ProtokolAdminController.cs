using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using SubuProtokol.Models.Filter;
using SubuProtokol.WEBUI.Service;
using SubuProtokol.WEBUI.Models;
using System.Security.Claims;
using SubuProtokol.Services.EntityFramework.Managers;
using Microsoft.AspNetCore.Http;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using SubuProtokol.Core.Enums;

namespace SubuProtokol.WEBUI.Controllers
{
    public class ProtokolAdminController : Controller
    {


        private readonly IApiService _apiService;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _accessor;
        private string Url = "";
        private string UserName = "";
        private string Password = "";
        readonly IHostingEnvironment _hostingEnvironment;


        public string Get(string fileKey)
        {
            string result = null;

            if (fileKey != null)
            {
                var hostUrl = _config.GetSection("HostUrl").Value;
                result = hostUrl + "file/" + fileKey;

            }
            else
            {
                result = null;
            }

            return result;
        }

        public string Upload(IFormFile file)
        {
            string result = null;
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "file");
            var fileName = Guid.NewGuid() + file.FileName;
            var filePath = Path.Combine(uploadPath, fileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }

            result = fileName;

            return result;
        }

        public ProtokolAdminController(IApiService apiService, IConfiguration config, IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor)
        {
            _apiService = apiService;
            _config = config;
            var fileSettengs = _config.GetSection("FileService");
            Url = fileSettengs.GetSection("Url").Value;
            UserName = fileSettengs.GetSection("UserName").Value;
            Password = fileSettengs.GetSection("Password").Value;
            _hostingEnvironment = hostingEnvironment;
            _accessor = accessor;
        }

        public IActionResult ProtokolCreate()
        {
            var result = _apiService.GetAllUnit();
            ViewBag.unit = result;
            return View();
        }


        [HttpPost]
        public IActionResult ProtokolCreate(ProtokolQuery model)
        {
            var contentType = new System.Net.Mime.ContentType("application/pdf").ToString();

            if (model.File != null && model.File.ContentType != contentType)
                return RedirectToAction("ProtokolCreate", "ProtokolAdmin");

            if (model.File != null)
            {
                string result = null;
                //result.Type = Core.Enums.EnumResponseResultType.Error;
                new SDFSClientCore.AppSettings(Url, UserName, Password);

                using (var ms = new MemoryStream())
                {
                    model.File.CopyTo(ms);

                    var fileBytes = ms.ToArray();

                    var key = SDFSClientCore.FileClient.Upload(UserName, fileBytes, model.File.FileName, true, UserName, Password);
                    if (!string.IsNullOrEmpty(key))
                    {
                        result = key;
                        //result.Type = Core.Enums.EnumResponseResultType.Success;
                    }
                }
                model.FileKey = result;
                var query = _apiService.ProtokolCreate(model);


                return RedirectToAction("Index", "ProtokolAdmin");
            }
            var resultmodel = _apiService.ProtokolCreate(model);
            return RedirectToAction("Index", "ProtokolAdmin");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _apiService.DeleteProtokol(id);
            TempData["Message"] = "Kayıt Başarılı Şekilde Silindi.";
            return Ok(Json(result));
        }

        [HttpPost]
        public IActionResult Index(int? page, FilterModel model)
        {

            var Tarih1 = model.Tarih1;
            var Tarih2 = model.Tarih2;
            var KurumAdi = model.KurumAdi;
            var query = _apiService.TarihFilter(model);
            var pagenumber = (page ?? 1);
            ViewBag.CurrentPage = page;
            ViewBag.StartDate = model.Tarih1;
            ViewBag.EndDate = model.Tarih2;
            ViewBag.KurumAdi = model.KurumAdi;
            ViewBag.data = query.ToList();

            if(model.Tarih1 != null && model.Tarih2 != null)
            {
                var yeniDeger = (from protokoller in _apiService.ListAll().Item1
                                 where (protokoller.Time == Core.Enums.EnumProtokolTime.Evet
                                                  && (protokoller.ProtokolFinishTime.HasValue == true &&
                                                  protokoller.ProtokolFinishTime.Value < DateTime.Parse(model.Tarih1)))
                                                  //|| (protokoller.ProtokolFinishTime.HasValue == true &&
                                                  //protokoller.ProtokolFinishTime.Value < DateTime.Parse(model.Tarih1))
                                 select protokoller).Count();

                var buYilkiVeriler = (from protokoller in _apiService.ListAll().Item1.Where(x=>x.ProtokolFinishTime.HasValue && x.ProtokolStartTime.HasValue)
                                      where protokoller.ProtokolStartTime!.Value.Year <= DateTime.Parse(model.Tarih1).Year &&
                                      protokoller.ProtokolFinishTime!.Value.Year == DateTime.Parse(model.Tarih2).Year
                                      select protokoller).Count();

                ViewBag.SecilenYilBilgisi = DateTime.Parse(model.Tarih1).Year + " yılında imzalanan " + DateTime.Parse(model.Tarih2).Year +" yılında biten "+ query.Count() + " adet yeni protokol imzalanmıştır.";
                ViewBag.OncekiYillar = DateTime.Parse(model.Tarih1).Year + " yılından önceki senelerde imzalanan ve otomatik uzayacak protokollerin sayısı " + yeniDeger + " adettir.";
            }

            ViewBag.EnumDepartmentType = Extensions.ConvertEnumToDictionary<EnumDepartmentType>();
            ViewBag.EnumMainSurec = Extensions.ConvertEnumToDictionary<enumprotokolmainsurec>();
            ViewBag.EnumMainType = Extensions.ConvertEnumToDictionary<EnumProtokolMainType>();
            ViewBag.EnumScope = Extensions.ConvertEnumToDictionary<EnumProtokolScope>();
            ViewBag.EnumSector = Extensions.ConvertEnumToDictionary<EnumSector>();
            ViewBag.EnumUnderType = Extensions.ConvertEnumToDictionary<EnumProtokolUnderType>();
            var birimler = _apiService.GetAllUnit().Select(x => new { Id = x.Id, Name = x.BirimAd }).ToList();
            ViewBag.Units = birimler;

            return View(query.ToPagedList(pagenumber, 10));

        }
        public IActionResult Index(int? page, int? MainType, ProtokolGetAllFilter filter, FilterModel model)
        {
            TempData["GirisYapmisKullanici"] = _accessor.HttpContext.Items["UserName"];
            TempData["GirisYapmisKullaniciRol"] = _accessor.HttpContext.Items["Role"];
            ViewBag.StartDate = model.Tarih1;
            ViewBag.EndDate = model.Tarih2;
            ViewBag.KurumAdi = model.KurumAdi;

            if (MainType != null)
            {
                ViewBag.CurrentFilter = MainType.Value;
            }

            var result = _apiService.ListAll().Item1;

            if (filter.MainType.ToString() == "DegisimProgrami" || filter.MainType.ToString() == "UniversiteIsDunyasiIsbirligi" || filter.MainType.ToString() == "GenelIsbirligi" || filter.MainType.ToString() == "EgitimBelgelendirme" || filter.MainType.ToString() == "Danismanlik" || filter.MainType.ToString() == "IndirimProtokolu")
            {
                result = _apiService.ListAll().Item1.Where(x => x.MainType == filter.MainType).ToList();


            }
            if (model.Tarih1 != null || model.Tarih2 != null || !string.IsNullOrEmpty(model.KurumAdi))
            {
                result = _apiService.TarihFilter(model);
            }

            var pagenumber = (page ?? 1);
            ViewBag.CurrentPage = page;
            ViewBag.data = result.ToList();

            ViewBag.EnumDepartmentType = Extensions.ConvertEnumToDictionary<EnumDepartmentType>();
            ViewBag.EnumMainSurec = Extensions.ConvertEnumToDictionary<enumprotokolmainsurec>();
            ViewBag.EnumMainType = Extensions.ConvertEnumToDictionary<EnumProtokolMainType>();
            ViewBag.EnumScope = Extensions.ConvertEnumToDictionary<EnumProtokolScope>();
            ViewBag.EnumSector = Extensions.ConvertEnumToDictionary<EnumSector>();
            ViewBag.EnumUnderType = Extensions.ConvertEnumToDictionary<EnumProtokolUnderType>();
            var birimler = _apiService.GetAllUnit().Select(x => new { Id = x.Id, Name = x.BirimAd }).ToList();
            ViewBag.Units = birimler;

            return View(result.ToPagedList(pagenumber, 10));

        }

        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            var data = _apiService.ListAll().Item1.ToList();


            if (SearchValue != null)
            {
                SearchValue = SearchValue.ToLower();
                data = data.FindAll(x => x.ProtokolImzalananKurum.ToLower().Contains(SearchValue));
                return Json(data);
            }

            return null;
        }

        [HttpGet("{id}")]
        public IActionResult UpdateProtokolById(int id)
        {
            _accessor.HttpContext.Items["UserName"] = TempData["GirisYapmisKullanici"];
            _accessor.HttpContext.Items["Role"] = TempData["GirisYapmisKullaniciRol"];
            var result = _apiService.GetById(id);
            ViewBag.ıd = id;
            return View(result);
        }

        [HttpPost]
        public IActionResult Update(int id, [FromBody] ProtokolQuery model)
        {


            if (model.File != null)
            {
                string result = null;
                //result.Type = Core.Enums.EnumResponseResultType.Error;
                new SDFSClientCore.AppSettings(Url, UserName, Password);

                using (var ms = new MemoryStream())
                {
                    model.File.CopyTo(ms);

                    var fileBytes = ms.ToArray();

                    var key = SDFSClientCore.FileClient.Upload(UserName, fileBytes, model.File.FileName, true, UserName, Password);
                    if (!string.IsNullOrEmpty(key))
                    {
                        result = key;
                        //result.Type = Core.Enums.EnumResponseResultType.Success;
                    }
                }
                model.FileKey = result;

                var query = _apiService.Update(id, model);


                return RedirectToAction("Index", "ProtokolAdmin", new { id = id });
            }

            var data = _apiService.Update(id, model);

            return RedirectToAction("Index", "ProtokolAdmin", new { id = id });
        }

    }
}
