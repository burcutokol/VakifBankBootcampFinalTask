using BaseProject.Response;
using MediatR;
using SchemaProject;

namespace OperationProject.Cqrs
{
    public record CreateMessageCommand(MessageRequest model, int userId) : IRequest<ApiResponse<MessageResponse>>;
    public record UpdateMessageCommand(MessageRequest model, int id) : IRequest<ApiResponse>;
    public record DeleteMessageCommand(int id) : IRequest<ApiResponse>;


    public record GetAllMessagesQuery() : IRequest<ApiResponse<List<MessageResponse>>>;
    public record GetMessageByIdQuery(int id) : IRequest<ApiResponse<MessageResponse>>;
}
