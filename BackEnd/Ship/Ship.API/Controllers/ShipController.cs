using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ship.API.Commands;
using Ship.API.Queries;
using Ship.Core.Roles;
using System.Net;

namespace Ship.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ShipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = UserRole.SuperAdminRole)]
        public async Task<IActionResult> Post([FromBody] CreateShipCommand command)
        {
            var commandResult = await _mediator.Send(command);

            return commandResult != null ? Ok(commandResult) : BadRequest() as IActionResult;

        }


        [HttpPut("{id:min(1)}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = UserRole.SuperAdminRole)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateShipCommand command)
        {
            command.Id = id;

            var commandResult = await _mediator.Send(command);

            return commandResult != null ? Ok(commandResult) : BadRequest() as IActionResult;
        }

        [HttpDelete("{id:min(1)}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = UserRole.SuperAdminRole)]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteShipCommand command = new DeleteShipCommand();
            command.Id = id;

            var commandResult = await _mediator.Send(command);

            return commandResult != null ? Ok(commandResult) : BadRequest() as IActionResult;
        }


        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = UserRole.SuperAdminRole + "," + UserRole.AdminRole)]
        public async Task<IActionResult> Get([FromQuery] GetShipListingQuery query)
        {

            var commandResult = await _mediator.Send(query);

            return commandResult != null ? Ok(commandResult) : BadRequest() as IActionResult;
        }
    }
}
