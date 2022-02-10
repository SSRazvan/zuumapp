using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Contacts.Queries.GetContacts;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

public class ContactsController : ApiControllerBase
{
    [HttpGet]
    [Route("GetContacts")]
    public async Task<ActionResult<PaginatedDataVm<ContactDTO>>>  GetContacts([FromQuery] GetContactsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
