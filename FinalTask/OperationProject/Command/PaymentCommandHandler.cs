using AutoMapper;
using BaseProject.Response;
using Braintree;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using OperationProject.Mapper;
using SchemaProject;

namespace OperationProject.Command
{
    public class PaymentCommandHandler :
        IRequestHandler<CreateEFTPaymentCommand, ApiResponse<PaymentResponse>>,
         IRequestHandler<CreateCreditCardPaymentCommand, ApiResponse<PaymentResponse>>,
         IRequestHandler<CreateCreditLimitPaymentCommand, ApiResponse<PaymentResponse>>
        , IRequestHandler<DeletePaymentCommand, ApiResponse>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public PaymentCommandHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }

        public async Task<ApiResponse<PaymentResponse>> Handle(CreateEFTPaymentCommand request, CancellationToken cancellationToken)
        {
            Order? order = await dbContextClass.Set<Order>()
           .Include(x => x.Payment)
           .Include(order => order.Dealer)
           .ThenInclude(dealer => dealer.User)
           .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);

            Payment? payment = await dbContextClass.Set<Payment>()
           .Include(x => x.Order)
           .ThenInclude(order => order.Dealer)
           .ThenInclude(dealer => dealer.User)
           .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);
            if (order == null)
            {
                return new ApiResponse<PaymentResponse>("Record not found!");
            }
            if(payment != null)
            {
                return new ApiResponse<PaymentResponse>("There is a payment for this order.");
            }
            Payment newPayment = new Payment();

            newPayment.OrderId = request.OrderId;
            newPayment.PaymentType = request.model.PaymentType;
            newPayment.PaidAmount = order.TotalAmount;
            newPayment.PaymentStatus = "Pending approval";
            newPayment.PaymentId = newPayment.Id;

            order.PaymentId = newPayment.PaymentId;
            newPayment.PaidAmount = order.TotalAmount;
            order.Payment = newPayment;

            await dbContextClass.SaveChangesAsync(cancellationToken);
            PaymentResponse mappedPayment = mapper.Map<PaymentResponse>(newPayment);
            return new ApiResponse<PaymentResponse>(mappedPayment);
        }
        public async Task<ApiResponse<PaymentResponse>> Handle(CreateCreditCardPaymentCommand request, CancellationToken cancellationToken)
        {
            //TODO Card Model Validation
            Order? order = await dbContextClass.Set<Order>()
          .Include(x => x.Payment)
          .Include(order => order.Dealer)
          .ThenInclude(dealer => dealer.User)
          .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);

            Payment? payment = await dbContextClass.Set<Payment>()
           .Include(x => x.Order)
           .ThenInclude(order => order.Dealer)
           .ThenInclude(dealer => dealer.User)
           .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);
            if (order == null)
            {
                return new ApiResponse<PaymentResponse>("Record not found!");
            }
            if (payment != null)
            {
                return new ApiResponse<PaymentResponse>("There is a payment for this order.");
            }
            Payment newPayment = new Payment();

            newPayment.OrderId = request.OrderId;
            newPayment.PaymentType = request.model.PaymentType;
            newPayment.PaidAmount = order.TotalAmount;
            newPayment.PaymentStatus = "Pending approval";
            newPayment.PaymentId = newPayment.Id;

            order.PaymentId = newPayment.PaymentId;
            newPayment.PaidAmount = order.TotalAmount;
            order.Payment = newPayment;

            await dbContextClass.SaveChangesAsync(cancellationToken);
            PaymentResponse mappedPayment = mapper.Map<PaymentResponse>(newPayment);
            return new ApiResponse<PaymentResponse>(mappedPayment);
        }

        public async Task<ApiResponse<PaymentResponse>> Handle(CreateCreditLimitPaymentCommand request, CancellationToken cancellationToken)
        {
            Order? order = await dbContextClass.Set<Order>()
          .Include(x => x.Payment)
          .Include(order => order.Dealer)
          .ThenInclude(dealer => dealer.User)
          .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);

            Payment? payment = await dbContextClass.Set<Payment>()
           .Include(x => x.Order)
           .ThenInclude(order => order.Dealer)
           .ThenInclude(dealer => dealer.User)
           .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);

            Dealer? dealer = await dbContextClass.Set<Dealer>().
                FirstOrDefaultAsync(x => x.UserLoginId == request.UserId, cancellationToken);
            Payment newPayment = new Payment();
            if (order == null)
            {
                return new ApiResponse<PaymentResponse>("Record not found!");
            }
            if (payment != null)
            {
                return new ApiResponse<PaymentResponse>("There is a payment for this order.");
            }
            if(order.TotalAmount > dealer.Limit )
            {
                newPayment.PaymentStatus = "Insufficient limit, order canceled";
                order.Status = "Cancelled.";
            }

            newPayment.OrderId = request.OrderId;
            newPayment.PaymentType = request.model.PaymentType;
            newPayment.PaidAmount = order.TotalAmount;
            newPayment.PaymentStatus = "Pending approval";
            newPayment.PaymentId = newPayment.Id;

            order.PaymentId = newPayment.PaymentId;
            newPayment.PaidAmount = order.TotalAmount;
            order.Payment = newPayment;

            var entity = await dbContextClass.Set<Payment>().AddAsync(newPayment, cancellationToken);
            await dbContextClass.SaveChangesAsync(cancellationToken);
            PaymentResponse mappedPayment = mapper.Map<PaymentResponse>(newPayment);
            return new ApiResponse<PaymentResponse>(mappedPayment);
        }
        public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContextClass.Set<Payment>().FirstOrDefaultAsync(x => x.Id == request.PaymentId, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            var order = await dbContextClass.Set<Order>().FirstOrDefaultAsync(x => x.Id == entity.OrderId, cancellationToken);
            if (order != null)
            {
                return new ApiResponse("You must delete the order first!");
            }
            entity.IsActive = false;
            await dbContextClass.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

       
    }
}
