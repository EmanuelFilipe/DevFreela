using DevFreela.Payments.API.Commands;

namespace DevFreela.Payments.API.Services
{
    public interface IPaymentService
    {
        Task<bool> Process(PaymentCommand paymentInfoCommand);
    }
}
