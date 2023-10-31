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
using System.Text;
using System.Threading.Tasks;

namespace OperationProject.Query
{
    public class ProductQueryHandler
        : IRequestHandler<GetAllProductsQuery, ApiResponse<List<ProductResponse>>>,
        IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public ProductQueryHandler(DbContextClass dbContextClass, IMapper mapper)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapper;
        }
        public Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> list = dbContextClass.Set<Product>().ToList();
            var mappedList = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mappedList);
        }
    }
}
