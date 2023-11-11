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
    public class OrderQueryHandler :
        IRequestHandler<GetAllOrdersQuery, ApiResponse<List<OrderResponse>>>
        , IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;
       
        public OrderQueryHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }
        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Order> list = await dbContextClass.Set<Order>()
           .Include(x => x.Dealer)
           .Include(x => x.Payment)
           .Include(x=> x.Items)
           .ThenInclude(item => item.Product)
           .Where(x => x.IsActive)
           .ToListAsync(cancellationToken);

            List<OrderResponse> mapped = mapper.Map<List<OrderResponse>>(list);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }

        public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {

            Order? entity = await dbContextClass.Set<Order>()
           .Include(x => x.Dealer)
           .Include(x => x.Payment)
           .Include(x => x.Items)
           .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<OrderResponse>("Record not found!");
            }
           
            var destOrder = mapper.Map<Order, OrderResponse>(entity);
            if (destOrder.Items != null)
            {
                foreach (var item in destOrder.Items)
                {
                    if (item.Product != null)
                    {
                        item.Product = mapper.Map<ProductResponse>(item.Product);
                    }
                }
            }
            return new ApiResponse<OrderResponse>(destOrder);
        }
    }
}
