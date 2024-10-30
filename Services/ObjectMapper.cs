using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy =new Lazy<IMapper>(() =>
        {
            var congif = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });
            return congif.CreateMapper();
        });
        public static IMapper Mapper =>lazy.Value;
    }
    
}
