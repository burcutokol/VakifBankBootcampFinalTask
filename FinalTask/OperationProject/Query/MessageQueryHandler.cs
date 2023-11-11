using AutoMapper;
using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationProject.Cqrs;
using OperationProject.Mapper;
using SchemaProject;

namespace OperationProject.Query
{
    public class MessageQueryHandler :
        IRequestHandler<GetAllMessagesQuery, ApiResponse<List<MessageResponse>>>,
        IRequestHandler<GetMessageByIdQuery, ApiResponse<MessageResponse>>,
        IRequestHandler<GetDealersMessagesByIdQuery, ApiResponse<List<MessageResponse>>>
    {
        private readonly DbContextClass dbContextClass;
        private readonly IMapper mapper;

        public MessageQueryHandler(DbContextClass dbContextClass, MapperConfig mapperConfig)
        {
            this.dbContextClass = dbContextClass;
            this.mapper = mapperConfig.GetMapper();
        }
        public async Task<ApiResponse<List<MessageResponse>>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            List<Message> list = await dbContextClass.Set<Message>().Where(x => x.IsActive).ToListAsync(cancellationToken);
            
            List<MessageResponse> mapped = mapper.Map<List<MessageResponse>>(list);
            foreach(MessageResponse message in mapped)
            {
                message.SenderUserName = await dbContextClass.Set<Dealer>().Where(x => x.UserLoginId == message.SenderId && x.IsActive).Select(x => x.Name).FirstOrDefaultAsync();

            }
            return new ApiResponse<List<MessageResponse>>(mapped);
        }

        public async Task<ApiResponse<MessageResponse>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            Message? entity = await dbContextClass.Set<Message>().FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse<MessageResponse>("Record not found!");
            }

            MessageResponse messageResponse = mapper.Map<MessageResponse>(entity);
            messageResponse.SenderUserName  = await dbContextClass.Set<Dealer>().Where(x => x.UserLoginId == messageResponse.SenderId && x.IsActive).Select(x => x.Name).FirstOrDefaultAsync();
            return new ApiResponse<MessageResponse>(messageResponse);
        }

        public async Task<ApiResponse<List<MessageResponse>>> Handle(GetDealersMessagesByIdQuery request, CancellationToken cancellationToken)
        {
            List<Message> list = null;
            if (char.ToUpper(request.SenderOrReceiver) == 'R')
                list = await dbContextClass.Set<Message>().Where(x => x.RecevierId == request.DealerId && x.IsActive).ToListAsync();
            else if(char.ToUpper(request.SenderOrReceiver) == 'S')
                list = await dbContextClass.Set<Message>().Where(x => x.SenderId == request.DealerId && x.IsActive).ToListAsync();
            if (list == null)
            {
                return new ApiResponse<List<MessageResponse>>("Record not found!");
            }
            List<MessageResponse> mappedList = mapper.Map<List<MessageResponse>>(list);
            foreach (MessageResponse message in mappedList)
            {
                message.SenderUserName = await dbContextClass.Set<Dealer>().Where(x => x.UserLoginId == message.SenderId && x.IsActive).Select(x => x.Name).FirstOrDefaultAsync();

            }
            return new ApiResponse<List<MessageResponse>>(mappedList);
        }
    }
}
