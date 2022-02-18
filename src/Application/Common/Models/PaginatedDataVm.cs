using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuumApp.Application.Common.Models;

public class PaginatedDataVm<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviosPage { get; set; }
    public bool HasNextPage { get; set; }
    public int PageNumber { get; set; }
    public object AdditionalData { get; set; }
}
