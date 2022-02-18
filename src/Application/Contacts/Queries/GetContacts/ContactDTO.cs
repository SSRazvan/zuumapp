using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuumApp.Application.Common.Mappings;
using ZuumApp.Domain.Entities;

namespace ZuumApp.Application.Contacts.Queries.GetContacts;

public class ContactDTO : IMapFrom<Contact>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string UserId { get; set; }
    public bool IsFavorite { get; set; }
}
