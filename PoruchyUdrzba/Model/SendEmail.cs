using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PoruchyUdrzba.Model
{
    class SendEmail
    {
        public void emailClose(int errorId)
        {
            Database db = new Database();
            DataTable dt = db.getRecord(errorId);
            DataTable emailData = db.getEmails(errorId);
            string htmlMessage = getMessage(dt);
            emailSend(htmlMessage, emailData, dt);
        }
        
        private string getMessage(DataTable dt)
        {
            string  typeError= dt.Rows[0]["type"].ToString();
            string detailError = "";
            if (typeError== "budova")
            {
                detailError = "Porucha č. " + dt.Rows[0]["poruchy_ID"].ToString() + "-" + dt.Rows[0]["stredisko"].ToString() + "-" + dt.Rows[0]["type"].ToString();
            }
            else
            {
                 detailError = "Porucha č. " + dt.Rows[0]["poruchy_ID"].ToString() + "-" + dt.Rows[0]["stredisko"].ToString() + "-" +
                    dt.Rows[0]["machine"].ToString() + "-" + dt.Rows[0]["type"].ToString();
            }
            
            string message = System.IO.File.ReadAllText("Image/EmailClose.html");
            message = message.Replace("#Detail_Opravy#", detailError);
            message = message.Replace("#datumOD#", dt.Rows[0]["startTime"].ToString());
            message = message.Replace("#datumDO#", dt.Rows[0]["finishTime"].ToString());
            message = message.Replace("#CommetOP#", dt.Rows[0]["commentOp"].ToString());
            message = message.Replace("#CommetMT#", dt.Rows[0]["commentM"].ToString());
            message = message.Replace("#NameMT#", dt.Rows[0]["maintenance"].ToString());

            return message;
        }
        private void emailSend(string htmlMessage, DataTable emailData, DataTable dt)
        {
            
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("192.168.100.201");

                for (int i = 0; i < emailData.Rows.Count; i++)
                {
                    mail.To.Add(emailData.Rows[i]["Email"].ToString());
                }
                if (dt.Rows[0]["type"].ToString() == "Elektro")
                {
                    mail.To.Add("ales.duchac@bnint.cz");
                }
                //mail.To.Add("lukas.vanek@bnint.cz");
                mail.From = new MailAddress("poruchy@bnint.cz");
                mail.Subject = "Uzavření opravy č. " + dt.Rows[0]["poruchy_ID"].ToString();
                mail.Body = htmlMessage;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 25;
               

                SmtpServer.Send(mail);
            }
            catch (Exception x) 
            {
                LogClass log = new LogClass();
                log.writeLog(x.ToString());
            }
        }
    }
}
