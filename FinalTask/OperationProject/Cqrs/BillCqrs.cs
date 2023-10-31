using BaseProject.Response;
using MediatR;
using SchemaProject;

namespace OperationProject.Cqrs
{
      public record CreateBillCommand(BillRequest model) : IRequest<ApiResponse<BillResponse>>;
      public record UpdateBillCommand(BillRequest model, int id) : IRequest<ApiResponse>;
      public record DeleteBillCommand(BillRequest model, int id) : IRequest<ApiResponse>;

      
      public record GetAllBillsQuery()  : IRequest<ApiResponse<List<BillResponse>>>;
      public record GetBillByIdQuery(int id) : IRequest<ApiResponse<BillResponse>>;

}
