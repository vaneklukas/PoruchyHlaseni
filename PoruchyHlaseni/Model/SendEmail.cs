using System;
using System.Data;
using System.Net.Mail;

namespace PoruchyHlaseni.Model
{
    internal class SendEmail
    {
        internal void sendNewFailure(string[] data, string dateTime,DataTable dt)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("192.168.100.201");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    mail.To.Add(dt.Rows[i]["Email"].ToString()); //TODO add list of senders
                }
                if (data[1]=="Elektro")
                {
                    mail.To.Add("ales.duchac@bnint.cz");
                }
                mail.From = new MailAddress("poruchy@bnint.cz");
                mail.Subject = "Nová porucha "+data[0];
                mail.Priority = MailPriority.High;
                mail.Body = getMessage(data,dateTime);
                mail.IsBodyHtml = true;

                SmtpServer.Port = 25;
                
                //SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception x) 
            {
                LogClass log = new LogClass();
                log.writeLog(x.ToString());
            }
        }
        private string getMessage(string[] data, string dateTime)
        {
            string detailError = "";
            if (data[1]=="Budova")
            {
                 detailError = data[0] + "-" + data[1];
            }
            else
            {
                 detailError = data[0] + "-" + data[2]+ "-" + data[1];
            }
            string message = System.IO.File.ReadAllText("Image/EmailOpen.html");
            message = message.Replace("#Detail_Opravy#", detailError);
            message = message.Replace("#datumOD#", dateTime);
            message = message.Replace("#CommetMT#", data[3]);
            message = message.Replace("#NameMT#", data[4]);

            return message;
        }
    }
}