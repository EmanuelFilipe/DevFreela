using DevFreela.Payments.API.Commands;

namespace DevFreela.Payments.API.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> Process(PaymentCommand paymentInfoCommand)
        {
            return Task.FromResult(true);
        }
    }
}
