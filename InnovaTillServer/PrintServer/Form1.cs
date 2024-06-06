using MQTTnet.Server;
using MQTTnet;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace PrintServer
{
    public partial class Form1 : Form
    {
        private MqttServer? mqttServer;
        public Form1()
        {
            this.InitializeComponent();
        }



        private void chbSocket_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbMqtt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void btnServerStart_Click(object sender, EventArgs e)
        {
            await Run_Server_With_Self_Signed_Certificate();
        }

        public async Task Run_Server_With_Self_Signed_Certificate()
        {
            // This certificate is self signed so that
            var certificate = CreateSelfSignedCertificate("1.3.6.1.5.5.7.3.1");

            var mqttServerOptions = new MqttServerOptionsBuilder().WithEncryptionCertificate(certificate).WithEncryptedEndpoint().Build();

            this.mqttServer = new MqttFactory().CreateMqttServer(mqttServerOptions);
            {
                if (!this.mqttServer.IsStarted)
                {
                    await mqttServer.StartAsync();

                    MessageBox.Show("Server started");
                }
            }
        }

        static X509Certificate2 CreateSelfSignedCertificate(string oid)
        {
            var sanBuilder = new SubjectAlternativeNameBuilder();
            sanBuilder.AddIpAddress(IPAddress.Loopback);
            sanBuilder.AddIpAddress(IPAddress.IPv6Loopback);
            sanBuilder.AddDnsName("localhost");

            using (var rsa = RSA.Create())
            {
                var certRequest = new CertificateRequest("CN=localhost", rsa, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);

                certRequest.CertificateExtensions.Add(
                    new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));

                certRequest.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new(oid) }, false));

                certRequest.CertificateExtensions.Add(sanBuilder.Build());

                using (var certificate = certRequest.CreateSelfSigned(DateTimeOffset.Now.AddMinutes(-10), DateTimeOffset.Now.AddMinutes(10)))
                {
                    var pfxCertificate = new X509Certificate2(
                        certificate.Export(X509ContentType.Pfx),
                        (string)null!,
                        X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

                    return pfxCertificate;
                }
            }
        }
        private  void btnSend_Click(object sender, EventArgs e)
        {
           Publish_Message_From_Broker();
        }
        public async void Publish_Message_From_Broker()
        {


            // Create a new message using the builder as usual.
            var message = new MqttApplicationMessageBuilder().WithTopic("Topic").WithPayload("Test").Build();

            // Now inject the new message at the broker.
            await this.mqttServer.InjectApplicationMessage(
                new InjectedMqttApplicationMessage(message)
                {
                    SenderClientId = "SenderClientId"
                });

        }
        private async void btnServerStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.mqttServer.IsStarted)
                {
                    await this.mqttServer.StopAsync();
                    MessageBox.Show("Server Stoped");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private async void btnClientStart_Click(object sender, EventArgs e)
        {
            await Connect_Client();
        }
        public static async Task Connect_Client()
        {
            /*
             * This sample creates a simple managed MQTT client and connects to a public broker, subscribe to a topic and verifies subscription result.
             *
             * The managed client extends the existing _MqttClient_. It adds the following features.
             * - Reconnecting when connection is lost.
             * - Storing pending messages in an internal queue so that an enqueue is possible while the client remains not connected.
             */

            var mqttFactory = new MqttFactory();
            var subscribed = false;

            using (var managedMqttClient = mqttFactory.CreateManagedMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("localhost").WithClientId("SenderClientId")
                    .Build();

                var managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
                    .WithClientOptions(mqttClientOptions)
                    .Build();

                await managedMqttClient.StartAsync(managedMqttClientOptions);

                // The application message is not sent. It is stored in an internal queue and
                // will be sent when the client is connected.
                await managedMqttClient.EnqueueAsync("Topic", "Payload");

                Console.WriteLine("The managed MQTT client is connected.");

                // Wait until the queue is fully processed.
                SpinWait.SpinUntil(() => managedMqttClient.PendingApplicationMessagesCount == 0, 10000);

                Console.WriteLine($"Pending messages = {managedMqttClient.PendingApplicationMessagesCount}");

                managedMqttClient.SubscriptionsChangedAsync += args => SubscriptionsResultAsync(args, subscribed);
                await managedMqttClient.SubscribeAsync("Topic").ConfigureAwait(false);

                SpinWait.SpinUntil(() => subscribed, 1000);
                Console.WriteLine("Subscription properly done");
            }
        }

        private static Task SubscriptionsResultAsync(SubscriptionsChangedEventArgs arg, bool subscribed)
        {
            foreach (var mqttClientSubscribeResult in arg.SubscribeResult)
            {
                Console.WriteLine($"Subscription reason {mqttClientSubscribeResult.ReasonString}");
                foreach (var item in mqttClientSubscribeResult.Items)
                {
                    Console.WriteLine($"For topic filter {item.TopicFilter}, result code: {item.ResultCode}");

                    if (item.TopicFilter.Topic == "Topic" && item.ResultCode == MqttClientSubscribeResultCode.GrantedQoS0 && !subscribed)
                    {
                        subscribed = true;
                    }
                }
            }

            foreach (var mqttClientUnsubscribeResult in arg.UnsubscribeResult)
            {
                Console.WriteLine($"Unsubscription reason {mqttClientUnsubscribeResult.ReasonString}");
                foreach (var item in mqttClientUnsubscribeResult.Items)
                {
                    Console.WriteLine($"For topic filter {item.TopicFilter}, result code: {item.ResultCode}");
                }
            }

            return Task.CompletedTask;
        }

     
    }
}
