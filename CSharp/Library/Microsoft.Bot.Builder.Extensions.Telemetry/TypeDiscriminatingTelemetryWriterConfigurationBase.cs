﻿namespace Microsoft.Bot.Builder.Extensions.Telemetry
{
    public abstract class TypeDiscriminatingTelemetryWriterConfigurationBase
    {
        public TelemetryTypes TelemetryTypesToHandle { get; set; }

        protected TypeDiscriminatingTelemetryWriterConfigurationBase()
        {
            TelemetryTypesToHandle = TelemetryTypes.All;
        }

        public bool Handles(TelemetryTypes telemetryType)
        {
            return TelemetryTypesToHandle.HasFlag(telemetryType);
        }
    }
}