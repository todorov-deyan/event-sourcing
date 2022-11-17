using EventSourcing.Api.Aggregates.CustomEs.Commands;
using EventSourcing.Api.Aggregates.CustomEs.ModelDto;
using EventSourcing.Api.Aggregates.CustomEs.Queries;
using EventSourcing.Api.Aggregates.MartenDb.Commands;
using EventSourcing.Api.Aggregates.MartenDb.Queries;
using EventSourcing.Api.Aggregates.Model;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BankAccountCustomEsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BankAccountCustomEsController> _logger;

        public BankAccountCustomEsController(IMediator mediator, ILogger<BankAccountCustomEsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("{accountId:guid}", Name = "GetAccountCustomEs")]
        public async Task<ActionResult<Account>> Get(Guid accountId, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var account = await _mediator.Send(new GetAccountByIdCustomEs(accountId), cancellationToken);
            if (!account.IsSuccess)
                return NotFound();

            return Ok(account.Value);
        }


        [HttpPost(Name = "CreateAccountCustomEs")]
        public async Task<ActionResult> Post(AccountCreateRequestCustomEs createAccount, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var account = await _mediator.Send(new AccountCreateCommandCustomEs(createAccount), cancellationToken);
            if (!account.IsSuccess)
                return BadRequest();

            return CreatedAtAction("Get", new { accountId = account.Value.Id }, account.Value);
        }

        [HttpPut("{accountId}/Activate")]
        public async Task<ActionResult> ActivateAccountCustomEs(Guid accountId, AccountActivateRequestCustomEs activateAccount, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var account = await _mediator.Send(new AccountActivateCommandCustomEs(accountId, activateAccount), cancellationToken);
            if (!account.IsSuccess)
                return BadRequest();

            return CreatedAtAction("Get", new { accountId = account.Value.Id }, account.Value);
        }

        [HttpPut("{accountId}/Deactivate")]
        public async Task<ActionResult> DeactivateAccountCustomEs(Guid accountId, AccountDeactivateRequestCustomEs deactivateAccount, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var account = await _mediator.Send(new AccountDeactivateCommandCustomEs(accountId, deactivateAccount), cancellationToken);
            if (!account.IsSuccess)
                return BadRequest();

            return CreatedAtAction("Get", new { accountId = account.Value.Id }, account.Value);
        }
    }
}
