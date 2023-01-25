using AutoFixStationContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationBusinessLogic.Mail
{
    public class MailKitWorker
    {
        protected static string mailLogin;
        protected static string mailPassword;
        protected static string smtpClientHost;
        protected static int smtpClientPort;
        protected static string popHost;
        protected static int popPort;

        public void MailConfig(MailConfigBindingModel config)
        {
            mailLogin = config.MailLogin;
            mailPassword = config.MailPassword;
            smtpClientHost = config.SmtpClientHost;
            smtpClientPort = config.SmtpClientPort;
            popHost = config.PopHost;
            popPort = config.PopPort;
        }

        public async void MailSendAsync(MailSendInfoBindingModel info)
        {
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text))
            {
                return;
            }
            await SendMailAsync(info);
        }
        protected async Task SendMailAsync(MailSendInfoBindingModel info)
        {
            using var objMailMessage = new MailMessage();
            using var objSmtpClient = new SmtpClient(smtpClientHost, smtpClientPort);
            try
            {
                objMailMessage.From = new MailAddress(mailLogin);
                objMailMessage.To.Add(new MailAddress(info.MailAddress));
                objMailMessage.Subject = info.Subject;
                objMailMessage.Body = info.Text;
                if (!string.IsNullOrEmpty(info.FileName) && File.Exists(info.FileName))
                {
                    Attachment attachment = new Attachment(info.FileName);
                    objMailMessage.Attachments.Add(attachment);
                }
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(mailLogin, mailPassword);
                await Task.Run(() => objSmtpClient.Send(objMailMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
