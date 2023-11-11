using AutoMapper;
using BaseProject.Response;
using Braintree;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using OperationProject.Mapper;
using SchemaProject;

namespace OperationProject.Command
{
    public class MessageCommandHandler :
        IRequestHandler<CreateMessageCommand, ApiResponse<MessageResponse>>
        , IRequestHandler<UpdateMessageCommand, ApiResponse>
        , IRequestHandler<DeleteMessageCommand, ApiResponse>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public MessageCommandHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }

        public async Task<ApiResponse<MessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            Message newMessage = mapper.Map<Message>(request.model);
            newMessage.SenderId = request.userId;
            var user = await dbContextClass.Set<User>().FirstOrDefaultAsync(x => x.UserLoginId == request.userId, cancellationToken);
            if(user.UserLoginId != 0 && request.model.RecevierId != 0) //if sender is not admin, can not send message the other dealers
                return new ApiResponse<MessageResponse>("Dealers can only send messages to the company!");
            await dbContextClass.Set<Message>().AddAsync(newMessage, cancellationToken);
            await dbContextClass.SaveChangesAsync(cancellationToken);

            MessageResponse mappedMessage = mapper.Map<MessageResponse>(newMessage);
            mappedMessage.SenderUserName = user.UserName;
            return new ApiResponse<MessageResponse>(mappedMessage);
        }

        public async Task<ApiResponse> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContextClass.Set<Message>().FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            entity.MessageContent = request.model.MessageContent;
            entity.RecevierId = request.model.RecevierId;
            entity.UpdateDate = DateTime.UtcNow;
            await dbContextClass.SaveChangesAsync(cancellationToken);

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContextClass.Set<Message>().FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }

            entity.IsActive = false;
            await dbContextClass.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
