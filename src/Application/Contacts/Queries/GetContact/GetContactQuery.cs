using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZuumApp.Application.Common.Interfaces;
using ZuumApp.Application.Contacts.Queries.GetContacts;

namespace ZuumApp.Application.Contacts.Queries.GetContact;

public class GetContactQuery : IRequest<ContactDTO>
{
    public int Id { get; set; }
}
public class GetContactHandler : IRequestHandler<GetContactQuery, ContactDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;


    public GetContactHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContactDTO> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        return await _context.Contacts.Where(w => w.Id == request.Id)
                                                 .ProjectTo<ContactDTO>(_mapper.ConfigurationProvider)
                                                 .FirstOrDefaultAsync(cancellationToken);
    }
}