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
        [HttpGet("GetMessageById{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<MessageResponse>> GetMessageById(int id)
        {
            var handler = new GetMessageByIdQuery(id);
            var result = await mediator.Send(handler);
            return result;
        }
        [HttpGet("GetMessageByUserId{id}")]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ApiResponse<List<MessageResponse>>> GetMessageByUserId(int id, char SenderOrReceiver)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var handler = new GetDealersMessagesByIdQuery(id, SenderOrReceiver);
            var result = await mediator.Send(handler);
            return result;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ApiResponse<MessageResponse>> CreateMessage([FromBody]MessageRequest model)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new CreateMessageCommand(model, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost("UpdateMessage")]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ApiResponse> UpdateMessage([FromBody] MessageRequest model, int messageId)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateMessageCommand(model, messageId, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost("DeleteMessage")]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ApiResponse> DeleteMessage(int messageId)
        {
            var operation = new DeleteMessageCommand(messageId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
