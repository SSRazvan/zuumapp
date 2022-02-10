using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Contacts.Queries.GetContacts;

public class GetContactsQuery : IRequest<PaginatedDataVm<ContactDTO>>
{
    public string UserEmail { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
public class GetContactsHandler : IRequestHandler<GetContactsQuery, PaginatedDataVm<ContactDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContactsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedDataVm<ContactDTO>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        if (request.PageIndex != null && request.PageSize != null)
        {
            var paginatedData = await PaginatedList<Contact>.CreateAsync(_context.Contacts.AsNoTracking(),
                                                                            (int)request.PageIndex,
                                                                            (int)request.PageSize);
            var vm = new PaginatedDataVm<ContactDTO>()
            {
                Data = paginatedData.Items
                .Select(s => new ContactDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone,
                    Email = s.Email
                })
                .OrderBy(o => o.Name),
                PageNumber = paginatedData.PageNumber,
                TotalCount = paginatedData.TotalCount,
                TotalPages = paginatedData.TotalPages,
                HasPreviosPage = paginatedData.HasNextPage,
                HasNextPage = paginatedData.HasNextPage
            };
            return vm;

        }
        else
        {
            var vm = new PaginatedDataVm<ContactDTO>()
            {
                Data = await _context.Contacts
                 .Select(s => new ContactDTO
                 {
                     Id = s.Id,
                     Name = s.Name,
                     Phone = s.Phone,
                     Email = s.Email
                 }).OrderBy(o => o.Name).ToListAsync()
            };
            return vm;
        }
    }

    
}
