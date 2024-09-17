using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RestSharp;
using RestSharp.Extensions;
using Microsoft.AspNetCore.Authorization;
using SubuProtokol.WEBUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using NuGet.Common;
using Newtonsoft.Json.Linq;
using System.Net;
using NuGet.Protocol.Plugins;
using MongoDB.Bson;
using SubuProtokol.Services.EntityFramework.Managers;
using SubuProtokol.Core.Enums;
using MongoDB.Bson.IO;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using SharpCompress.Common;
using System.Diagnostics;
using System.Net.Http.Headers;


namespace SubuProtokol.WEBUI.Service
{

    public interface IApiService
    {
        IEnumerable<ProtokolQuery> List();
        ProtokolQuery GetById(int id);
        ApiUserLoginModel Auth(ApiUserLoginModel model);
        (IEnumerable<ProtokolQuery>, string) ListAll();
        IEnumerable<ApiUserModel> GetAllUser();
        ApiUserModel CreateUser(ApiUserModel model);
        ApiUserModel EditUser(int id, ApiUserModel model);
        ApiUserModel UserGetById(int id);
        ApiUserModel DeleteUser(int id);
        ProtokolQuery DeleteProtokol(int id);
        ProtokolModel ProtokolCreate(ProtokolQuery model);
        ProtokolModel Update(int id, ProtokolQuery model);
        IEnumerable<UnitModel> GetAllUnit();

        List<ProtokolQuery> TarihFilter(FilterModel model);
    }
    public class ApiService : IApiService
    {
        private readonly IApiServiceClient _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        // public static string token;
        public ApiService(IApiServiceClient apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;

        }
        public ApiUserLoginModel Auth(ApiUserLoginModel model)
        {
            RestRequest request = new RestRequest("/Auth/Login/api/login/" + model.UserName + "/" + model.Password, Method.Post);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");


            var response = _apiService.Client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
            {
                var responseJson = response.Content;
                string token = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson)["token"].ToString();
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var result = handler.ReadJwtToken(token);

                ClaimsIdentity identity = new ClaimsIdentity(result.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddHours(12)
                };
                var principal = new ClaimsPrincipal(identity);
                model.UserRole = identity.Claims.ToList()[1].Value.ToString();
                model.UserName = identity.Name;

                _httpContextAccessor.HttpContext.Response.Cookies.Append("session_token", token, cookieOptions);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("role", model.UserRole, cookieOptions);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("username", model.UserName, cookieOptions);

                return model;
            }
            return model;
        }
        public ProtokolModel Update(int id, ProtokolQuery model)
        {
            RestRequest request = new RestRequest("/Protokol/Update/Protokol/Update", Method.Post);
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);

            request.AddUrlSegment("id", id);


