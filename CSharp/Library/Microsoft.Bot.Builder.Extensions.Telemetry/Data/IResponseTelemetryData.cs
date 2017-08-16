using System;

namespace Microsoft.Bot.Builder.Extensions.Telemetry.Data
{
    public interface IResponseTelemetryData : ICommonTelemetryData
    {
        string ResponseText { get; set; }
        string ResponseImageUrl { get; set; }
        string ResponseJson { get; set; }
        string ResponseType { get; set; }
    }
}