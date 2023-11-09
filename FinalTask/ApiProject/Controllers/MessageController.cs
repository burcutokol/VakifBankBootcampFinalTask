using BaseProject.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OperationProject.Cqrs;
using SchemaProject;
using System.Security.Claims;

namespace ApiProject.Controllers
{
    [Route("Vk/Api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMediator mediator;
        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<MessageResponse>>> GetAll()
        {
            var operation = new GetAllMessagesQuery();
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<MessageResponse>> GetMessageById(int id)
        {
            var handler = new GetMessageByIdQuery(id);
            var result = await mediator.Send(handler);
            return result;
        }
    }
}
