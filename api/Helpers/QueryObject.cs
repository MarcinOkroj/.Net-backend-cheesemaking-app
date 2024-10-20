using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; } = null;
        public string? Style { get; set; } = null;
        public string? Bacterias { get; set; } = null;
        public string? Complexity { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}