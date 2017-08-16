using System;

namespace Microsoft.Bot.Builder.Extensions.Telemetry.Data
{
    public interface ICommonTelemetryData
    {
        string RecordType { get; set; }
        DateTime Timestamp { get; set; }
        string CorrelationId { get; set; }
        string ChannelId { get; set; }
        string ConversationId { get; set; }
        string ActivityId { get; set; }
        string UserId { get; set; }
        string Json { get; set; }
        string AsStringWith(ITelemetryContext context);
    }
}