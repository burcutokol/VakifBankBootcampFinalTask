using BaseProject.Response;
using MediatR;
using SchemaProject;

namespace OperationProject.Cqrs
{
    public record CreateEFTPaymentCommand(PaymentRequest model, int OrderId) : IRequest<ApiResponse<PaymentResponse>>;
    public record CreateCreditCardPaymentCommand(CreditCard model, int OrderId) : IRequest<ApiResponse<PaymentResponse>>;
    public record CreateCreditLimitPaymentCommand(PaymentRequest model, int OrderId, int UserId) : IRequest<ApiResponse<PaymentResponse>>;
    public record UpdatePaymentCommand(int PaymentId) : IRequest<ApiResponse>;
    public record DeletePaymentCommand(int PaymentId) : IRequest<ApiResponse>;

    public record GetPaymentByIdQuery(int PaymentId) : IRequest<ApiResponse<PaymentResponse>>;
    public record GetDealersPaymentByIdQuery(int DealerId) : IRequest<ApiResponse<List<PaymentResponse>>>;
}
