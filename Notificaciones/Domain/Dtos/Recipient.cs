using System.Text.Json.Serialization;
namespace MVCT.Transversales.Services.Notificaciones.Domain.Dtos;
public partial class Recipient
{
    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("messageId")]
    public Guid MessageId { get; set; }
}

