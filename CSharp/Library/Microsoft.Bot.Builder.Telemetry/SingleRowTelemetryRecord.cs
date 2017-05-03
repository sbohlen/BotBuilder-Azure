﻿using System;
using System.Text;

namespace Microsoft.Bot.Builder.Telemetry
{
    public class SingleRowTelemetryRecord
    {
        public string RecordType { get; set; }
        public string Timestamp { get; set; }
        public string CorrelationId { get; set; }
        public string ChannelId { get; set; }
        public string ConversationId { get; set; }
        public string ActivityId { get; set; }
        public string UserId { get; set; }
        public string IntentName { get; set; }
        public string IntentText { get; set; }
        public string IntentScore { get; set; }
        public string EntityType { get; set; }
        public string EntityValue { get; set; }
        public string ResponseText { get; set; }
        public string ResponseImageUrl { get; set; }
        public string ResponseJson { get; set; }
        public string ResponseResult { get; set; }
        public string ResponseDuration { get; set; }
        public string ResponseCacheHit { get; set; }
        public string CounterName { get; set; }
        public string CounterValue { get; set; }
        public string ServiceResultName { get; set; }
        public string ServiceResultMilliseconds { get; set; }
        public string ServiceResultSuccess { get; set; }
        public string ServiceResultResponse { get; set; }
        public string TraceName { get; set; }
        public string TraceValue { get; set; }
        public string ExceptionComponent { get; set; }
        public string ExceptionContext { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionDetail { get; set; }

        public string AsStringWith(ITelemetryContext context)
        {
            var sb = new StringBuilder();

            sb.Append($"{RecordType}");

            sb.Append($"\t{context.Timestamp}");
            sb.Append($"\t{context.CorrelationId}");
            sb.Append($"\t{context.ChannelId}");
            sb.Append($"\t{context.ConversationId}");
            sb.Append($"\t{context.ActivityId}");
            sb.Append($"\t{context.UserId}");

            sb.Append($"\t{IntentName}");
            sb.Append($"\t{IntentText}");
            sb.Append($"\t{IntentScore}");

            sb.Append($"\t{EntityType}");
            sb.Append($"\t{EntityValue}");

            sb.Append($"\t{ResponseText}");
            sb.Append($"\t{ResponseImageUrl}");
            sb.Append($"\t{ResponseJson}");
            sb.Append($"\t{ResponseResult}");
            sb.Append($"\t{ResponseDuration}");
            sb.Append($"\t{ResponseCacheHit}");


            sb.Append($"\t{CounterName}");
            sb.Append($"\t{CounterValue}");

            sb.Append($"\t{ServiceResultName}");
            sb.Append($"\t{ServiceResultMilliseconds}");
            sb.Append($"\t{ServiceResultSuccess}");
            sb.Append($"\t{ServiceResultResponse}");

            sb.Append($"\t{TraceName}");
            sb.Append($"\t{TraceValue}");

            sb.Append($"\t{ExceptionComponent}");
            sb.Append($"\t{ExceptionContext}");
            sb.Append($"\t{ExceptionType}");
            sb.Append($"\t{ExceptionMessage}");
            sb.Append($"\t{ExceptionDetail}");

            sb.Append($"{Environment.NewLine}");

            return sb.ToString();
        }

    }
}