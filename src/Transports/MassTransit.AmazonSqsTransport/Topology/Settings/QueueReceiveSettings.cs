namespace MassTransit.AmazonSqsTransport.Topology.Settings
{
    using System;
    using Configuration;
    using Configurators;


    public class QueueReceiveSettings :
        QueueSubscriptionConfigurator,
        ReceiveSettings
    {
        readonly IAmazonSqsEndpointConfiguration _configuration;

        public QueueReceiveSettings(IAmazonSqsEndpointConfiguration configuration, string queueName, bool durable, bool autoDelete)
            : base(queueName, durable, autoDelete)
        {
            _configuration = configuration;

            WaitTimeSeconds = 3;

            if (AmazonSqsEndpointAddress.IsFifo(queueName))
                IsOrdered = true;
        }

        public int PrefetchCount => _configuration.Transport.PrefetchCount;
        public int ConcurrentMessageLimit => _configuration.Transport.GetConcurrentMessageLimit();

        public int WaitTimeSeconds { get; set; }

        public bool PurgeOnStartup { get; set; }

        public bool IsOrdered { get; set; }

        public Uri GetInputAddress(Uri hostAddress)
        {
            return GetEndpointAddress(hostAddress);
        }
    }
}
