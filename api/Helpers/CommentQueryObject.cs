using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class CommentQueryObject
    {
        public int Id { get; set; }
        public bool IsDecsending { get; set; } = true;
    }
}