using ZuumApp.Application.Common.Models;
using ZuumApp.Application.Contacts.Queries.GetContacts;
using Microsoft.AspNetCore.Mvc;
using ZuumApp.Application.Contacts.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ZuumApp.Application.Contacts.Queries.GetContact;
using ZuumApp.Application.Favorites.Commands;
using ZuumApp.Application.Favorites.Queries;

namespace ZuumApp.WebUI.Controllers;

[Authorize]
public class ContactsController : ApiControllerBase
{
    [HttpGet]
    [Route("GetContacts")]
    public async Task<ActionResult<PaginatedDataVm<ContactDTO>>>  GetContacts([FromQuery] GetContactsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    [HttpGet]
    [Route("GetFavorites")]
    public async Task<ActionResult<PaginatedDataVm<ContactDTO>>> GetFavorites([FromQuery] GetFavoritesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    [HttpGet]
    [Route("GetContact")]
    public async Task<ActionResult<ContactDTO>> GetContact([FromQuery] GetContactQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    [HttpPost]
    [Route("AddContact")]
    public async Task<ActionResult<int>> AddContact(AddContactCommand command)
    {
        return await Mediator.Send(command);
    }
    [HttpPost]
    [Route("AddContactToFavorites")]
    public async Task<ActionResult<int>> AddContactToFavorites(AddContactToFavoritesCommand command)
    {
        return await Mediator.Send(command);
    }
    [HttpPut]
    [Route("UpdateContact")]
    public async Task<ActionResult<Unit>> UpdateContact(UpdateContactCommand command)
    {
        return await Mediator.Send(command);
    }
    [HttpPut]
    [Route("DeleteContact")]
    public async Task<ActionResult<Unit>> DeleteContact(int Id)
    {
        return await Mediator.Send(new DeleteContactCommand { Id = Id });
    }
}
