using BaseProject.Response;
using MediatR;
using SchemaProject;

namespace OperationProject.Cqrs
{
    public record CreateOrderCommand(OrderRequest model) : IRequest<ApiResponse<OrderResponse>>;
    public record UpdateOrderCommand(OrderRequest model, int id) : IRequest<ApiResponse>;
    public record DeleteOrderCommand(int id) : IRequest<ApiResponse>;


    public record GetAllOrdersQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
    public record GetOrderByIdQuery(int id) : IRequest<ApiResponse<OrderResponse>>;
}
