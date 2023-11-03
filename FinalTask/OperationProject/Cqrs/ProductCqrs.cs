using BaseProject.Response;
using MediatR;
using SchemaProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationProject.Cqrs
{
    public record CreateProductCommand(ProductRequest model) : IRequest<ApiResponse<ProductResponse>>;
    public record UpdateProductCommand(ProductRequest model, int id) : IRequest<ApiResponse>;
    public record DeleteCommand(int id) : IRequest<ApiResponse>;


    public record GetAllProductsQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
    public record GetProductByIdQuery(int id) : IRequest<ApiResponse<ProductResponse>>;
}
