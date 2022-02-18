using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZuumApp.Application.Common.Exceptions;
using ZuumApp.Application.Common.Interfaces;
using ZuumApp.Domain.Entities;

namespace ZuumApp.Application.Favorites.Commands;

public class AddContactToFavoritesCommand : IRequest<int>
{
    public int ContactId { get; set; }
    public bool IsFavorite { get; set; }
}
public class AddContactToFavoritesCommandHandler : IRequestHandler<AddContactToFavoritesCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AddContactToFavoritesCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(AddContactToFavoritesCommand request, CancellationToken cancellationToken)
    {
        if(request.IsFavorite == true)
        {
            var newFavoriteContact = new Favorite
            {
                ContactId = request.ContactId,
                UserId = _currentUserService.UserId
            };

            _context.Favorites.Add(newFavoriteContact);
            await _context.SaveChangesAsync(cancellationToken);

            return newFavoriteContact.Id;
        } else
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.ContactId == request.ContactId);

            if (favorite == null)
            {
                throw new NotFoundException();
            }
            _context.Favorites.Remove(favorite);

            await _context.SaveChangesAsync(cancellationToken);

            return 0;
        }
    }
        
}

