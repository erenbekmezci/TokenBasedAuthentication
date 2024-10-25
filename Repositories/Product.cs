using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public int Stock { get; set; }
        public decimal Price { get; set; }

    }
}
