using BaseProject.Response;
using MediatR;
using SchemaProject;

namespace OperationProject.Cqrs
{
    public record CreateOrderCommand(OrderRequest model, int DealerId) : IRequest<ApiResponse<OrderResponse>>;
    public record UpdateOrderCommand(int OrderId, int DealerId) : IRequest<ApiResponse>;
    public record DeleteOrderCommand(int id) : IRequest<ApiResponse>;
    public record ApproveOrder(int id) : IRequest<ApiResponse>;
    public record RejectOrder(int id) : IRequest<ApiResponse>;


    public record GetAllOrdersQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
    public record GetOrderByIdQuery(int id) : IRequest<ApiResponse<OrderResponse>>;
}
