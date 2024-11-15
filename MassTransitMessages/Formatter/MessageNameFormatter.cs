using System;
using MassTransit;


namespace MassTransitMessages.Formatter;

public class MessageNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return $"Mensaje-{typeof(T).Name}";
    }

}
