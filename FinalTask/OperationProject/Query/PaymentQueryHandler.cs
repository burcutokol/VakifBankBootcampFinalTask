using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using OperationProject.Mapper;
using SchemaProject;

namespace OperationProject.Query
{
    public class PaymentQueryHandler :
        IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>
        , IRequestHandler<GetDealersPaymentByIdQuery, ApiResponse<List<PaymentResponse>>>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public PaymentQueryHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }
        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetDealersPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            List<Payment> list = await dbContextClass.Set<Payment>()
            .Include(x => x.Order)
            .ThenInclude(order => order.Dealer)
            .ThenInclude(dealer => dealer.User)
            .Where(x => x.Order.Dealer.UserLoginId == request.DealerId && x.IsActive)
            .ToListAsync();
            if (list == null)
            {
                return new ApiResponse<List<PaymentResponse>>("Record not found!");
            }
            List<PaymentResponse> mappedList = mapper.Map<List<PaymentResponse>>(list);
            return new ApiResponse<List<PaymentResponse>>(mappedList);
            
        }

        public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            Payment? payment = await dbContextClass.Set<Payment>()
            .Include(x => x.Order)
            .ThenInclude(order => order.Dealer)
            .ThenInclude(dealer => dealer.User)
            .FirstOrDefaultAsync(x => x.PaymentId == request.PaymentId && x.IsActive, cancellationToken);
            if (payment == null)
            {
                return new ApiResponse<PaymentResponse>("Record not found!");
            }
            PaymentResponse mappedPayment = mapper.Map<PaymentResponse>(payment);
            return new ApiResponse<PaymentResponse>(mappedPayment);
        }

    }
}
