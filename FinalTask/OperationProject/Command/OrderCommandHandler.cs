using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using OperationProject.Mapper;
using SchemaProject;

namespace OperationProject.Command
{
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>
        , IRequestHandler<UpdateOrderCommand, ApiResponse>
        , IRequestHandler<DeleteOrderCommand, ApiResponse>
        , IRequestHandler<ApproveOrder, ApiResponse>
        , IRequestHandler<RejectOrder, ApiResponse>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public OrderCommandHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }
        public bool CheckStock(OrderItemRequest model, int count)
        {
            var stock = dbContextClass.Set<Product>().FirstOrDefault(x => x.ProductId == model.ProductId);
            if (stock != null && stock.StockAmount >= count)
                return true;
            return false;
        }
        public double CalculateTotalAmount(List<OrderItemRequest> list)
        {
            var totalAmount = 0.0;
            foreach (OrderItemRequest request in list)
            {
                var item = dbContextClass.Set<Product>().FirstOrDefault(x => x.ProductId == request.ProductId);
                totalAmount += item.Price * request.Count;
            }
            return totalAmount;

        }
        public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContextClass.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }

            entity.IsActive = false;
            await dbContextClass.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }


        public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContextClass.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            entity.Status = "Cancelled"; //TODO fatura oluştuysa iptal edilemez.
            await dbContextClass.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            foreach(OrderItemRequest item in request.model.Items)
            {
                bool hasEnoughStock = CheckStock(item, item.Count);

                if (!hasEnoughStock)
                {
                    return new ApiResponse<OrderResponse>("Insufficient stock");
                }

            }
            Payment newPayment = mapper.Map<PaymentRequest, Payment>(request.model.Payment);
           

            Order newOrder = new Order();
            newOrder = mapper.Map<Order>(request.model);
            newOrder.Dealer = await dbContextClass.Set<Dealer>().FindAsync(request.DealerId);
            newOrder.DealerId = request.DealerId;
            newOrder.OrderId = newOrder.Id;
            newOrder.Status = "Pending approval";
            newPayment.PaymentStatus = "Pending approval";
            newPayment.PaymentId = newPayment.Id;
            newOrder.PaymentId = newPayment.PaymentId;
            newOrder.TotalAmount = CalculateTotalAmount(request.model.Items); //TODO Kar marjı ekle
            newPayment.PaidAmount = newOrder.TotalAmount;
            newOrder.Payment = newPayment;


            dbContextClass.Set<Payment>().AddAsync(newPayment);
            dbContextClass.Set<Order>().AddAsync(newOrder);
            dbContextClass.SaveChangesAsync(cancellationToken);

            return new ApiResponse<OrderResponse>(new OrderResponse());
        }

        public async Task<ApiResponse> Handle(ApproveOrder request, CancellationToken cancellationToken)
        {
            var order = await dbContextClass.Set<Order>().FirstOrDefaultAsync(o => o.Id == request.id);
            if(order == null) 
            {
                return new ApiResponse("Order not found.");
            }
            order.Status = "Approved.";
            await dbContextClass.SaveChangesAsync(cancellationToken);

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(RejectOrder request, CancellationToken cancellationToken)
        {
            var order = await dbContextClass.Set<Order>().FirstOrDefaultAsync(o => o.Id == request.id);
            if (order == null)
            {
                return new ApiResponse("Order not found.");
            }
            order.Status = "Rejected.";
            await dbContextClass.SaveChangesAsync(cancellationToken);

            return new ApiResponse();
        }
    }
}

