﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Telemetry;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Microsoft.Bot.Builder.Azure.Telemetry.BlobStorageWriter
{
    public class BlobStorageTelemetryWriter : ITelemetryWriter
    {
        private readonly ITelemetryOutputFormatter _formatter;
        private CloudAppendBlob _blob;
        private readonly BlobStorageTelemetryWriterConfiguration _configuration;

        public BlobStorageTelemetryWriter(BlobStorageTelemetryWriterConfiguration configuration, ITelemetryOutputFormatter formatter)
        {
            SetField.NotNull(out _configuration, nameof(configuration), configuration);
            SetField.NotNull(out _formatter, nameof(formatter), formatter);

            Initialize();
        }

        private void Initialize()
        {
            //set initial configuration
            _blob = GetAppendBlob();
        }

        private CloudAppendBlob GetAppendBlob()
        {
            //Parse the connection string for the storage account.
            var storageAccount = CloudStorageAccount.Parse(_configuration.StorageConnectionString);

            //Create service client for credentialed access to the Blob service.
            var blobClient = storageAccount.CreateCloudBlobClient();

            //Get a reference to a container.
            var container = blobClient.GetContainerReference(_configuration.BlobStorageContainerName);

            //Create the container if it does not already exist.
            container.CreateIfNotExists();

            //Get a reference to an append blob.
            var appendBlob = container.GetAppendBlobReference(_configuration.BlobStorageBlobName);

            //Create the append blob if not exists
            if (!appendBlob.Exists())
            {
                appendBlob.CreateOrReplace();
            }

            return appendBlob;
        }

        private void DoPostLogActions()
        {
            //no-op; left to ease future use if needed
        }

        public async Task WriteIntentAsync(IIntentTelemetry intentTelemetry)
        {
            if (_configuration.Handles(TelemetryTypes.Intents))
            {
                await Task.Run(async () =>
                {
                    //process each entity if we have any
                    if (null != intentTelemetry.IntentEntities)
                    {
                        foreach (var entity in intentTelemetry.IntentEntities)
                        {
                            await WriteEntityAsync(new EntityTelemetry(entity.Key, entity.Value));
                        }
                    }

                    //now process the intent
                    await AppendToBlob(_formatter.FormatIntent(intentTelemetry));

                    DoPostLogActions();
                });
            }
        }

        private async Task AppendToBlob(string record)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(record)))
            {
                await _blob.AppendFromStreamAsync(stream);
            }
        }

        public async Task WriteEntityAsync(IEntityTelemetry entityTelemetry)
        {
            if (_configuration.Handles(TelemetryTypes.Entities))
            {
                await Task.Run(async () =>
                {
                    await AppendToBlob(_formatter.FormatEntity(entityTelemetry));
                    DoPostLogActions();

                });
            }
        }

        public async Task WriteResponseAsync(IResponseTelemetry responseTelemetry)
        {
            if (_configuration.Handles(TelemetryTypes.Responses))
            {
                await Task.Run(async () =>
                {
                    await AppendToBlob(_formatter.FormatResponse(responseTelemetry));
                    DoPostLogActions();

                });
            }
        }

        public async Task WriteServiceResultAsync(IServiceResultTelemetry serviceResultTelemetry)
        {
            if (_configuration.Handles(TelemetryTypes.ServiceResults))
            {
                await Task.Run(async () =>
                {
                    await AppendToBlob(_formatter.FormatServiceResult(serviceResultTelemetry));
                    DoPostLogActions();
                });
            }

        }

        public async Task WriteCounterAsync(ICounterTelemetry counterTelemetry)
        {
            if (_configuration.Handles(TelemetryTypes.Counters))
            {
                await Task.Run(async () =>
                {
                    await AppendToBlob(_formatter.FormatCounter(counterTelemetry));
                    DoPostLogActions();
                });
            }
        }

        public async Task WriteExceptionAsync(IExceptionTelemetry exceptionTelemetry)
        {
            if (_configuration.Handles(TelemetryTypes.Exceptions))
            {
                await Task.Run(async () =>
                {
                    await AppendToBlob(_formatter.FormatException(exceptionTelemetry));
                    DoPostLogActions();
                });
            }
        }

        public void SetContext(ITelemetryContext context)
        {
            _formatter.SetContext(context);
        }

        public async Task WriteEventAsync(string key, string value)
        {
            await WriteEventAsync(new Dictionary<string, string> { { key, value } });
        }

        public async Task WriteEventAsync(string key, double value)
        {
            await WriteEventAsync(new Dictionary<string, double> { { key, value } });
        }

        public async Task WriteEventAsync(Dictionary<string, double> metrics)
        {
            await WriteEventAsync(new Dictionary<string, string>(), metrics);
        }

        public async Task WriteEventAsync(Dictionary<string, string> eventProperties, Dictionary<string, double> eventMetrics = null)
        {
            if (_configuration.Handles(TelemetryTypes.CustomEvents))
            {
                await Task.Run(async () =>
                {
                    await AppendToBlob(_formatter.FormatEvent(eventProperties, eventMetrics));
                    DoPostLogActions();
                });
            }
        }
    }
}