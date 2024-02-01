using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.AccountModels;
using Webapi.Data.DataModels;
using Webapi.Data;
using Webapi.Data.Repositories.Interfaces;
using Webapi.Data.Repositories;

namespace Webapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
	    private readonly IUnitOfWork _uow;

        public AccountController(IUnitOfWork uow)
        {
	        _uow = uow;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccounts()
        {
            var data = await _uow.AccountRepository.GetAll();

            return Ok(data);
        }

        [HttpGet("{EmailAddress}", Name = "GetAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmailAddress(string EmailAddress)
        { 
            var data = await _uow.AccountRepository.GetByEmailAddress(EmailAddress);
            return Ok(data);

        }

        [HttpPut("{accountId:int}", Name = "UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccount(int accountId, AccountUpdateModel account)
        {
            await _uow.AccountRepository.UpdateAccount(accountId, account);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAccount(AccountModel account)
        {
            await _uow.AccountRepository.CreateAccount(account);
            return Ok();
        }

        [HttpPost("check-if-account-exist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckIfAccountExist([FromBody] LoginModel model)
        {
            var account = await _uow.AccountRepository.CheckIfAccountExist(model);
            return Ok(account);
        }
    }
}
