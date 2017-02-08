using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace CommonWeal.NGOWeb.CommonOperation
{
    public class EmailGateway
    {

        
        public bool sendEmail(string receiver, string subject, string attachment, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(
                  "commonweal9@gmail.com", ".netgroup");
                MailMessage msg = new MailMessage();
                string[] ToMuliId = receiver.Split(';');
                foreach (string ToEMailId in ToMuliId)
                {
                    msg.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id
                }

                msg.From = new MailAddress("commonweal9@gmail.com");
                msg.Subject = subject;
                string body = string.Empty;
                //using streamreader for reading my htmltemplate   

                body = message;
                msg.IsBodyHtml = true;

                msg.Body = body;

                if (attachment != "")
                {

                    msg.Attachments.Add(new Attachment(attachment));
                }

                client.Send(msg);

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }


    }


    }
