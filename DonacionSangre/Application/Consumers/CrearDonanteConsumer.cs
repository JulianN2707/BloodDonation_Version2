using System;
using MassTransit;
using MassTransitMessages.Messages;

namespace DonacionSangre.Application.Consumers;

public class CrearDonanteConsumer : IConsumer<EnviarCreacionDonanteMessage>
{
    public async Task Consume(ConsumeContext<EnviarCreacionDonanteMessage> context)
    {
        var data = context.Message;
        if (data is not null)
        {
            DonanteCreadoEvent evento = new DonanteCreadoEvent
            {
                CorrelationId = data.CorrelationId,
                DonanteId = NewId.NextSequentialGuid(),
                SolicitudUsuarioId = data.SolicitudUsuarioId,

            };
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:saga-aprobar-donante"));
            await endpoint.Send(evento);

        }

    }
}
