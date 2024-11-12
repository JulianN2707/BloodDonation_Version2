using System;
using System.Text.Json.Serialization;

namespace Solicitudes.Domain.Dto;

public partial class Recipient
{
    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("messageId")]
    public Guid MessageId { get; set; }
}
