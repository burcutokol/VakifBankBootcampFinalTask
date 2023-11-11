using BaseProject;
using BaseProject.Response;
using Braintree;
using DataProject.Entites;
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
    public class PaymentController : ControllerBase
    {
        private IMediator mediator;
        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<List<PaymentResponse>>> GetDealersPayment()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var handler = new GetDealersPaymentByIdQuery(int.Parse(userId));
            var result = await mediator.Send(handler);
            return result;
        }
        [HttpGet("GetPaymentById")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<PaymentResponse>> GetPaymentById(int paymentId)
        {
            var handler = new GetPaymentByIdQuery(paymentId);
            var result = await mediator.Send(handler);
            return result;
        }
        [HttpPost]
        public async Task<ApiResponse<PaymentResponse>> CreatePayment([FromBody]PaymentRequest model,int orderId)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            IRequest<ApiResponse<PaymentResponse>> operation = null;
            if (model.PaymentType == "Eft/Havale")
                operation = new CreateEFTPaymentCommand(model,orderId);
            else if(model.PaymentType == "Açik Hesap")
                operation = new CreateCreditLimitPaymentCommand(model, orderId, int.Parse(userId));

            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost("GetCreditCardPayment")]
        public async Task<ApiResponse<PaymentResponse>> CreateCreditCardPayment([FromBody] SchemaProject.CreditCard model, int orderId)
        {
            IRequest<ApiResponse<PaymentResponse>> operation = new CreateCreditCardPaymentCommand(model, orderId);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost("DeleteMessage")]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ApiResponse> DeletePayment(int PaymentId)
        {
            var operation = new DeletePaymentCommand(PaymentId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
