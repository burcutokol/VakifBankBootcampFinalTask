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
    public record UpdateProductCommand(int ProductId, int StockNumber) : IRequest<ApiResponse>;
    public record UpdateProductStockByOrderCommand(int OrderId, bool Status)  : IRequest<ApiResponse>;
    public record DeleteCommand(int id) : IRequest<ApiResponse>;



    public record GetAllProductsQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
    public record GetProductByIdQuery(int ProductId, int UserId) : IRequest<ApiResponse<ProductResponse>>;
}
