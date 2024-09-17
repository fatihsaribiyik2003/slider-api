using AutoMapper;
using SubuProtokol.DataAccess.EntityFramework.Repositories;
using SubuProtokol.DataAccess.EntityFramework.UnitOfWork;
using SubuProtokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Services.EntityFramework.Managers
{


    public interface IUnitService
    {
        List<UnitModel> GetallUnit();

    }
        public class UnitService:IUnitService
    {
        private readonly IDatabase1UnitOfWork2 _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
      
        private readonly IUnitRepository _repository;

        public UnitService(IDatabase1UnitOfWork2 unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
          
            _repository = _unitOfWork.GetRepository<IUnitRepository>();
        }


        public List<UnitModel> GetallUnit()
        {

            return _mapper.Map<List<UnitModel>>(_repository.GetAll());
        }
    }
}
