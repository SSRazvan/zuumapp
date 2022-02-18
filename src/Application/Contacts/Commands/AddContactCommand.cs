using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuumApp.Application.Common.Interfaces;
using MediatR;
using ZuumApp.Domain.Entities;

namespace ZuumApp.Application.Contacts.Commands;

public class AddContactCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

}
public class AddContactCommandHandler : IRequestHandler<AddContactCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AddContactCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        var newContact = new Contact
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.PhoneNumber,
            UserId = _currentUserService.UserId
        };

        _context.Contacts.Add(newContact);
        await _context.SaveChangesAsync(cancellationToken);

        return newContact.Id;
    }
}