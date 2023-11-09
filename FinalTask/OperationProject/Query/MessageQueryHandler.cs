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
    public class MessageQueryHandler :
        IRequestHandler<GetAllMessagesQuery, ApiResponse<List<MessageResponse>>>,
        IRequestHandler<GetMessageByIdQuery, ApiResponse<MessageResponse>>
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
            List<Message> list = await dbContextClass.Set<Message>().ToListAsync(cancellationToken);

            List<MessageResponse> mapped = mapper.Map<List<MessageResponse>>(list);
            return new ApiResponse<List<MessageResponse>>(mapped);
        }

        public async Task<ApiResponse<MessageResponse>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            Message? entity = await dbContextClass.Set<Message>().FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse<MessageResponse>("Record not found!");
            }

            MessageResponse messageResponse = mapper.Map<MessageResponse>(entity);
            return new ApiResponse<MessageResponse>(messageResponse);
        }
    }
}
