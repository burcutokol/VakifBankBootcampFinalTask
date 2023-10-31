using BaseProject.Response;
using DataProject.Context;
using DataProject.Entites;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("Vk/Api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private IMediator mediator;
        public DealerController(IMediator mediator) 
        {
            this.mediator = mediator;
        }

      
    }
}
