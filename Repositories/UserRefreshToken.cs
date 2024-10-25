using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRefreshToken
    {
        public string UserId { get; set; } = default!;
        public int Code { get; set; }
        public DateTime ExpireTime { get; set; }
    }

}
