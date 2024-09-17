using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson.IO;
using RestSharp;
using SDFSClientCore;
using SubuProtokol.Core.Enums;
using SubuProtokol.Entities.EntityFramework.Database1;
using SubuProtokol.Models.Filter;
using SubuProtokol.Services.EntityFramework.Managers;
using SubuProtokol.WEBUI.Models;
using SubuProtokol.WEBUI.Service;
using System.Linq;
using System.Security.Claims;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SubuProtokol.WEBUI.Controllers
{
    public class ProtokolController : Controller
    {


        private readonly IApiService _apiService;
     

        public ProtokolController(IApiService apiService)
        {
            _apiService = apiService;
        
        }
        public IActionResult Index()
        {
            return View();
        }


       
        public IActionResult ProtokolListPartial(int id)
        {
            var result = _apiService.GetById(id);
            return PartialView("_deneme", result);
        }


        public IActionResult ProtokolListOgr(int? page, int? MainType, ProtokolGetAllFilter filter, FilterModel model)
        {
            if(MainType != null)
            {
                ViewBag.CurrentFilter = MainType.Value;
            }
            ViewBag.KurumAdi = model.KurumAdi;
             var result = _apiService.List();

            if (filter.MainType.ToString() == "DegisimProgrami" || filter.MainType.ToString() == "UniversiteIsDunyasiIsbirligi" || filter.MainType.ToString() == "GenelIsbirligi" || filter.MainType.ToString() == "EgitimBelgelendirme" || filter.MainType.ToString() == "Danismanlik")
            {
                result = _apiService.List().Where(x => x.MainType == filter.MainType).ToList();
                ViewData["MainType"] = MainType;

            }

            var pagenumber = (page ?? 1);
            ViewBag.CurrentPage = page;

            
            return View(result.ToPagedList(pagenumber,10));


        }
        [HttpPost]
        public JsonResult GetSearchingDataOgr( string SearchBy, string SearchValue)
        {
            if (SearchValue != null)
            {
                var data = _apiService.List().ToList(); 
                SearchValue = SearchValue.ToLower();
                data = data.Where(x => string.IsNullOrEmpty(x.ProtokolImzalananKurum) != true ? x.ProtokolImzalananKurum.ToLower().Contains(SearchValue) : 1 == 1).ToList();
                return Json(data);
            }

            return Json(null);
        }
        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            var data = _apiService.ListAll().Item1.ToList();


            if (SearchValue != null)
            {
                SearchValue = SearchValue.ToLower();
                data = data.Where(x => string.IsNullOrEmpty(x.ProtokolImzalananKurum) != true ? x.ProtokolImzalananKurum.ToLower().Contains(SearchValue) : 1 == 1).ToList();
                return Json(data);
            }
           
            return null;
        }

        public IActionResult ProtokolListAll(int? page , int? MainType, ProtokolGetAllFilter filter)
        {
            if (MainType != null)
            {
                ViewBag.CurrentFilter = MainType.Value;
                
            }


          

           var result = _apiService.ListAll().Item1;
            
            if (filter.MainType.ToString() == "DegisimProgrami" || filter.MainType.ToString() == "UniversiteIsDunyasiIsbirligi" || filter.MainType.ToString() == "GenelIsbirligi" || filter.MainType.ToString() == "EgitimBelgelendirme" || filter.MainType.ToString() == "Danismanlik" ||filter.MainType.ToString() == "IndirimProtokolu")
            {
                result = _apiService.ListAll().Item1.Where(x => x.MainType == filter.MainType).ToList();
                ViewData["MainType"] =  MainType;
               

            }

            var pagenumber = (page ?? 1);
            ViewBag.CurrentPage = page;
            ViewBag.Role = _apiService.ListAll().Item2;
          

            return View(result.ToPagedList(pagenumber, 10));



        }

        public IActionResult ProtokolListAllPersonel(int? page, int? MainType, ProtokolGetAllFilter filter, FilterModel model)
        {

            if (MainType != null)
            {
                ViewBag.CurrentFilter = MainType.Value;
            }
            ViewBag.KurumAdi = model.KurumAdi;
            var result = _apiService.List();

            if (filter.MainType.ToString() == "DegisimProgrami" || filter.MainType.ToString() == "UniversiteIsDunyasiIsbirligi" || filter.MainType.ToString() == "GenelIsbirligi" || filter.MainType.ToString() == "EgitimBelgelendirme" || filter.MainType.ToString() == "Danismanlik")
            {
                result = _apiService.List().Where(x => x.MainType == filter.MainType).ToList();
                ViewData["MainType"] = MainType;

            }

            var pagenumber = (page ?? 1);
            ViewBag.CurrentPage = page;


            return View(result.ToPagedList(pagenumber, 10));

        }
    }
    }
