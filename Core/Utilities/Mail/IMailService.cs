using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Mail
{
    public interface IMailService
    {
        void Send(Mail mail);
    }
}
