using AutoMapper;
using Microsoft.Extensions.Primitives;
using SubuProtokol.Core.Enums;
using SubuProtokol.DataAccess.EntityFramework.Repositories;
using SubuProtokol.DataAccess.EntityFramework.UnitOfWork;
using SubuProtokol.Entities.EntityFramework.Database1;
using SubuProtokol.Models;
using SubuProtokol.Models.Filter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SDFSClientCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;


namespace SubuProtokol.Services.EntityFramework.Managers
{

    public interface IProtokolService
    {
        ProtokolQuery Create(ProtokolModel model);
        ProtokolQuery GetByMainType(EnumProtokolMainType? maintype_);

        List<ProtokolQuery> Getall();

        List<ProtokolQuery> ProtokolList(string name);


        ProtokolQuery GetAll(ProtokolGetAllFilter? filter, PaginationFilter? paginationFilter);
        ProtokolQuery GetallOgr(ProtokolGetAllFilter filter);
        ProtokolQuery GetById(int id);
        ProtokolQuery Update(int id, ProtokolModel model);

        List<ProtokolQuery> GetAllFilter(FilterModel model);
        ProtokolQuery GettableId(int? id);
        void Delete(int id);
        public string UploadFile( IFormFile? file, List<string> contentTypes = null);
        List<ProtokolQuery> ProtokolListByTarih(DateTime tarih1, DateTime tarih2);



    }

    public class ProtokolService : IProtokolService
    {
        //1
        private readonly IDatabase1UnitOfWork2 _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        //kullancağım repository
        private readonly IProtokolRepository _repository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserProtokolRepository _userProtokolRepository;
      

        public ProtokolService(IDatabase1UnitOfWork2 unitOfWork, IMapper mapper, IUserProtokolRepository userProtokolRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //3. repostiroyme.unitofWorkumu ekliyorum.
            _repository = _unitOfWork.GetRepository<IProtokolRepository>();
            _unitRepository = _unitOfWork.GetRepository<IUnitRepository>();
            _userProtokolRepository = _unitOfWork.GetRepository<IUserProtokolRepository>();
           
        }

       

        public ProtokolQuery GetAll(ProtokolGetAllFilter? filter, PaginationFilter? paginationFilter)
        {

            var query = _repository.GetAll().Where(x => x.MainType == filter.MainType).AsQueryable();

            if (filter.sectorId != null && filter.sectorId.ToString() != "0")
            {
                query = query.Where(q => q.Sector == filter.sectorId);
            }

            if (filter.departmantTypeId != null && filter.departmantTypeId.ToString() != "0")
            {
                query = query.Where(q => q.DepartmantType == filter.departmantTypeId);
            }

            if (filter.scopeId != null && filter.scopeId.ToString() != "0")
            {
                query = query.Where(q => q.Scope == filter.scopeId);
            }

            if (paginationFilter.Skip > 0)
            {
                query = query.Skip(paginationFilter.Skip);
            }

            if (query.Any())
            {
                Protokol protokol = _mapper.Map<Protokol>(query);

                //foreach (var item in query)
                //{
                //    item.FileKey= _fileService.Get(item.FileKey).Data;
                //}

            }
            return _mapper.Map<ProtokolQuery>(query.ToList());

        }


        public List<ProtokolQuery> GetAllFilter(FilterModel model)
        {
            var dataFilter = _repository.Queryable();
            #region filtreleme
            if(!string.IsNullOrEmpty(model.Tarih1) && !string.IsNullOrEmpty(model.Tarih2))
            {
                DateTime IlkTarih = DateTime.Parse(model.Tarih1);
                DateTime IlkTarih2 = DateTime.Parse(model.Tarih2);
                dataFilter = (from kayitlar in dataFilter
                              where (DateTime.Compare(IlkTarih, (DateTime)kayitlar.ProtokolStartTime) < 0 &&
                              DateTime.Compare((DateTime)kayitlar.ProtokolStartTime!, IlkTarih2) <= 0) ||
                              (DateTime.Compare(IlkTarih, (DateTime)kayitlar.ProtokolFinishTime) < 0 &&
                              DateTime.Compare((DateTime)kayitlar.ProtokolFinishTime!, IlkTarih2) <= 0)
                              select kayitlar).AsQueryable();
            }
            if (!string.IsNullOrEmpty(model.KurumAdi))
            {
                dataFilter = dataFilter.Where(x => x.ProtokolImzalananKurum.ToLower().Contains(model.KurumAdi.ToLower()));

            }
            #endregion
            var result = _mapper.Map<List<ProtokolQuery>>(dataFilter.ToList());
            return result;
        }

