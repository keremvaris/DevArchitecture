using Core.Utilities.Mail.FakeMail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Mail
{
    public interface INotificationService
    {
        void Send(Mail mail);
    }
    public class NotificationService:INotificationService
    {
        IMailService _mailService;
        internal NotificationService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public void Send(Mail mail)
        {
            _mailService.Send(mail);
        }
    }
}
