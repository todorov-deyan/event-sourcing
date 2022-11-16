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
    public class BankAccountMartenDbController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BankAccountMartenDbController> _logger;

        public BankAccountMartenDbController(IMediator mediator, ILogger<BankAccountMartenDbController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("{accountId:guid}", Name = "GetAccountMartenDb")]
        public async Task<ActionResult<Account>> Get(Guid accountId, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

                var account = await _mediator.Send(new GetAccountByIdQuery(accountId), cancellationToken);
            if(!account.IsSuccess)
                return NotFound();

            return Ok(account);
        }


        [HttpPost(Name = "CreateAccountMartenDb")]
        public async Task<ActionResult> Post(AccountCreateRequestMartenDb createAccount, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var account =  await _mediator.Send(new AccountCreateCommandMartenDb(createAccount), cancellationToken);
            if (!account.IsSuccess)
                return BadRequest();

            return CreatedAtAction("Get", new { accountId = account.Value.Id }, account.Value);
        }
    }
}
