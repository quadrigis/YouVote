using System.Collections.Generic;
using System.Net.Mail;

namespace YouVote.Model.Plugin.Mail
{
    interface IMail
    {
        string Message { get; set; }
        string Topic { get; set; }
        string Sender { get; set; }
        string Adres { get; set; }
        bool IsHtmlBody { get; set; }
        IEnumerable<Attachment> Attachments { get; set; }

        void Send();
    }
}

