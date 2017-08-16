﻿namespace Microsoft.Bot.Builder.Extensions.Telemetry
{
    public class TelemetryReporterConfiguration
    {
        public bool FailSilently { get; set; }

        public TelemetryReporterConfiguration()
        {
            //by default, ensure that the telemetry reporting swallows all exceptions
            // to ensure that mis-configured telemetry doesn't crash the system
            // (can be set to FALSE for e.g., debugging/troubleshooting any telemetry issues)
            FailSilently = true;
        }
    }
}