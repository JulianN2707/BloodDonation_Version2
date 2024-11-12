using System.Collections;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail;
public class MailData
{
    public MailData()
    {
        From = string.Empty;
        Subject = string.Empty;
        ToRecipients = new List<MailAddress>();
        CcRecipients = new List<MailAddress>();
        BccRecipients = new List<MailAddress>();
        Attachments = null;
        BodyEncoding = Encoding.UTF8;
        IsHtml = true;
        Priority = MailPriority.Low;
        TemplateFileAbsolutePath = string.Empty;
        TemplateVars = new Hashtable();
        SmtpData = new SmtpData
        {
            Address = string.Empty,
            UseDefaultCredentials = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = false,
            SmtpPort = 25,
            SmtpHost = "localhost",
            Password = string.Empty,
            DeliveryFormat = SmtpDeliveryFormat.International
        };
    }

    public string From { get; set; }
    public string Subject { get; set; }
    public List<MailAddress> ToRecipients { get; set; }
    public List<MailAddress> CcRecipients { get; set; }
    public List<MailAddress> BccRecipients { get; set; }
    public AttachmentCollection Attachments { get; set; }
    public Encoding BodyEncoding { get; set; }
    public bool IsHtml { get; set; }
    public MailPriority Priority { get; set; }
    public string TemplateFileAbsolutePath { get; set; }
    public NameValueCollection Headers { get; set; }
    public Hashtable TemplateVars { get; set; }
    public DeliveryNotificationOptions DeliveryNotificationOptions { get; set; }
    public SmtpData SmtpData { get; set; }
}
