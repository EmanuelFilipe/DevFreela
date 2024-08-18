using DevFreela.Payments.API.Services;
using MediatR;

namespace DevFreela.Payments.API.Commands
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand, bool>
    {
        private readonly IPaymentService _paymentService;

        public PaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            var result = await _paymentService.Process(request);
            
            return result;
        }
    }
}
