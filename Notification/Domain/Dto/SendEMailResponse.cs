﻿using System.Text.Json.Serialization;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Domain.Dto;

public class SendEMailResponse
{
    [JsonPropertyName("responseText")]
    public string ResponseText { get; set; }

    [JsonPropertyName("statusCode")]
    public long StatusCode { get; set; }

    [JsonPropertyName("recipients")]
    public List<Recipient> Recipients { get; set; }
}
