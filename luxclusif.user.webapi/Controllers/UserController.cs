using luxclusif.user.application.Models;
using luxclusif.user.application.UseCases.User.CreateUser;
using luxclusif.user.webapi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace luxclusif.user.webapi.Controllers
{
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UserController : BaseController
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator,
            Notifier notifier) : base(notifier)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        [SwaggerOperation(
            OperationId = "Post_New_User",
            Summary = "Post new user")]
        [SwaggerResponse(200, Type = typeof(DefaultResponseDto<CreateUserInput>), Description = "Create new user")]
        [SwaggerResponse(400, Type = typeof(DefaultResponseDto<object>), Description = "Error")]
        [SwaggerResponse(500, Type = typeof(DefaultResponseDto<object>), Description = "Error")]
        public async Task<IActionResult> CreateConfiguration([FromBody] CreateUserInput model)
        {
            var input = new CreateUserInput(model.Name);

            var ret = await mediator.Send<CreateUserOutput>(input);

            return Result(ret);
        }
    }
}
