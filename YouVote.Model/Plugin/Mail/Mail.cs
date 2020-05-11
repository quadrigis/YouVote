using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace YouVote.Model.Plugin.Mail
{
    public class Mail : IMail
    {
        public string Message { get; set; }
        public string Topic { get; set; }
        public string Sender { get; set; }
        public string Adres { get; set; }
        public bool IsHtmlBody { get; set; }

        public IEnumerable<Attachment> Attachments { get; set; }

        private readonly SmtpClient _server;

        public Mail()
        {
            var login = ConfigurationManager.AppSettings["smtpLogin"] ?? "";
            var password = ConfigurationManager.AppSettings["smtpPassword"] ?? "";
            var domain = ConfigurationManager.AppSettings["smtpDomain"] ?? "";
            var serverCredential = new NetworkCredential(login, password, domain);
            _server = new SmtpClient(serverCredential.Domain)
                          {
                              Port =
                                  ConfigurationManager.AppSettings["smtpPort"] != null
                                      ? Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"])
                                      : 25,
                              Credentials =
                                  new NetworkCredential(serverCredential.UserName, serverCredential.Password)
                          };
            if (string.IsNullOrEmpty(serverCredential.UserName)) throw new ArgumentException("ServerCredential.UserName");
        }

        public void Send()
        {
            var mailObj = new MailMessage(Sender, Adres, Topic, Message) {IsBodyHtml = IsHtmlBody};
            foreach (var z in Attachments)
            {
                mailObj.Attachments.Add(z);
            }
            _server.Send(mailObj);
        }
    }
}

