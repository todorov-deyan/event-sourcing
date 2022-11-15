using EventSourcing.Api.Aggregates.MartenDb.Commands;
using EventSourcing.Api.Aggregates.MartenDb.ModelDto;
using EventSourcing.Api.Aggregates.MartenDb.Queries;
using EventSourcing.Api.Aggregates.Model;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BankAccountController> _logger;

        public BankAccountController(IMediator mediator, ILogger<BankAccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("{accountId:guid}", Name = "GetAccount")]
        public async Task<ActionResult<Account>> Get(Guid accountId, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

                var account = await _mediator.Send(new GetAccountByIdQuery(accountId), cancellationToken);
            if(!account.IsSuccess)
                return NotFound();

            return Ok(account);
        }


        [HttpPost(Name = "CreateAccount")]
        public async Task<ActionResult> Post(AccountCreateRequest createAccount, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var account =  await _mediator.Send(new AccountCreateCommand(createAccount), cancellationToken);
            if (!account.IsSuccess)
                return BadRequest();

            return CreatedAtAction("Get", new { accountId = account.Value.Id }, account.Value);
        }
    }
}
