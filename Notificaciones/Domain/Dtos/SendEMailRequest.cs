namespace MVCT.Transversales.Services.Notificaciones.Domain.Dtos;

using Ardalis.GuardClauses;
using MVCT.Transversales.Services.Notificaciones.Domain.Enum;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public partial class SendEMailRequest
{
    public SendEMailRequest(string fromAddress, List<string> recipients, string fromName, string subject, string bodyContent, bool certification, NotificationType type)
    {
        Guard.Against.NullOrEmpty(fromAddress, nameof(fromAddress));
        Guard.Against.NullOrEmpty(recipients, nameof(recipients));
        Guard.Against.NullOrEmpty(fromName, nameof(fromName));
        Guard.Against.NullOrEmpty(subject, nameof(subject));
        Guard.Against.NullOrEmpty(bodyContent, nameof(bodyContent));

        FromAddress = fromAddress;
        Recipients = recipients;
        FromName = fromName;
        Subject = subject;
        BodyContent = bodyContent;
        Certification = certification;
        Type = type;
    }
    [JsonPropertyName("fromAddress")]
    public string FromAddress { get; set; }

    [JsonPropertyName("recipients")]
    public List<string> Recipients { get; set; }

    [JsonPropertyName("fromName")]
    public string FromName { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    [JsonPropertyName("bodyContent")]
    public string BodyContent { get; set; }

    [JsonPropertyName("certification")]
    public bool Certification { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationType Type { get; set; }
}





