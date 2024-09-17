using AutoMapper;
using SubuProtokol.DataAccess.EntityFramework.Repositories;
using SubuProtokol.DataAccess.EntityFramework.UnitOfWork;
using SubuProtokol.Entities.EntityFramework.Database1;
using SubuProtokol.Entities.Mongo;
using SubuProtokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Services.EntityFramework.Managers
{
    public interface IProtokolUserService
    {
        List<UserProtokolModel> GetAll();
        UserProtokolModel GetByUserName(string userName);
        UserProtokolModel GetById(int id);

        UserProtokolModel Add(UserProtokolModel model);
        UserProtokolModel Update(int id, UserProtokolModel model);
        void Delete(int id);
        UserLoginModel Authenticate(string username);
    }
    public  class ProtokolUserService :IProtokolUserService
    {
        //1
        private readonly IDatabase1UnitOfWork2 _unitOfWork;
        private readonly IMapper _mapper;
        //kullancağım repository
        private readonly IUserProtokolRepository _repository;

        public ProtokolUserService(IDatabase1UnitOfWork2 unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //3. repostiroyme.unitofWorkumu ekliyorum.
            _repository = _unitOfWork.GetRepository<IUserProtokolRepository>();
        }


        public UserLoginModel Authenticate(string username)
        {
            //  var model = _mapper.Map<UserLoginModel>(_repository.GetAll().Where(x => x.UserName == username).First().UserName);
            //  var model = _mapper.Map<UserLoginModel>(_repository.GetAll().Where(x => x.UserName == username).First().UserName);
            if (_repository.GetAll().Where(x => x.UserName == username).FirstOrDefault() != null)
            {
                UserLoginModel xx = _mapper.Map<UserLoginModel>(_repository.GetAll().Where(x => x.UserName == username).First());

                return xx;
            }
            else return null;
        }
        public List< UserProtokolModel >GetAll()
        {
            return _mapper.Map<List<UserProtokolModel>>(_repository.GetAll());
        }

        public UserProtokolModel GetById(int id)
        {
            return _mapper.Map<UserProtokolModel>(_repository.Get(id));
        }

        public UserProtokolModel GetByUserName(string userName)
        {
            var query = _repository.GetAll().Where(x=>x.UserName==userName).Select(q=>q.Id).FirstOrDefault();
           
            return _mapper.Map<UserProtokolModel>(_repository.Get(query));
          
        }

        public UserProtokolModel Add(UserProtokolModel model)
        {
            {
                User user = _repository.Add(_mapper.Map<User>(model));
                _repository.Save();

                return _mapper.Map<UserProtokolModel>(user);
            }
        }

        public UserProtokolModel Update(int id, UserProtokolModel model)
        {
            User user = _repository.Get(id);
            _repository.Update(id, _mapper.Map(model, user));
            _repository.Save();
            return _mapper.Map<UserProtokolModel>(user);

        }

        public void Delete(int id)
        {
            _repository.Remove(id);
            _repository.Save();
        }
    }
}
