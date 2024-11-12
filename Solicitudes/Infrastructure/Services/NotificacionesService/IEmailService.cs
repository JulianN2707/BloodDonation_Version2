using System;
using System.Collections;
using Solicitudes.Domain.Dto;

namespace Solicitudes.Infrastructure.Services.NotificacionesService;

public interface IEmailService
{
    Task<SendEMailResponse> SendEMail(SendEMailRequest sendEmailRequest);
    string GetEMailBody(string templateName, Hashtable templateData);
    Task<SendEMailResponse> SendNotification(string bodyContent, List<string> recipients);

}
