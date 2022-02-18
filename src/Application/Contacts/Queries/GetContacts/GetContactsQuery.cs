using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZuumApp.Application.Common.Interfaces;
using ZuumApp.Application.Common.Models;
using ZuumApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ZuumApp.Application.Contacts.Queries.GetContacts;

public class GetContactsQuery : IRequest<PaginatedDataVm<ContactDTO>>
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public string? SearchFilter { get; set; }
}
public class GetContactsHandler : IRequestHandler<GetContactsQuery, PaginatedDataVm<ContactDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetContactsHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedDataVm<ContactDTO>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        var favorites = await _context.Favorites.Where(w => w.UserId == _currentUserService.UserId).ToListAsync();
        if (request.PageIndex != null && request.PageSize != null)
        {
            var paginatedData = await PaginatedList<Contact>.CreateAsync(_context.Contacts.AsNoTracking(),
                                                                            (int)request.PageIndex,
                                                                            (int)request.PageSize);
            var vm = new PaginatedDataVm<ContactDTO>()
            {
                Data = paginatedData.Items.Where(w => w.UserId == _currentUserService.UserId)
                .Select(s => new ContactDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone,
                    Email = s.Email,
                    IsFavorite = favorites.Any(a => a.ContactId == s.Id)
                })
                
                .OrderBy(o => o.Name),
                PageNumber = paginatedData.PageNumber,
                TotalCount = paginatedData.TotalCount,
                TotalPages = paginatedData.TotalPages,
                HasPreviosPage = paginatedData.HasNextPage,
                HasNextPage = paginatedData.HasNextPage
            };
            if(request.SearchFilter != null)
            {
                vm.Data = vm.Data.Where(w => String.IsNullOrWhiteSpace(request.SearchFilter) || w.Name.ToUpper().Contains(request.SearchFilter.ToUpper()));
            }
            return vm;

        }
        else
        {
            var contacts = await _context.Contacts.Where(w => w.UserId == _currentUserService.UserId)
                 .OrderBy(o => o.Name).ToListAsync();
            var vm = new PaginatedDataVm<ContactDTO>()
            {
                Data = contacts
                 .Select(s => new ContactDTO
                 {
                     Id = s.Id,
                     Name = s.Name,
                     Phone = s.Phone,
                     Email = s.Email,
                     IsFavorite = favorites.Any(a => a.ContactId == s.Id)
                 }).Where(w => w.IsFavorite == false).ToList()
            };
            return vm;
        }
    }

    
}
