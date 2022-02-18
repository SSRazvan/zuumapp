using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZuumApp.Application.Common.Exceptions;
using ZuumApp.Application.Common.Interfaces;

namespace ZuumApp.Application.Contacts.Commands;

public class DeleteContactCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public DeleteContactHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(f => f.Id == request.Id);

        if (contact == null)
        {
            throw new NotFoundException();
        }
        var favorites = await _context.Favorites.Where(w => w.ContactId == contact.Id && w.UserId == contact.UserId).ToListAsync();
        foreach(var favorite in favorites)
        {
            _context.Favorites.Remove(favorite);
        }
        _context.Contacts.Remove(contact);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
