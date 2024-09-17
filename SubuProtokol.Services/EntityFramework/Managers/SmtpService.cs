using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using System.Net;
using SubuProtokol.Models;
using AutoMapper;
using SubuProtokol.DataAccess.EntityFramework.Repositories;
using SubuProtokol.DataAccess.EntityFramework.UnitOfWork;
using SubuProtokol.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using SubuProtokol.Entities.EntityFramework.Database1;

namespace SubuProtokol.Services.EntityFramework.Managers
{


    public interface ISmtpService
    {
        Task SendEmailAsync();
    }
    public class SmtpService : ISmtpService
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly IDatabase1UnitOfWork2 _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        //kullancağım repository
        private readonly IProtokolRepository _repository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserProtokolRepository _userProtokolRepository;

        public SmtpService(IOptions<SmtpOptions> smtpOptions, IDatabase1UnitOfWork2 unitOfWork, IMapper mapper, IUserProtokolRepository userProtokolRepository)
        {
            _smtpOptions = smtpOptions.Value;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //3. repostiroyme.unitofWorkumu ekliyorum.
            _repository = _unitOfWork.GetRepository<IProtokolRepository>();
            _unitRepository = _unitOfWork.GetRepository<IUnitRepository>();
            _userProtokolRepository = _unitOfWork.GetRepository<IUserProtokolRepository>();
        }

        public async Task SendEmailAsync()
        {
            var protokolResult = (from protokoller in _repository.GetAll()
                                  where protokoller.ProtokolFinishTime != null &&
                                  protokoller.ProtokolFinishTime > DateTime.Now &&
                                  protokoller.ProtokolFinishTime <= DateTime.Now.AddDays(1) &&
                                  protokoller.MailKontrol == false
                                  select protokoller).ToList();
            if (protokolResult.Count < 1)
                return;
            var userResult = (from kullanicilar in _userProtokolRepository.GetAll()
                              where kullanicilar.UserRole == (int)EnumUserRole.Admin /*&& kullanicilar.UserName == "dilarakaraca"*/
                              select new
                              {
                                  Email = kullanicilar.UserName,
                                  Data = string.Join("\n=========================\n", protokolResult.Select(x => $"<strong>Kurum Adı:</strong> <span style='font-weight:bold'>{x.ProtokolImzalananKurum}</span><br/><strong>Ana Süreç:</strong> {x.MainSurec}<br/><strong>Protokol Türü:</strong> {x.MainType}<br/><strong>Protokol Bitiş Süresi:</strong> <i>Protokole ait bitiş süresi:</i> {x.ProtokolFinishTime}"))
                              }).ToList();
            //<br/><strong>Düzenlemek İçin <a target=\"_blank\" href=\"https://protokol.subu.edu.tr/{x.Id}\"> Tıklayınız</a></strong>

            // SMTP configuration
            var smtpClient = new SmtpClient("subumail.subu.edu.tr", 587);
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("noreply@subumail.subu.edu.tr", "b5e2,Gd9zW");

            foreach (var item in userResult)
            {
                //Email configuration
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("noreply@subumail.subu.edu.tr");
                mailMessage.To.Add(item.Email + "@subu.edu.tr");
                mailMessage.Subject = "protokol süresi";
                mailMessage.Body = item.Data;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
            }

            foreach (var item in protokolResult)
            {
                item.MailKontrol = true;
                _repository.Update(item.Id, item);
                _repository.Save();
            }
        }
    }
}
