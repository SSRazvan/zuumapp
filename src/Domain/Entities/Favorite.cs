using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuumApp.Domain.Entities;

public class Favorite
{
    public int Id { get; set; }
    public int ContactId {get; set; }
    public Contact Contact { get; set; }
    public string UserId { get; set; }
}