        public List<ProtokolQuery> ProtokolList(string name )
        {
            //var protokols = _mapper.Map<List<ProtokolQuery>>(_repository.GetAll());
            //foreach (var item in protokols)
            //    item.UnitName = _unitRepository.GetAll().Where(x => x.Id == item.UnitId).FirstOrDefault().BirimAd;
            //return protokols;

            var loginUser = _userProtokolRepository.GetAll().Where(x => x.UserName == name).FirstOrDefault();
            var rol = loginUser.UserRole;
            if(rol != 4)
            {
                var protokolsunit = _mapper.Map<List<ProtokolQuery>>(_repository.GetAll());
                var birimProtokol = protokolsunit.Where(x=>x.silindimi==false || x.silindimi==null).Where(x => x.UnitId == Convert.ToInt32(loginUser.Phone)).ToList();
                foreach (var item in birimProtokol)
                    item.UnitName = _unitRepository.GetAll().Where(x => x.Id == item.UnitId).FirstOrDefault().BirimAd;
                return birimProtokol;
            }

            var protokols = _mapper.Map<List<ProtokolQuery>>(_repository.GetAll().Where(x=>x.silindimi==false || x.silindimi==false));
            foreach (var item in protokols)
                item.UnitName = _unitRepository.GetAll().Where(x => x.Id == item.UnitId).FirstOrDefault().BirimAd;
            return protokols;
        }

        public List<ProtokolQuery> ProtokolListByTarih(DateTime tarih1, DateTime tarih2)
        {
            var allProtokoller = _repository.GetAll().Where(x=>x.silindimi==false).ToList();
            var filteredProtokoller = allProtokoller
                .Where(p => p.ProtokolStartTime > tarih1 && p.ProtokolFinishTime < tarih2)
                .ToList();
            var result = _mapper.Map<List<ProtokolQuery>>(filteredProtokoller);
            return result;
        }


        public List<ProtokolQuery> Getall()
        {
            var protokols = _mapper.Map<List<ProtokolQuery>>(_repository.GetAll().Where(x => x.MainType != EnumProtokolMainType.IndirimProtokolu).Where(x=>x.silindimi==false||x.silindimi==null).AsQueryable());
            foreach (var item in protokols)
                item.UnitName = _unitRepository.GetAll().Where(x => x.Id == item.UnitId).FirstOrDefault().BirimAd;
            return protokols;
        }

        public ProtokolQuery GetallOgr(ProtokolGetAllFilter? filter)
        {
            var query = _repository.GetAll().Where(x => x.MainType != EnumProtokolMainType.IndirimProtokolu).AsQueryable();
            Protokol protokol = new Protokol();
            if (query.Any())
            {
                if (filter.MainType.ToString() == "DegisimProgrami" || filter.MainType.ToString() == "UniversiteIsDunyasiIsbirligi" || filter.MainType.ToString() == "GenelIsbirligi" || filter.MainType.ToString() == "EgitimBelgelendirme" || filter.MainType.ToString() == "Danismanlik")
                {
                    protokol = _mapper.Map<Protokol>(query.Where(x => x.MainType == filter.MainType).ToList());


                }


                else
                {
                    protokol = _mapper.Map<Protokol>(query.ToList());
                }
            }

            return _mapper.Map<ProtokolQuery>(protokol);
        }

