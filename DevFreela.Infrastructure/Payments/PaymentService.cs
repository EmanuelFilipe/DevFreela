using DevFreela.Core.DTOs;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Services;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";

        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
           
        }

        //modo antigo para comunicação com microsserviço
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly string _paymentBaseUrl;

        //public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _paymentBaseUrl = configuration.GetSection("Services:Payments").Value;
        //}

        //[Obsolete(message: "codigo antigo para comunicação com microsserviço, será substituido por RabbitMQ")]
        //public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        //{
        //    var url = $"{_paymentBaseUrl}/api/payments";
        //    var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

        //    var paymentInfoContent = new StringContent(
        //        paymentInfoJson, Encoding.UTF8, "application/json");

        //    var httpClient = _httpClientFactory.CreateClient("Payments");

        //    var response = httpClient.PostAsync(url, paymentInfoContent);

        //    //return response.IsSuccessStatusCode;
        //}
    }
}
