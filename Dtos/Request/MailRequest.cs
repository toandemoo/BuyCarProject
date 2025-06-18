using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class MailRequest
    {
        public string EmailTo { get; set; }              // Địa chỉ gửi đến
        public string EmailName { get; set; }              // Địa chỉ gửi đến
        public string EmailSubject { get; set; }         // Chủ đề (tiêu đề email)
        public string EmailBody { get; set; }            // Nội dung (hỗ trợ HTML) của email
    }
}