        public ProtokolQuery GettableId(int? id)
        {
            var query = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            Protokol protokol = new Protokol();
            if (query != null)
            {
                protokol = _mapper.Map<Protokol>(query);
            }
            return _mapper.Map<ProtokolQuery>(protokol);
        }

        public ProtokolQuery GetByMainType(EnumProtokolMainType? maintype)
        {
            var query = _repository.GetAll().FirstOrDefault(q => q.MainType == maintype);
            Protokol protokol = new Protokol();
            if (query != null)
            {
                protokol = _mapper.Map<Protokol>(query);
            }
            return _mapper.Map<ProtokolQuery>(protokol);
        }

        public ProtokolQuery Update(int id, ProtokolModel model)
        {
            Protokol protokol = _repository.Get(id);
            if (model.protokolfiltreleme != 2 || model.protokolfiltreleme != 1)
            {
                protokol.arsiv = false;
                protokol.silindimi = false;
                _repository.Update(id, _mapper.Map(model, protokol));
                _repository.Save();

                return _mapper.Map<ProtokolQuery>(protokol);
            }
            if ((protokol.protokolfiltreleme == 1 || protokol.protokolfiltreleme == 2) && protokol.ProtokolFinishTime != model.ProtokolFinishTime )
            {
                if (model.protokolfiltreleme == 2)
                {
                    protokol.arsiv = true;
                    protokol.silindimi = false;
                    _repository.Update(id, _mapper.Map(model, protokol));
                    _repository.Save();
                    if (protokol.parentprotokolid != null)
                    {
                        model.parentprotokolid = protokol.parentprotokolid;
                    }
                    else {
                        model.parentprotokolid = protokol.Id;
                    }
                    _repository.Add(_mapper.Map<Protokol>(model));
                    _repository.Save();

                    return _mapper.Map<ProtokolQuery>(protokol);
                }
                else
                {
                    model.arsiv = false;
                    model.silindimi = false;
                    _repository.Update(id, _mapper.Map(model, _mapper.Map(model, protokol)));
                    _repository.Save();

                    return _mapper.Map<ProtokolQuery>(protokol);
                }
            }

            _repository.Update(id, _mapper.Map(model, protokol));
            _repository.Save();

            return _mapper.Map<ProtokolQuery>(protokol);
        }
        public ProtokolQuery Create(ProtokolModel model)
        {
            //  Protokol protokol = _unitOfWork.GetRepository<IProtokolService>().Create(model);
            var result = _mapper.Map<Protokol>(model);
            model.silindimi = false;
            model.arsiv = false;
            var protokol = _repository.Add(result);
            _repository.Save();
            return _mapper.Map<ProtokolQuery>(protokol);
        }
        public ProtokolQuery GetById(int id)
        {
            return _mapper.Map<ProtokolQuery>(_repository.Get(id));

        }

        

        public void Delete(int id)
        {
            var model= _repository.Get(id);
            if(model != null)
            {
                model.silindimi = true;
                model.arsiv = true;
                _repository.Update(id, model);
                _repository.Save();
            }

        }

        public string UploadFile(IFormFile? file, List<string> contentTypes = null)
        {
            if (file != null)
            {
                if (file.Length > 8 * 1024 * 1024) throw new Exception("Dosya boyutu fazla.");

                if (contentTypes != null)
                    if (!contentTypes.Select(s => new ContentType(s)).Any(c => c.ToString() == file.ContentType))
                        return string.Empty;

                var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                var Url = configuration.GetValue<string>("FileService:Url");
                var UserName = configuration.GetValue<string>("FileService:UserName");
                var Password = configuration.GetValue<string>("FileService:Password");

                //result.Type = Core.Enums.EnumResponseResultType.Error;
                new AppSettings(Url, UserName, Password);

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);

                    var fileBytes = ms.ToArray();

                    var key = FileClient.Upload(UserName, fileBytes, file.FileName, true, UserName, Password);

                    if (!string.IsNullOrEmpty(key))
                        return key;
                }
            }

            return string.Empty;
        }
    }
}

