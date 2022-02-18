using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuumApp.Application.Common.Interfaces;
using MediatR;
using ZuumApp.Application.Common.Exceptions;
using ZuumApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ZuumApp.Application.Contacts.Commands;

public class UpdateContactCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateContactCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(f => f.Id == request.Id);

        if (contact == default)
        {
            throw new NotFoundException(nameof(Contact), request.Id);
        }
        if (request.Name == "" || request.Name == null)
        {
            throw new ArgumentNullException();
        }

        contact.Name = request.Name;
        contact.Email = request.Email;
        contact.Phone = request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
