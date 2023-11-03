using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using SchemaProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OperationProject.Command
{
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>
        , IRequestHandler<UpdateOrderCommand, ApiResponse>
        , IRequestHandler<DeleteOrderCommand, ApiResponse>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public OrderCommandHandler(DbContextClass dbContextClass, IMapper mapper)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapper;
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
            var entity = await dbContextClass.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            //TODO updates
            //TODO stock control func
            mapper.Map<OrderResponse>(entity);

            await dbContextClass.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

