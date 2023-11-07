using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using OperationProject.Mapper;
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

        public ProductQueryHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }
        public double CalculateProductPrice(double profitMargin, double price)
        {
            return price + (price*(profitMargin/100));
        }
        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? entity = await dbContextClass.Set<Product>().FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse<ProductResponse>("Record not found!");
            }
            if(request.UserId == 0) //Admin
            {
                var mappedProduct = mapper.Map<ProductResponse>(entity);
                return new ApiResponse<ProductResponse>(mappedProduct);
            }
            Dealer dealer = await dbContextClass.Set<Dealer>().FirstOrDefaultAsync(x => x.UserLoginId == request.UserId, cancellationToken);
            ProductResponse productResponse = mapper.Map<ProductResponse>(entity);
            productResponse.Price = CalculateProductPrice(dealer.ProfitMargin, entity.Price);
            return new ApiResponse<ProductResponse>(productResponse);
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> list = dbContextClass.Set<Product>().ToList();
            var mappedList = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mappedList);
        }
    }
}
