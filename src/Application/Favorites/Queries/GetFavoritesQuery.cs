using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZuumApp.Application.Common.Interfaces;
using ZuumApp.Application.Common.Models;
using ZuumApp.Application.Contacts.Queries.GetContacts;
using ZuumApp.Domain.Entities;

namespace ZuumApp.Application.Favorites.Queries;

public class GetFavoritesQuery : IRequest<PaginatedDataVm<ContactDTO>>
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
public class GetFavoritesHandler : IRequestHandler<GetFavoritesQuery, PaginatedDataVm<ContactDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetFavoritesHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedDataVm<ContactDTO>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        if (request.PageIndex != null && request.PageSize != null)
        {
            var paginatedData = await PaginatedList<Favorite>.CreateAsync(_context.Favorites.Include(i => i.Contact).Where(w => w.UserId == _currentUserService.UserId).AsNoTracking(),
                                                                            (int)request.PageIndex,
                                                                            (int)request.PageSize);
            var vm = new PaginatedDataVm<ContactDTO>()
            {
                Data = paginatedData.Items
                .Select(s => new ContactDTO
                {
                    UserId = s.UserId,
                    Id = s.Contact.Id,
                    Name = s.Contact.Name,
                    Email = s.Contact.Email,
                    Phone = s.Contact.Phone
                })

                .OrderBy(o => o.Name),
                PageNumber = paginatedData.PageNumber,
                TotalCount = paginatedData.TotalCount,
                TotalPages = paginatedData.TotalPages,
                HasPreviosPage = paginatedData.HasNextPage,
                HasNextPage = paginatedData.HasNextPage
            };
           
            return vm;
        } else
        {
            var vm = new PaginatedDataVm<ContactDTO>()
            {
                Data = await _context.Favorites
                 .Select(s => new ContactDTO
                 {
                     UserId = s.UserId,
                     Id = s.Contact.Id,
                     Name = s.Contact.Name,
                     Email = s.Contact.Email,
                     Phone = s.Contact.Phone
                 })
                 .OrderBy(o => o.Name).ToListAsync()
            };
            return vm;
        }
            
    }
}
