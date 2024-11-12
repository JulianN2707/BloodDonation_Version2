using System.Net;
using MailKit.Net.Smtp;
using MimeKit;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail;

public class EMailServices
{
    public async Task<bool> Send(MailData mailData)
    {
        var parser = new Parser(mailData.TemplateFileAbsolutePath, mailData.TemplateVars);

        try
        {
            var mailMessage = new MimeMessage();

            mailMessage.From.Add(MailboxAddress.Parse(mailData.From));

            if (mailData.ToRecipients == null)
            {
                throw new Exception("No  Target Recipients");
            }

            foreach (var toRcpt in mailData.ToRecipients)
            {
                mailMessage.To.Add(MailboxAddress.Parse(toRcpt.Address));
            }

            mailMessage.Subject = mailData.Subject;

            if (mailData.CcRecipients != null)
            {
                foreach (var ccRcpt in mailData.CcRecipients)
                {
                    mailMessage.Cc.Add(MailboxAddress.Parse(ccRcpt.Address));
                }
            }

            if (mailData.BccRecipients != null)
            {
                foreach (var bccRcpt in mailData.CcRecipients)
                {
                    mailMessage.Bcc.Add(MailboxAddress.Parse(bccRcpt.Address));
                }
            }

            var body = new BodyBuilder();
            body.HtmlBody = parser.Parse();
            mailMessage.Body = body.ToMessageBody();

            if (mailData.Attachments != null)
            {

                //foreach (IFormFile attachment in mailData.Attachments)
                //{
                //    // Check if length of the file in bytes is larger than 0
                //    if (attachment.Length > 0)
                //    {
                //        // Create a new memory stream and attach attachment to mail body
                //        using (MemoryStream memoryStream = new MemoryStream())
                //        {
                //            // Copy the attachment to the stream
                //            attachment.CopyTo(memoryStream);
                //            attachmentFileByteArray = memoryStream.ToArray();
                //        }
                //        // Add the attachment from the byte array
                //        body.Attachments.Add(attachment.FileName, attachmentFileByteArray, ContentType.Parse(attachment.ContentType));
                //    }
                //}
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(mailData.SmtpData.SmtpHost, mailData.SmtpData.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(mailData.SmtpData.Address, mailData.SmtpData.Password);
                await client.SendAsync(mailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                client.Dispose();
            }

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not send email: " + ex.Message);
        }
    }
}
