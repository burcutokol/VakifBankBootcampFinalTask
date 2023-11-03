using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using SchemaProject;

namespace OperationProject.Query
{ //eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJSb2xlIjoiRGVhbGVyIiwiRW1haWwiOiJkZWFsZXIxQGdtYWlsLmNvbSIsIlVzZXJOYW1lIjoiRGVhbGVyMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkRlYWxlciIsImV4cCI6MTY5OTA0NjY1NCwiaXNzIjoiRmluYWxBcGkiLCJhdWQiOiJGaW5hbEFwaSJ9.c2yftvXLytHtiq872juaxu_E1YGNr1Yh8XS4gFHLgPc
    public class OrderQueryHandler :
        IRequestHandler<GetAllOrdersQuery, ApiResponse<List<OrderResponse>>>
        , IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public OrderQueryHandler(DbContextClass dbContextClass, IMapper mapper)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Order> list = await dbContextClass.Set<Order>()
           .Include(x => x.Dealer)
           .Include(x => x.Payment)
           .Include(x=> x.Items)
           .ThenInclude(item => item.Product)
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
                .FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<OrderResponse>("Record not found!");
            }

            OrderResponse mapped = mapper.Map<OrderResponse>(entity);
            return new ApiResponse<OrderResponse>(mapped);
        }
    }
}
