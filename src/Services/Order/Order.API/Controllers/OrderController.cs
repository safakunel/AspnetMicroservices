﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders.Commands.DeleteOrder;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Application.Features.Orders.Queries.GetOrdersList;
using System.Net;

namespace Order.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
