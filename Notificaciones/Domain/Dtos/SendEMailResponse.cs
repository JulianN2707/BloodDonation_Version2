namespace MVCT.Transversales.Services.Notificaciones.Domain.Dtos;
using System.Text.Json.Serialization;

public  class SendEMailResponse
{
    [JsonPropertyName("responseText")]
    public string ResponseText { get; set; }

    [JsonPropertyName("statusCode")]
    public long StatusCode { get; set; }

    [JsonPropertyName("recipients")]
    public List<Recipient> Recipients { get; set; }
}

