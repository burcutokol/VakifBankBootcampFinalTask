using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using OperationProject.Mapper;

namespace OperationProject.Command
{
    public class ProductCommandHandler : 
        IRequestHandler<UpdateProductCommand, ApiResponse>
        , IRequestHandler<UpdateProductStockByOrderCommand, ApiResponse>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public ProductCommandHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }

        public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContextClass.Set<Product>().FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            entity.StockAmount = request.StockNumber;
            await dbContextClass.SaveChangesAsync(cancellationToken);

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(UpdateProductStockByOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await dbContextClass.Set<Order>().
                Include(x => x.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                return new ApiResponse("Order not found!");
            }
            foreach(var orderItem in order.Items)
            {
                if(request.Status) //order approved
                    orderItem.Product.StockAmount -= orderItem.Count;
                if(!request.Status) //order cancelled
                    orderItem.Product.StockAmount += orderItem.Count;
            }
            
            await dbContextClass.SaveChangesAsync(cancellationToken);

            return new ApiResponse();
        }
    }
}