            request.AddBody(new ProtokolModel
            {
                ProtokolFinishTime = model.ProtokolFinishTime,
                ProtokolStartTime = model.ProtokolStartTime,
                protokoldensorumlubirim = "",
                ProtokoldenSorumluKisi = model.ProtokoldenSorumluKisi,
                ProtokolImzalananKurum = model.ProtokolImzalananKurum,
                Scope = model.Scope,
                Time = model.Time,
                Sector = model.Sector,
                MainType = model.MainType,
                Email = model.Email,
                indirimAciklamasi = model.indirimAciklamasi,
                MainSurec = model.MainSurec,
                DepartmantType = model.DepartmantType,
                UnderType = model.UnderType,
                FileKey = model.FileKey,
                Konu = model.Konu,
                Telefon = model.Telefon,
                Id = id,
                UnitId = model.UnitId,
                UnitName = "",
                EmailImzalananKurum=model.EmailImzalananKurum,

            });
            //       var response = await _apiService.Client.PostAsync<ApiResponse<object>>(request);
            var response = _apiService.Client.Execute<ApiAuthResponse<ProtokolModel>>(request);
            return null;




        }
        public List<ProtokolQuery> TarihFilter(FilterModel model)
        {
            RestRequest request = new RestRequest("/Protokol/FilterGet", Method.Post);
            var access_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + access_token);
            request.AddBody(new FilterModel
            {
                Tarih1 = model.Tarih1,
                Tarih2 = model.Tarih2,
                KurumAdi = model.KurumAdi
            });
            var response = _apiService.Client.Post<List<ProtokolQuery>>(request);
            return response;
        }
        public ProtokolModel ProtokolCreate(ProtokolQuery model)
        {
            try
            {
                RestRequest request = new RestRequest("/Protokol/ProtokolCreate/Protokol/Create", Method.Post);

                var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
                request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);
                request.AddBody(new ProtokolModel
                {
                    ProtokolFinishTime = model.ProtokolFinishTime,
                    ProtokolStartTime = model.ProtokolStartTime,
                    protokoldensorumlubirim = "",
                    ProtokoldenSorumluKisi = model.ProtokoldenSorumluKisi,
                    ProtokolImzalananKurum = model.ProtokolImzalananKurum,
                    Scope = model.Scope,
                    Time = model.Time,
                    Sector = model.Sector,
                    MainType = model.MainType,
                    Email = model.Email,
                    indirimAciklamasi = model.indirimAciklamasi,
                    MainSurec = model.MainSurec,
                    DepartmantType = model.DepartmantType,
                    UnderType = model.UnderType,
                    FileKey = model.FileKey,
                    Konu = model.Konu,
                    Telefon = model.Telefon,
                    UnitId = model.UnitId,
                    UnitName = ""
                });

                var response = _apiService.Client.Post<ApiResponse<ProtokolQuery>>(request);

            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
            return null;
        }
        public IEnumerable<ProtokolQuery> List()
        {

            RestRequest request = new RestRequest("/Protokol/getall", Method.Get);

            var response = _apiService.Client.Get<ApiResponse<IEnumerable<ProtokolQuery>>>(request);

            return response.data;
        }
        public (IEnumerable<ProtokolQuery>, string) ListAll()
        {
            RestRequest request = new RestRequest("/Protokol/ProtokolList", Method.Get);
            //var acces_token = _httpContextAccessor.HttpContext.Session.GetString("session_token");
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);

            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(acces_token);
            var tokenS = jwtSecurityToken as JwtSecurityToken;

            var role = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

            var response = _apiService.Client.Get<ApiResponse<IEnumerable<ProtokolQuery>>>(request);

            return (response.data, role);

        }
        public ProtokolQuery GetById(int id)
        {
            RestRequest request = new RestRequest("/Protokol/GetById", Method.Get);
            request.AddParameter("id", id);
            var response = _apiService.Client.Get<ApiResponse<ProtokolQuery>>(request);

            return response.data;
        }
        public IEnumerable<ApiUserModel> GetAllUser()
        {
            RestRequest request = new RestRequest("/api/User", Method.Get);
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);
            var response = _apiService.Client.Get<ApiResponse<IEnumerable<ApiUserModel>>>(request);
            return response.data;
        }
        public IEnumerable<UnitModel> GetAllUnit()
        {

            RestRequest request = new RestRequest("/api/Unit", Method.Get);
            string acces_token = _httpContextAccessor.HttpContext.Session.GetString("session_token");
            request.AddHeader("Authorization", "Bearer " + acces_token);
            var response = _apiService.Client.Get<ApiResponse<IEnumerable<UnitModel>>>(request);
            return response.data;
        }
        public ApiUserModel CreateUser(ApiUserModel model)
        {
            RestRequest request = new RestRequest("/api/User/User/Create", Method.Post);
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);

            request.AddBody(new ApiUserModel
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                FamilyName = model.FamilyName,
                Email = model.Email,
                Phone = model.Phone,
                UserRole = model.UserRole

            });
            var response = _apiService.Client.Post<ApiResponse<ApiUserModel>>(request);
            return response.data;

        }
        public ApiUserModel UserGetById(int id)
        {
            RestRequest request = new RestRequest("/api/User/User/FindById", Method.Get);
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);
            request.AddParameter("id", id);
            var response = _apiService.Client.Get<ApiResponse<ApiUserModel>>(request);

            return response.data;
        }
        public ApiUserModel EditUser(int id, ApiUserModel model)
        {
            //TODO: Düzenlenecek.
            RestRequest request = new RestRequest("/api/User/User/Edit", Method.Post);
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);
            request.AddQueryParameter("id", id);
            request.AddBody(new ApiUserModel
            {
                Id= id,
                UserName = model.UserName,
                FirstName = model.FirstName,
                FamilyName = model.FamilyName,
                Email = model.Email,
                Phone = model.Phone,
                UserRole = model.UserRole
            });

            var response = _apiService.Client.Post<ApiResponse<ApiUserModel>>(request);
            return response.data;
        }
        [HttpDelete]
        public ApiUserModel DeleteUser(int id)
        {
            RestRequest request = new RestRequest("/api/User/User/Delete", Method.Delete);
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);
            request.AddParameter("id", id);
            var response = _apiService.Client.Delete<ApiResponse<ApiUserModel>>(request);
            return response.data;
        }
        [HttpDelete]
        public ProtokolQuery DeleteProtokol(int id)
        {
            RestRequest request = new RestRequest("/Protokol/Delete/Protokol/Delete", Method.Delete);
            var acces_token = _httpContextAccessor.HttpContext.Request.Cookies["session_token"];
            request.AddOrUpdateHeader("Authorization", "Bearer " + acces_token);
            request.AddQueryParameter("id", id);
            var response = _apiService.Client.Delete<ApiResponse<ProtokolQuery>>(request);
            return response.data;
        }
    }
}
