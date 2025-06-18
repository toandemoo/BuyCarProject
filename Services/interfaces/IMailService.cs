using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Models;

namespace Project.Services.interfaces
{
    public interface IMailService
    {
        Task<object> SendMail(MailRequest mailContents);
    }
}